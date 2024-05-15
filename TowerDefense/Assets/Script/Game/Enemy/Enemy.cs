using GameData;
using GameData.Enemy;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Game.Enemy
{
    /// <summary>
    /// 敵の処理
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        #region PublicField
        /// <summary>敵の情報</summary>
        public EnemyDataInfo enemyDataInfo;
        /// <summary>敵が消滅する時の処理</summary>
        public Subject<Unit> EnemyDestroySubject = new Subject<Unit>();
        #endregion

        #region PrivateField
        /// <summary>敵の体力</summary>
        private int life;
        /// <summary>現在の中継を通過した値</summary>
        private int currentRouteAnchorIndex = 0;
        /// <summary>NavMeshAgentコンポーネント</summary>
        private NavMeshAgent agent;
        #endregion

        #region SerializeField
        /// <summary>移動目標</summary>
        [SerializeField] private Transform target;
        /// <summary>敵の体力バー</summary>
        [SerializeField] private Canvas sliderCanvas;
        /// <summary>敵の体力バー</summary>
        [SerializeField] private Slider lifeBar;
        #endregion

        #region UnityEvent
        void Update()
        {
            RotationLifeBar();


            if (!agent.pathPending && agent.remainingDistance < 0.1f)
            {
                MoveToNextWaypoint();
            }
        }
        #endregion

        #region PublicMethod
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init(EnemyDataInfo enemyDataInfo)
        {
            agent = GetComponent<NavMeshAgent>();

            var stageDataInfo = GameDataManager.instance.GetStageDataInfo();
            target = stageDataInfo.fortressTransform;

            this.enemyDataInfo = enemyDataInfo;
            life = enemyDataInfo.life;
            // 体力バーを設定する
            lifeBar.maxValue = enemyDataInfo.life;
            lifeBar.value = life;

            agent.speed = enemyDataInfo.speed;
            agent.destination = target.position;
        }

        private void MoveToNextWaypoint()
        {
            // 現在の中継地点の次の地点を設定
            currentRouteAnchorIndex++;

            var stageDataInfo = GameDataManager.instance.GetStageDataInfo();
            var gameDataInfo = GameDataManager.instance.GetGameDataInfo();

            // もしすべての中継地点を通過したら、砦に向かう
            if (currentRouteAnchorIndex >= stageDataInfo.waveInfo[gameDataInfo.waveNum].routeAnchor.Count)
            {
                agent.destination = target.position;
            }
            else
            {
                // 次の中継地点に向かう
                agent.destination = stageDataInfo.waveInfo[gameDataInfo.waveNum].routeAnchor[currentRouteAnchorIndex].position;
            }
        }

        /// <summary>
        /// ダメージを与える処理
        /// </summary>
        /// <param name="damage">ダメージ量</param>
        public void TakeDamage(int damage)
        {
            life -= damage;

            lifeBar.value = life;
            LifeCheck();
        }
        #endregion

        #region PrivateMethod
        /// <summary>
        /// 弾を発射する
        /// </summary>
        private void LifeCheck()
        {
            // HPが0を下回ったかどうかを確認
            if (IsLifeZero())
            {
                EnemyDestroySubject.OnNext(Unit.Default);
            }
        }

        /// <summary>
        /// 体力バーをカメラに見えるように回転させる処理
        /// </summary>
        private void RotationLifeBar()
        {
            Vector3 cameraDirection = Camera.main.transform.forward;

            // HPバーの方向をカメラの方向に向ける
            sliderCanvas.transform.LookAt(sliderCanvas.transform.position + cameraDirection);

            // カメラのY軸回転を無視してHPバーを水平に保つ
            Quaternion targetRotation = Quaternion.Euler(0, sliderCanvas.transform.rotation.eulerAngles.y, 0);
            sliderCanvas.transform.rotation = Quaternion.Lerp(sliderCanvas.transform.rotation, targetRotation, Time.deltaTime);
        }

        /// <summary>
        /// 体力が0を下回ったかどうかを判定
        /// </summary>
        private bool IsLifeZero()
        {
            return life <= 0;
        }
        #endregion
    }
}
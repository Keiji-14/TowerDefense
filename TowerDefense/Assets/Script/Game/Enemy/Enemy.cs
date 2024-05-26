using Audio;
using GameData;
using GameData.Enemy;
using GameData.Stage;
using System.Collections.Generic;
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
        /// <summary>敵を倒した時の処理</summary>
        public Subject<Unit> EnemyDefeatSubject = new Subject<Unit>();
        #endregion

        #region PrivateField
        /// <summary>敵の体力</summary>
        private int life;
        /// <summary>現在の中継を通過した値</summary>
        private int currentRouteAnchorIndex = -1;
        /// <summary>敵の移動速度</summary>
        private float speed;
        /// <summary>NavMeshAgentコンポーネント</summary>
        private NavMeshAgent agent;
        /// <summary>通過するルート</summary>
        private List<Transform> routeAnchor;
        
        #endregion

        #region SerializeField
        /// <summary>移動目標</summary>
        [SerializeField] private Transform target;
        /// <summary>敵の体力バー</summary>
        [SerializeField] private Canvas sliderCanvas;
        /// <summary>敵の体力バー</summary>
        [SerializeField] private Slider lifeBar;
        /// <summary>消滅時の効果音</summary>
        [SerializeField] private AudioClip destroySE;
        /// <summary>消滅時のパーティクル</summary>
        [SerializeField] public ParticleSystem destroyParticle;
        #endregion

        #region UnityEvent
        void Update()
        {
            // ゲームオーバー時に動作を停止させる
            if (GameDataManager.instance.GetGameDataInfo().isGameOver)
            {
                StopMovement();
            }
            else
            {
                RotationLifeBar();

                if (!agent.pathPending && agent.remainingDistance < 0.1f)
                {
                    MoveToNextWaypoint();
                }
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

            var gameDataInfo = GameDataManager.instance.GetGameDataInfo();
            var stageDataInfo = GameDataManager.instance.GetStageDataInfo();
            target = stageDataInfo.fortressTransform;
            routeAnchor = stageDataInfo.waveInfo[gameDataInfo.waveNum].routeAnchor;

            InitStatus(enemyDataInfo);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Init(EnemyDataInfo enemyDataInfo, RouteInfo routeInfo)
        {
            agent = GetComponent<NavMeshAgent>();

            var stageDataInfo = GameDataManager.instance.GetEXStageDataInfo();
            target = stageDataInfo.fortressTransform;
            routeAnchor = routeInfo.routeAnchor;

            InitStatus(enemyDataInfo);
        }

        /// <summary>
        /// ステータスの初期化
        /// </summary>
        public void InitStatus(EnemyDataInfo enemyDataInfo)
        {
            // 敵の情報を保持させる
            this.enemyDataInfo = enemyDataInfo;
            life = enemyDataInfo.life;
            // 体力バーを設定する
            lifeBar.maxValue = enemyDataInfo.life;
            lifeBar.value = life;

            // 移動速度を設定する
            speed = enemyDataInfo.speed;
            agent.speed = speed;
            agent.baseOffset = enemyDataInfo.baseOffset;
            // 目的地に近づいても速度を落とさない
            agent.autoBraking = false;
        }

        private void MoveToNextWaypoint()
        {
            // 現在の中継地点の次の地点を設定
            currentRouteAnchorIndex++;

            var gameDataInfo = GameDataManager.instance.GetGameDataInfo();

            if (gameDataInfo.stageType == StageType.EX)
            {
                // ルート情報のリストがnullでないことを確認
                if (routeAnchor != null && currentRouteAnchorIndex < routeAnchor.Count)
                {
                    // 次の中継地点に向かう
                    agent.destination = routeAnchor[currentRouteAnchorIndex].position;
                    
                }
                else
                {
                    // もしすべての中継地点を通過したら、砦に向かう
                    agent.destination = target.position;
                }
            }
            else
            {
                var stageDataInfo = GameDataManager.instance.GetStageDataInfo();

                // ルート情報のリストがnullでないことを確認
                if (routeAnchor != null && currentRouteAnchorIndex < routeAnchor.Count)
                {
                    // 次の中継地点に向かう
                    agent.destination = routeAnchor[currentRouteAnchorIndex].position;
                }
                else
                {
                    // もしすべての中継地点を通過したら、砦に向かう
                    agent.destination = target.position;
                }
            }
        }

        /// <summary>
        /// 動作を停止させる処理
        /// </summary>
        private void StopMovement()
        {
            if (agent != null && agent.isActiveAndEnabled)
            {
                agent.isStopped = true;
                agent.velocity = Vector3.zero;  // 動きを完全に停止させる
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

        /// <summary>
        /// 消滅時の処理
        /// </summary>
        public void Destroy()
        {
            Instantiate(destroyParticle, transform.position, Quaternion.identity);
            SE.instance.Play(destroySE);

            Destroy(gameObject);
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
                EnemyDefeatSubject.OnNext(Unit.Default);
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
        /// 移動速度を低下させる処理
        /// </summary>
        /// <param name="downValue">i低下率</param>
        public void ReduceSpeed(float downValue)
        {
            agent.speed = speed * downValue;
        }

        /// <summary>
        /// 移動速度を元に戻す処理
        /// </summary>
        public void ResetSpeed()
        {
            agent.speed = speed; // 元の速度に戻す
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
using UnityEngine;

namespace Game.Tower
{
    public class Bullet : MonoBehaviour
    {
        #region PrivateField
        /// <summary>�U����</summary>
        private int attack;
        /// <summary>�e�̑��x</summary>
        private float bulletSpeed;
        /// <summary>�ǔ��Ώۂ̓G</summary>
        private GameObject targetEnemy;
        /// <summary>�ǔ��Ώۂ̈ʒu</summary>
        private Vector3 targetPosition;
        #endregion

        #region UnityEvent
        void Update()
        {
            // �ǔ��Ώۂ����݂���ꍇ�͂��̕����Ɉړ�����
            if (targetEnemy != null)
            {
                // �ǔ��Ώۂ̈ʒu���X�V
                targetPosition = targetEnemy.transform.position;
                // �ǔ��Ώۂ̕����Ɍ������Ĉړ�
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, bulletSpeed * Time.deltaTime);
            }
            else
            {
                // �ǔ��Ώۂ����݂��Ȃ��ꍇ�͒e��j������
                Destroy(this.gameObject);
            }
        }
        #endregion

        #region PublicMethod
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="enemyObj">�ǔ��Ώ�</param>
        public void Init(int attack, float bulletSpeed, GameObject enemyObj)
        {
            this.attack = attack;
            this.bulletSpeed = bulletSpeed;
            targetEnemy = enemyObj;
            targetPosition = targetEnemy.transform.position;
        }
        #endregion

        #region PrivateMethod
        // �G���e�ɓ����������̏���
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("Enemy"))
            {
                var enemy = other.GetComponent<Enemy.Enemy>();

                enemy.TakeDamage(attack);

                Destroy(this.gameObject);
            }
        }
        #endregion
    }
}
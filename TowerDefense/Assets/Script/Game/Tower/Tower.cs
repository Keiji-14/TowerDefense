using GameData.Tower;
using UnityEngine;

namespace Game.Tower
{
    /// <summary>
    /// �^���[�ɂ��Ă̏���
    /// </summary>
    public class Tower : MonoBehaviour
    {
        #region PrivateField
        /// <summary>�^���[��]��Y���̒l</summary>
        private const float VerticalLockValue = 0f;
        /// <summary>�^���[�̉�]���x</summary>
        private float rotationSpeed = 5f;
        /// <summary></summary>
        private float lastAttackTime;
        /// <summary>���݂����^���[�̏��</summary>
        private TowerData towerData;
        #endregion

        #region SerializeField
        /// <summary>�e�̔��ˌ�</summary>
        [SerializeField] Transform firePoint;
        #endregion

        #region PublicMethod
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="towerData">�^���[�̏��</param>
        public void Init(TowerData towerData)
        {
            // �^���[�̏���ێ�������
            this.towerData = towerData;
        }
        #endregion

        #region PrivateMethod
        private void OnTriggerStay(Collider other)
        {
            // �G�������ɓ��������ǂ���
            if (other.transform.CompareTag("Enemy"))
            {
                ActionType(other.gameObject);
            }
        }

        /// <summary>
        /// �s���p�^�[���̔���
        /// </summary>
        /// <param name="enemyObj">�U���Ώ�</param>
        private void ActionType(GameObject enemyObj)
        {
            switch (towerData.towerType)
            {
                case TowerType.MachineGun:
                    LookTarget(enemyObj);
                    MachineGun(enemyObj);
                    break;
                case TowerType.Cannon:
                    LookTarget(enemyObj);
                    break;
                case TowerType.Jamming:
                    break;
            }
        }

        /// <summary>
        /// �^�[�Q�b�g�̕�������������
        /// </summary>
        /// <param name="enemyObj">�U���Ώ�</param>
        private void LookTarget(GameObject enemyObj)
        {
            Vector3 targetDirection = enemyObj.transform.position - transform.position;
            targetDirection.y = VerticalLockValue;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        /// <summary>
        /// �e�𔭎˂���
        /// </summary>
        /// <param name="enemyObj">�U���Ώ�</param>
        private void MachineGun(GameObject enemyObj)
        {
            // 1�t���[��������̃_���[�W�ʂ��Z�o�i�U�����x�ɉ����āj
            if (Time.time - lastAttackTime > (1f / towerData.attackSpeed))
            {
                var bullet = Instantiate(towerData.bulletObj, firePoint.position, firePoint.rotation).GetComponent<Bullet>();
                bullet.Init(towerData.attack, towerData.bulletSpeed, enemyObj);
                // �O��̍U���������X�V
                lastAttackTime = Time.time;
            }
        }

        /// <summary>
        /// �e�𔭎˂���
        /// </summary>
        /// <param name="enemyObj">�U���Ώ�</param>
        private void Shot(GameObject enemyObj)
        {
            var enemy = enemyObj.GetComponent<Enemy.Enemy>();

            // 1�t���[��������̃_���[�W�ʂ��Z�o�i�U�����x�ɉ����āj
            float damage = towerData.attack * (1f / towerData.attackSpeed) * Time.deltaTime;

            enemy.TakeDamage(damage);

            Debug.Log("�U��");
        }
        #endregion
    }
}
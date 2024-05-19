using System.Collections.Generic;
using UnityEngine;

namespace GameData.Stage
{
    /// <summary>
    /// EX�X�e�[�W�̏���ێ�����ScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "EXStageData", menuName = "Create EX Stage Data")]
    public class EXStageDataInfo : ScriptableObject
    {
        #region PublicField
        /// <summary>�X�e�[�WID</summary>
        public int stageID;
        /// <summary>�J�n���̍Ԃ̑ϋv�l</summary>
        public int startFortressLife;
        /// <summary>�J�n���̏�����</summary>
        public int startMoney;
        /// <summary>�E�F�[�u�̃C���^�[�o��</summary>
        public float waveInterval;
        /// <summary>�X�e�[�W�̃I�u�W�F�N�g</summary>
        public GameObject stageObj;
        /// <summary>�Ԃ̏ꏊ</summary>
        public Transform fortressTransform;
        /// <summary>EX�X�e�[�W�̃��[�g�̏��</summary>
        public List<RouteInfo> routeInfoList = new List<RouteInfo>();
        #endregion
    }

    /// <summary>
    /// EX�X�e�[�W�̃��[�g�̏��
    /// </summary>
    [System.Serializable]
    public class RouteInfo
    {
        #region PublicField
        /// <summary>�G�̏o����</summary>
        //public List<EnemySpawnInfo> enemySpawnInfoList = new List<EnemySpawnInfo>();
        /// <summary>�G�̏o���ꏊ</summary>
        public Transform spawnPoint;
        /// <summary>���[�g�̒��p�n�_</summary>
        public List<Transform> routeAnchor;
        #endregion
    }
}
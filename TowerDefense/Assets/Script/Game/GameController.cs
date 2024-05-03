using Game.Tower;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// �Q�[����ʂ̏���
    /// </summary>
    #region SerializeField 
    /// <summary>�^���[�̏���</summary>
    [SerializeField] private TowerController towerController;
    #endregion

    #region PublicMethod
    /// <summary>
    /// ������
    /// </summary>
    public void Init()
    {
        towerController.Init();
    }
    #endregion
}

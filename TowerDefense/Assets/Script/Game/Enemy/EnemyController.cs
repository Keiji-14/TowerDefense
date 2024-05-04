using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    #region PrivateField
    /// <summary>Rigidbodyコンポーネント</summary>
    private Rigidbody rb;
    /// <summary>NavMeshAgentコンポーネント</summary>
    private NavMeshAgent agent;
    #endregion

    #region SerializeField
    [SerializeField] private Vector3 distance;
    [SerializeField] private Transform targetPlayer;
    #endregion

    #region UnityEvent
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        agent.destination = targetPlayer.position;
    }
    #endregion
}

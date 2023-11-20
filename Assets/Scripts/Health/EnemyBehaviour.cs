using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] public List<Transform> _Waypoints;
    [SerializeField] private bool _MoveRandom;
    [SerializeField] private float _TargetDistance;

    public int _currentIndex;

    private UnityEngine.AI.NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _currentIndex = -1;
        SetNextTarget();
    }

    private void Update()
    {
        if (_agent.remainingDistance <= _TargetDistance)
        {
            SetNextTarget();
        }
    }

    private void SetNextTarget()
    {
        int prevIndex = _currentIndex;
        if (_MoveRandom)
        {
            _currentIndex = Random.Range(0, _Waypoints.Count);
            if (_currentIndex == prevIndex)
            {
                _currentIndex++;
            }
        }
        else _currentIndex++;
        _currentIndex %= _Waypoints.Count;

        _agent.SetDestination(_Waypoints[_currentIndex].position);
    }
    private void OnDrawGizmos()
    {
        if (_Waypoints.Count == 0)
        {
            return;
        }

        Gizmos.color = Color.black;
        foreach (Transform waypoint in _Waypoints)
        {
            Gizmos.DrawSphere(waypoint.position, 0.2f);
        }

        if (!Application.isPlaying)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _agent.destination);
    }

    private void OnDrawGizmosSelected()
    {
        if (_Waypoints.Count == 0)
        {
            return;
        }

        Gizmos.color = Color.green;
        foreach (Transform waypoint in _Waypoints)
        {
            Gizmos.DrawSphere(waypoint.position, 0.35f);
        }
    }
}

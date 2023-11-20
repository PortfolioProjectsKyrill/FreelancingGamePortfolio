using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Wander,
    Attack
}

public class EnemyAttacking : MonoBehaviour
{
    [Header("Attacking Vars")]
    [SerializeField] private float attackForce = 10f;
    [SerializeField] private float attackCooldown = 1f;

    [SerializeField] private State state;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    private EnemyBehaviour enemyBehaviour;
    private PlayerManager playerScript;
    private FieldOfView fieldOfViewScript;

    [SerializeField] private float AttackDistance;
    [SerializeField] private GameObject destination;

    private Collision Collisionlocal;

    private Rigidbody enemyRigidbody;
    private bool canAttack = true;

    void Start()
    {
        enemyBehaviour = GetComponent<EnemyBehaviour>();
        playerScript = FindObjectOfType<PlayerManager>();
        fieldOfViewScript = GetComponent<FieldOfView>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        enemyRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        state = CheckSwitchConditions();
        switch (state)
        {
            case State.Wander:
                Wander();
                break;

            case State.Attack:
                Attack();
                break;
        }
    }

    private void Wander()
    {
        //activate Enemybehaviour script
        enemyBehaviour.enabled = true;
        navMeshAgent.stoppingDistance = 0;
    }

    private void Attack()
    {
        navMeshAgent.stoppingDistance = 5;
        enemyBehaviour.enabled = false;
        navMeshAgent.SetDestination(destination.transform.position);
        
        if (canAttack) 
        {
            StartCoroutine(AttackPlayer());
        }
    }

    private State CheckSwitchConditions()
    {
        if (Vector3.Distance(transform.position, enemyBehaviour._Waypoints[enemyBehaviour._currentIndex].position) < AttackDistance && fieldOfViewScript.TargetInVision())
        {
            return State.Attack;
        }
        else return State.Wander;
    }

    private IEnumerator AttackPlayer()
    {
        canAttack = false;

        Vector3 playerDirection = (destination.transform.position - transform.position).normalized;
        Vector3 attackDirection = new Vector3(playerDirection.x, 0f, playerDirection.z);

        enemyRigidbody.AddForce(attackDirection * attackForce, ForceMode.Impulse);

        //make the player take damage
        destination.GetComponent<PlayerHealth>().SetHealth(25);

        yield return new WaitForSeconds(attackCooldown);

        enemyRigidbody.AddForce(-attackDirection * attackForce, ForceMode.Impulse);

        canAttack = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collision();
    }

    private Collision Collision()
    {
        Collision l_collision = Collisionlocal;
        return l_collision;
    }
}

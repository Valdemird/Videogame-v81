using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float attackDistance = 0.5f;
    [SerializeField] private float attackRate = 1f;
    [SerializeField] private Collider attackCollider;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private float nextAttackTime;
    private bool isBeingDestroyed = false;
    private Transform player;

    private static readonly int DestroyHash = Animator.StringToHash("destroy");
    private static readonly int AttackHash = Animator.StringToHash("attack");

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent.updateRotation = true;
        nextAttackTime = Mathf.NegativeInfinity;
    }

    private void Update()
    {
        if (isBeingDestroyed) return;

        navMeshAgent.SetDestination(player.position);

        if (navMeshAgent.remainingDistance <= attackDistance && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    public void Hit()
    {
        isBeingDestroyed = true;
        navMeshAgent.isStopped = true;
        animator.Play(DestroyHash);
        StartCoroutine(DestroyEnemy());
    }

    private IEnumerator DestroyEnemy()
    {
        GameManager.instance.gainScorePoints(1);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    private void Attack()
    {
        animator.Play(AttackHash);
        StartCoroutine(DisableAttackColliderAfterDelay());
    }

    private IEnumerator DisableAttackColliderAfterDelay()
    {
        yield return new WaitForSeconds(0.4f);
        attackCollider.enabled = true;
        yield return new WaitForSeconds(0.45f);
        attackCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<MovementHandler>().GetDamage(1);
        }
    }

    private void OnDestroy()
    {
        SpawnerBehavior.currentEnemies--;
    }
}
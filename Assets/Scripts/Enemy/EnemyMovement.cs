using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIdPlayer;

    public int health;

    //Patroling

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    [SerializeField]
    bool alreadyAttacked;
    public GameObject projectile;
    public int damage;

    //States
    public float sightRange, attackRange, playerLeaveSightRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight an attack range


        Vector3 direction = player.position - transform.position;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit))
        {

            if (raycastHit.collider.tag == "Player" || raycastHit.collider.tag == "Enemy" || raycastHit.collider.tag == "Projectile")
            {
                if (!playerInSightRange)
                {
                    playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIdPlayer);
                }
                else
                {
                    playerInSightRange = Physics.CheckSphere(transform.position, playerLeaveSightRange, whatIdPlayer);
                }
                playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIdPlayer);
            }
            else
            {
                playerInAttackRange = false;
            }

        }

        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();


    }


    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up,2f,whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {

        agent.SetDestination(transform.position);
        transform.LookAt(new Vector3(player.position.x, 0.12f, player.position.y));
        if(!alreadyAttacked)
        {
            //Attack code goes ehre
            GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
            proj.GetComponent<Projectile>().damage = damage;
            Rigidbody rb = proj.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.Normalize(player.transform.position - transform.position) , ForceMode.Impulse);
            

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health<= 0)
        {
            Invoke(nameof(DestroyEnemy), 0f);
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

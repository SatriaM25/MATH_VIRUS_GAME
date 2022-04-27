using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    private float speed = 5.0f;
    public Rigidbody rb;
    private Animator animator;
    private Vector3 movement;
    public Transform playerPos;
    private Vector3 relativePos;
    public FirstPersonController playerScript;
    public Transform RespawnPos;
    public CheckAnswer checkAnswer;

    // // Waypoints
    // public Vector3[] waypoints;

    // Handle Health
    public GameObject healthBarUI;
    public Slider slider;
    public float maxHealth;
    public float health;

    // Conditions
    private bool dizzy = false;
    public bool dead = true;
    private bool canAttack = true;
    private bool stopMoving = false;
    
    // Handle Attack
    public LayerMask whatIsPlayer;
    public LayerMask whatIsFence;
    private bool encounterFence;
    private bool playerInAttackRange;
    private float attackRange = 3f;
    private float attackTimer = 1.533f;

    //Handle Dizzy
    private float cooldown;


    // Start is called before the first frame update
    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        maxHealth = 100f;
        health = maxHealth;
        slider.value = CalculateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        relativePos = playerPos.position - transform.position;
        if(canAttack){
            transform.LookAt(playerPos);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            // encounterFence = Physics.CheckSphere(transform.position, 1f, whatIsFence);

            
            if(playerInAttackRange){
                Attack();
                attackTimer -= Time.deltaTime;
                if(attackTimer <= 0)
                {
                    Debug.Log("attack is called");
                    playerScript.Attacked(10f);
                    attackTimer = 1.533f;
                }
            }else {
                attackTimer = 1.533f;
                stopMoving = false;
                
            }
        } else if(dizzy){
            bool isSet = false;
            bool correct = false;
            if(!isSet)
            {
                correct = checkAnswer.isCorrect();
                isSet = true;
            }
            if(correct)
            {
                Dead();
            }
        } 
        // else if(encounterFence)
        // {
        //     Jump();
        //     encounterFence = false;
        // }
    }
    void FixedUpdate(){
        if(!stopMoving){
            moveCharacter(relativePos);
        }
    }
    void moveCharacter(Vector3 direction){
        bool isRunning = animator.GetBool("isRunning");
        if(!isRunning){
            StopAllCoroutines();
            animator.SetBool("isAttack", false);
            animator.SetBool("isJump", false);
            animator.SetBool("isRunning", true);
            animator.SetBool("isDead", false);
            Debug.Log("Running animation executed");
        }
        direction.Normalize();
        rb.velocity = direction * speed;


        // RaycastHit hit;
        // Ray checkingRay = new Ray(transform.position, playerPos.position * 5f);

        // Debug.DrawRay(transform.position, playerPos.position * 5f);
        // Debug.Log(playerPos.position);

        // if(Physics.Raycast(checkingRay, out hit))
        // {
        //     if(hit.collider.tag == "Fence")
        //     {
        //         Debug.Log("hit fence");
        //         Vector3 waypointDir = FindClosestWaypoint();
        //         waypointDir.Normalize();
        //         rb.velocity = waypointDir * speed;
        //     }else{
        //         direction.Normalize();
        //         rb.velocity = direction * speed;
        //     }
        // }
    }

    // Vector3 FindClosestWaypoint()
    // {
    //     Vector3 closest = waypoints[0];
    //     float x = 0;
    //     float z = 0;
    //     float path = 0;
    //     float prevPath = 0;
    //     for(int i = 1; i<waypoints.Length; i++)
    //     {
    //         x = transform.position.x - waypoints[i].x;
    //         z = transform.position.z - waypoints[i].z;
    //         path = x*x + z*z;
    //         prevPath = closest.x*closest.x + closest.z*closest.z;
    //         if(path>=prevPath)
    //         {
    //             closest = waypoints[i];
    //         }
    //     }
    //     Debug.Log(closest);
    //     return closest;
    // }
    void OnTriggerEnter(Collider other) {
        if(!dizzy)
        {
            if(other.tag == "bullet"){
            SFX.PlaySound("Hit");
            health -= 10f;
            slider.value = CalculateHealth();
            if(health <= 0f)
            {
               Dizzy();
            }
            }

            if(other.tag == "Fence")
            {
                Jump();
            }
        }
        
    }
    void Attack(){
        bool isAttack = animator.GetBool("isAttack");
        if(!isAttack)
        {
            stopMoving = true;
            rb.velocity = Vector3.zero;
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttack", true);
            animator.SetBool("isJump", false);
        }
    }

    void Jump()
    {
        bool isJump = animator.GetBool("isJump");
        if(!isJump)
        {
            Debug.Log("jump exeucted");
            animator.SetBool("isRunning", false);
            animator.SetBool("isJump", true);
            // animator.SetBool("isRunning", false);
        }
    }

    void Dizzy(){
        bool isDizzy = animator.GetBool("isDizzy");
        if (!isDizzy)
        {
            StopAllCoroutines();
            stopMoving = true;
            dizzy = true;
            canAttack = false;
            playerInAttackRange = false;
            rb.velocity = Vector3.zero;
            SFX.PlaySound("Dizzy");
            gameObject.tag = "enemyDizzy";
            animator.SetBool("isJump",false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttack", false);
            animator.SetBool("isDizzy",true);
        }
        
    }
    public void Dead(){
        bool isDead = animator.GetBool("isDead");
        if(!isDead)
        {
            dead = true;
            gameObject.tag = "Dead";
            RespawnEnemy.aliveEnemies--;
            Debug.Log("Enemy dead");
            SFX.PlaySound("EnemyDead");
            animator.SetBool("isDizzy",false);
            animator.SetBool("isDead",true);
            StartCoroutine(DelayDead(3f));
        }
        
    }
    public void InstantDead()
    {
        gameObject.tag = "Dead";
        dead = true;
        gameObject.SetActive(false);
    }
    float CalculateHealth()
    {
        return health / maxHealth;
    }

    
    IEnumerator DelayDead(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        gameObject.tag = "enemy";
        dead = false;
        canAttack = true;
        maxHealth = 100f;
        health = maxHealth;
        slider.value = CalculateHealth();
        gameObject.transform.position = RespawnPos.position;
        gameObject.SetActive(true);
    }

    
    
}

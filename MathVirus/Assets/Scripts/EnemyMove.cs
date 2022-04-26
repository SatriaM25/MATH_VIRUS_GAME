using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    private float speed = 10.0f;
    public Rigidbody rb;
    private Animator animator;
    private Vector3 movement;
    public Transform playerPos;
    private Vector3 relativePos;
    public Collider bullet;

    // Handle Health
    public GameObject healthBarUI;
    public Slider slider;
    public float maxHealth;
    public float health;

    // Conditions
    private bool dizzy = false;
    private bool dead = false;
    private bool canAttack = true;
    private bool stopMoving = false;
    
    // Handle Attack
    public LayerMask whatIsPlayer;
    private bool playerInAttackRange;
    private float attackRange = 5f;

    //Handle Dizzy
    private float cooldown;

    // Handle Question
    public Text questionUI;
    private int answer = 10;


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
        slider.value = CalculateHealth();
        relativePos = playerPos.position - transform.position;
        if(canAttack){
            transform.LookAt(playerPos);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if(playerInAttackRange){
                Attack();
            }else {
                stopMoving = false;
            }
        }
    }
    void FixedUpdate(){
        if(!stopMoving){
            moveCharacter(relativePos);
        }
    }
    void moveCharacter(Vector3 direction){
        bool isRunning = animator.GetBool("isRunning");
        if(!isRunning){
            animator.SetBool("isAttack", false);
            animator.SetBool("isRunning", true);
            Debug.Log("Running animation executed");
        }
        direction.Normalize();
        rb.velocity = direction * speed;
        // rb.velocity = Vector3.zero;
    }
    void OnTriggerEnter(Collider other) {
        if(other.tag == "bullet"){
            health -= 10f;
            if(health <= 0f)
            {
               Dizzy();
            }
        }
    }
    void Attack(){
        stopMoving = true;
        rb.velocity = Vector3.zero;
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttack", true);
    }
    void Dizzy(){
        stopMoving = true;
        canAttack = false;
        playerInAttackRange = false;
        rb.velocity = Vector3.zero;
        gameObject.tag = "enemyDizzy";
        CreateQuestion();
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttack", false);
        animator.SetBool("isDizzy",true);
    }
    public void Dead(){
        animator.SetBool("isDizzy",false);
        animator.SetBool("isDead",true);
        gameObject.SetActive(false);
    }
    float CalculateHealth()
    {
        return health / maxHealth;
    }

    void CreateQuestion()
    {
        questionUI.text = "" + answer;
    }
}

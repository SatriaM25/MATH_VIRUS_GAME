using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour
{
    // References
    BulletPool bPool;
    public Transform fpCamera;
    public Transform firePoint;
    public GameObject answerButton;
    [SerializeField] private Animator anim;

    // Gun settings
    public float firePower = 10;

    // State
    public bool isShooting;
    public float fireDelay;
    public float fireTimer;
    // Start is called before the first frame update
    void Start()
    {
        bPool = BulletPool.main;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Ray checkingRay = new Ray(transform.position, forward);

        Debug.DrawRay(transform.position,forward * 5f);

        if(Physics.Raycast(checkingRay, out hit, 5f))
        {
            if(hit.collider.tag == "enemyDizzy")
            {
                bool activated = answerButton.activeSelf;
                if(!activated)
                {
                    answerButton.SetActive(true);
                }
            }
        }
        else
        {
            bool activated = answerButton.activeSelf;
            if(activated)
            {
                answerButton.SetActive(false);
            }
        }
        // Check if the player is trying to shoot
        if (isShooting){
            // If the player has recently shot a bullet -> cooldown
            if (fireTimer > 0) fireTimer -= Time.deltaTime;

            // When the cooldown is over shoot again
            else{
                // Reset cooldown timer
                fireTimer = fireDelay;

                Shoot();
            }
        }
    }

    public void Shoot(){
        // Calculate bullet velocity
        Vector3 bulletVelocity = fpCamera.forward * firePower;
        SFX.PlaySound("Fire");

        //Pick (spawn) bullet from pool
        bPool.PickFromPool(firePoint.position, bulletVelocity);
    }

    public void PullTrigger()
    {
        anim.SetBool("isShooting", true);
        // "Full auto"
        if (fireDelay > 0) isShooting = true;

        // "Semi auto"
        else Shoot();
    }

    public void ReleaseTrigger()
    {
        anim.SetBool("isShooting", false);
        // Stop shooting
        isShooting = false;

        // Set cooldown timer to zero to immediately shoot on next press
        fireTimer = 0;
    }

    
}

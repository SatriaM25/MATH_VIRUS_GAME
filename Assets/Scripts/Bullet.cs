using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    // Rigidbody compnent reference
    public Rigidbody rbody;

    // Prevent the bullet from never deactivating if nothing is hit
    public float lifeTime;
    public void Activate(Vector3 position, Vector3 velocity){
        // Set position and movement velocity
        transform.position = position;
        rbody.velocity = velocity;

        // Activate the GameObject
        gameObject.SetActive(true);

        // Start decay coroutine
        StartCoroutine("Decay");
    }

    private IEnumerator Decay(){
        // Decay after lifeTime seconds
        yield return new WaitForSeconds(lifeTime);

        Deactivate();
    }

    public void Deactivate(){
        // Put the bullet back into the pool
        BulletPool.main.AddToPool(this);
        // Stop all coroutines to prevent errors
        StopAllCoroutines();
        // Deactivate the GameObject
        gameObject.SetActive(false);
    }

    // OnCollisionEnter can also be used
    public void OnTriggerEnter(Collider other){
        // Here is where you'd put the code to handle bullet hits
        //  Debug.Log("A bullet hit something");
        //....

        // After hitting anything just deactivate the bullet
        Deactivate();
    }
}

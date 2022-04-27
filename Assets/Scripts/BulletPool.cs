using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    // Singleton reference
    public static BulletPool main;
    //Settings
    public GameObject bulletPrefab;
    public int poolSize = 100;
    
    private List<Bullet> availableBullets;

    private void Awake()
    {
        //Initialize singleton
        main = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        availableBullets = new List<Bullet>();

        for(int i = 0; i< poolSize; i++){
            // Instantiate bullet clone
            Bullet b = Instantiate(bulletPrefab, transform).GetComponent<Bullet>();
            b.gameObject.SetActive(false);

            // Add it to the pool
            availableBullets.Add(b);
        }
    }

    public void PickFromPool(Vector3 position, Vector3 velocity){
        // Prevent errors
        if (availableBullets.Count < 1) return;

        // Activate the bullet
        availableBullets[0].Activate(position, velocity);

        // Pop it from the list
        availableBullets.RemoveAt(0);
    }

    public void AddToPool(Bullet bullet){
        // Add the bullet (back) to the pool
        if (!availableBullets.Contains(bullet)) availableBullets.Add(bullet);
    }
}

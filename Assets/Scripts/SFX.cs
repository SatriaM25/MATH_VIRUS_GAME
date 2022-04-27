using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public static AudioClip GunSound, EnemyDead, Stun, Hit;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        GunSound = Resources.Load<AudioClip> ("gunshot");
        EnemyDead = Resources.Load<AudioClip> ("dead monster sound");
        Stun = Resources.Load<AudioClip> ("zombie stun");
        Hit = Resources.Load<AudioClip> ("hit sound");

        audioSrc = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip){
            case "EnemyDead":
                audioSrc.PlayOneShot(EnemyDead);
                break;
            case "Fire":
                audioSrc.PlayOneShot(GunSound);
                break;
            case "Dizzy":
                audioSrc.PlayOneShot(Stun);
                break;
            case "Hit":
                audioSrc.PlayOneShot(Hit);
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBacksound : MonoBehaviour
{
    private static AudioBacksound backgroundmusic;

    void Awake()
    {
        if(backgroundmusic == null)
        {
            backgroundmusic = this;
            DontDestroyOnLoad(backgroundmusic);
        }

        else
        {
            Destroy(gameObject);
        }
    }
}

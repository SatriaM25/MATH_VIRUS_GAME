using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnEnemy : MonoBehaviour
{
    [SerializeField] private EnemyMove[] enemy;
    List<int> select;
    [SerializeField] private Text WaveUI;
    [SerializeField] private SceneLoader1 sceneManager;
    public int wave = 0;
    public static int aliveEnemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(aliveEnemies <= 0)
        {
            wave++;
            WaveUI.text = "WAVE " + wave;
            aliveEnemies = wave;
            RandomRespawn(wave);
        }
    }
    
    void RandomRespawn(int count)
    {
        ActivableEnemy();
        for(int i=0; i<count; i++)
        {
            int rand = Random.Range(0,10);
            enemy[select[rand]].Respawn();
            Debug.Log("respawn enemy " + select[rand]);
            select.RemoveAt(rand);
        }
    }
    void ActivableEnemy()
    {
        select = new List<int>(){0,1,2,3,4,5,6,7,8,9};
    }
    public void Reset()
    {
        wave = 0;
        aliveEnemies = wave;
        for(int i = 0; i<enemy.Length; i++)
        {
            if(!enemy[i].dead)
            {
                enemy[i].InstantDead();
            }
        }
    }
}

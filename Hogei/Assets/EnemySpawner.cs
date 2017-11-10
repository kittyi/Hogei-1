using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public int Level = 4; //public for testing
    public GameObject[] Enemy;
    public Transform[] EnemySpawnPoints;
    

	// Use this for initialization
	void Start () {
        SpawnEnemies();
	}
	
	// Update is called once per frame
	void Update () {

    }

    void SpawnEnemies()
    {
        int EnemyIndex = Random.Range(0, Enemy.Length);
   
        for (int i = 0; i < Level; i++)
        {
            Instantiate(Enemy[Random.Range(0, Enemy.Length)], EnemySpawnPoints[i].position, Quaternion.identity);
        }
    }
}

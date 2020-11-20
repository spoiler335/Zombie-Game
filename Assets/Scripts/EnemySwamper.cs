using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwamper : MonoBehaviour
{
    public GameObject zomprefab;
    [HideInInspector] public Transform[] enemyswampoints;
    public float spawnDuration = 5f;
    void Start()
    {
        enemyswampoints = new Transform[transform.childCount];
        for(int i=0;i<transform.childCount;i++)
        {
            enemyswampoints[i] = transform.GetChild(i);
        }
        StartCoroutine(CoStartSpawning());
    }

  IEnumerator CoStartSpawning()
    {
        while(true)
        {
            for(int i=0;i<enemyswampoints.Length;i++)
            {
                Transform enemyswampoint = enemyswampoints[i];
                Instantiate(zomprefab, enemyswampoint.position, enemyswampoint.rotation);
            }
            yield return new WaitForSeconds(spawnDuration);
        }
    }


    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSpaning : MonoBehaviour
{
    private float Timer = 0f;
    public GameObject Spawnpoint1;
    public GameObject Spawnpoint2;
    public GameObject Spawnpoint3;


    void Start()
    {
        Spawnpoint1.SetActive(true);
        Spawnpoint2.SetActive(false);
        Spawnpoint3.SetActive(false);
    }

    
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("One"))
        {
            Spawnpoint1.SetActive(true);
        }
        if (other.gameObject.CompareTag("Two"))
        {
            Spawnpoint2.SetActive(true);
        }
        if (other.gameObject.CompareTag("Three"))
        {
            Spawnpoint3.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("One"))
        {
            Spawnpoint1.SetActive(false);
            Destroy(Spawnpoint1);
        }

        if(other.gameObject.CompareTag("Two"))
        {
            Spawnpoint2.SetActive(false);
            Destroy(Spawnpoint2);
        }

        if (other.gameObject.CompareTag("Three"))
        {
            Spawnpoint3.SetActive(false);
            Destroy(Spawnpoint3);
        }
    }
}

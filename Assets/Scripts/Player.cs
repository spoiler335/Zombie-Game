using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Health health;
    [HideInInspector] public bool isDead = false;
    void Start()
    {
        health = GetComponent<Health>();
    }

  
    void Update()
    {
        CheckHealth();
    }

    void CheckHealth()
    {
        if (isDead) return;

        if (health.value <= 0)
        {
            isDead = true;
            print("Player Dead");

            Invoke("Restartgame", 2f);
        }
        }

    void Restartgame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

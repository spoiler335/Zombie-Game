using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Health health;
    public Text healthText;
    public Animator hitanimator;
 
    void Start()
    {
        health = GetComponent<Health>();
        health.onHit.AddListener(()=> {
            hitanimator.SetTrigger("Show");
            UpdadeHealthText();
        });
        UpdadeHealthText();
    }


    void Update()
    {
        
    }

    void UpdadeHealthText()
    {
        healthText.text = "Hp:" + health.value;
    }
}

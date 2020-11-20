using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private GameObject target;
    private NavMeshAgent agent;
    private Health health;
    [HideInInspector] public bool isDead = false;
    private Animator animator;
    private Collider collider;
    [HideInInspector] public bool isAttacking = false;
    public float speed = 1f;
    public float angularspeed = 120f;
    public float damage = 20f;
    private Health th;
    private Player player;
   


    void Start()
    {
        target = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        th = target.GetComponent<Health>();
        if (th == null)
        {
            throw new System.Exception("Target Doesnt Have Health Component");
        }

        player = target.GetComponent<Player>(); 
        if(player==null)
        {
            throw new System.Exception("No Player Component Found");
        }

       
    }


    void Update()
    {
        CheckHealth();
        Chase();
        CheckAttack();
    }

    void CheckHealth()
    {
        if (isDead) return;

        if(health.value<=0)
        {
            isDead = true;
            agent.isStopped = true;
            collider.enabled = false;
            animator.CrossFadeInFixedTime("Death", 0.1f);
            Destroy(gameObject, 1f);

        }
    }

    void Chase()
    {
        if (isDead) return;
        else if (player.isDead) return;
        agent.destination = target.transform.position;
    }

    void CheckAttack()
    {
        if (isDead) return;
        else if (isAttacking) return;
        else if (player.isDead) return;
        float distanceFromTarget = Vector3.Distance(target.transform.position, transform.position);
        if(distanceFromTarget<=1.8f)
        {
            Attack();
        }
    }

    void Attack()
    {
        th.TakeDamage(damage);
        agent.speed = 0;
        agent.angularSpeed = 0;
        isAttacking = true;
        animator.SetTrigger("ShouldAttack");

        Invoke("ResetAttacking", 1.5f);
    }

    void ResetAttacking()
    {
        isAttacking = false;
        agent.speed = speed;
        agent.angularSpeed = angularspeed;
    }

    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Controller player;
    Rigidbody r;
    [SerializeField] float attackdistance;
    [SerializeField] float MoveDistance;
    [SerializeField] float speed;
    public bool isAttacking = false;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Controller>();
        r = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlayerCloseEnough()){
            AttackPlayer();
        }else{
            if(IsPlayerInMovingDistance()){
                MoveToPlayer();
            }
        }
    }
    
    private void MoveToPlayer()
    {
        r.velocity = new Vector3(0,0,-1*speed);
    }

    private bool IsPlayerInMovingDistance()
    {
        return MoveDistance > Vector3.Distance(transform.position,player.transform.position);
    }

    private bool IsPlayerCloseEnough()
    {
        return attackdistance > Vector3.Distance(transform.position,player.transform.position);
    }
    private void AttackPlayer()
    {
        isAttacking = true;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            //KillPlayer(other);
        }
    }
    public void KillPlayer(Collision other)
    {
        if (!isAttacking) return;
        other.gameObject.GetComponent<Controller>().isEnabled = false;
        StartCoroutine(other.gameObject.GetComponent<Controller>().EndGame(0.2f));
    }
}

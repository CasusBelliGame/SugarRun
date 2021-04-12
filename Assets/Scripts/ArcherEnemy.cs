using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemy : MonoBehaviour
{
    [SerializeField] GameObject ThrowObjectPrefab;
    Controller player;
    Rigidbody r;
    [SerializeField] float attackdistance;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Controller>();
        r = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if(IsPlayerCloseEnough()){
            AttackPlayer();
        }     
    }


    private bool IsPlayerCloseEnough()
    {
        return attackdistance > Vector3.Distance(transform.position,player.transform.position);
    }

    private void AttackPlayer()
    {
        //start AttackAnimation -Instantiate ThrowWeapon
    }
    //Animation Event
    public void ThrowFrame(){
        GameObject projectile = Instantiate(ThrowObjectPrefab,transform.right,Quaternion.identity);
    }






}

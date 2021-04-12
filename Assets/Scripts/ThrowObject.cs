using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    Rigidbody r;
    Vector3 toWhere = new Vector3(0,0,0);
    [SerializeField]float speed;
    public bool isThrowing = true;
    void Start()
    {
        r = GetComponent<Rigidbody>();
        toWhere =  GameObject.FindWithTag("Player").transform.position- transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isThrowing){
            r.velocity = toWhere*speed;
        }

    }

    


}

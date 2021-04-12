using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SticktoFloor : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Floor"){
            //gameObject.GetComponent<Rigidbody>().useGravity = false;
            //gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}

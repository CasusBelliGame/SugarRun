using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SledgeHammer : MonoBehaviour
{
    [SerializeField] GameObject[] planes;
    public void StartAttack(int amount){
        if(amount <5){
            for (int i = 0; i < 5-amount; i++)
            {
                planes[i].SetActive(false);
            }
        }
    }

    public void Hit(){
        foreach (GameObject plane in planes)
        {
            plane.GetComponent<Slice>().sliceY = planes.Length;
            plane.GetComponent<Slice>().sliceX = planes.Length;
            plane.GetComponent<Slice>().sliceZ = planes.Length;
            plane.GetComponent<Slice>().SliceAttack();
        }
    }

    


}

using System.Collections;
using System.Collections.Generic;
using EzySlice;
using UnityEngine;

public class Slice : MonoBehaviour
{
    public Material material;
    public LayerMask layer;
    public float sliceY;
    public float sliceX;
    public float sliceZ;
    [SerializeField] float expPower;
    [SerializeField] private GameObject bloodEffect;

    private void Start() {
       
    }
    public void SliceAttack(){
        Collider[] objsToCut = Physics.OverlapBox(transform.position,new Vector3(2f,2f,2f),transform.rotation,layer);
        if(objsToCut == null) return;
        foreach (Collider objToCut in objsToCut)
        {
            if(objToCut.GetComponent<MeshRenderer>() != null){
                Material mat = objToCut.GetComponent<MeshRenderer>().material;
                material = mat;
            }else{
                Material matx = objToCut.GetComponent<SkinnedMeshRenderer>().material;
                if(matx != null) material = matx;
            }

            SlicedHull slicedObj = Cut(objToCut.gameObject,material);
            if (slicedObj == null) return;
            GameObject upperPart = slicedObj.CreateUpperHull(objToCut.gameObject,material);
            GameObject lowerPart = slicedObj.CreateLowerHull(objToCut.gameObject,material);
            AddToObject(upperPart);
            AddToObject(lowerPart);
            Instantiate(bloodEffect, (upperPart.transform.position - lowerPart.transform.position) / 2, Quaternion.identity);

            if (objToCut.transform.root.GetComponent<EnemyController>() != null)
            {
                Destroy(objToCut.transform.root.gameObject); 
            }
            else
            {
                Destroy(objToCut.gameObject); 
            }
            
        }
    }


    public SlicedHull Cut(GameObject obj, Material mat = null){
        return obj.Slice(transform.position,transform.up,mat);
    }

    void AddToObject(GameObject obj){
        //obj.layer = LayerMask.NameToLayer( "Cut" );
        obj.layer = LayerMask.NameToLayer( "Pass" );
        obj.AddComponent<MeshCollider>().convex = true;
        obj.AddComponent<SticktoFloor>();
        obj.AddComponent<Rigidbody>();
        obj.GetComponent<Rigidbody>().AddExplosionForce(expPower,transform.position,20);
        obj.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
        obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
    }
}

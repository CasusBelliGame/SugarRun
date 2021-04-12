using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Vector2 hitPos = new Vector2(0,0);
    [SerializeField] Vector2 currentPos = new Vector2(0,0); 
    [SerializeField] GameObject slicePlane;
    [SerializeField] GameObject sword;
    Rigidbody r;
    [SerializeField] float speed; 

    [SerializeField] bool isMoving = false;
    [SerializeField] bool isAttacking = false;
    float TimePassed;
    [SerializeField] float[] timesForAttacks;
    public bool isEnabled = true;

    //End Game parçaları
    int TapCount;
    [SerializeField] GameObject SledgeHammer;
    GameObject TapCountUI;
    GameObject EndGameUI;
    TapCountUI tapSlider;
    [SerializeField] bool isTapping;
    [SerializeField] GameObject[] hammerPlanes;
    private Slice _slice;
    [SerializeField] private GameObject effect1Handler;
    [SerializeField] private GameObject effect2Handler;
    private Material effect1;
    private Material effect2;

    void Start()
    {
        effect1 = effect1Handler.GetComponent<MeshRenderer>().material;
        effect2 = effect2Handler.GetComponent<MeshRenderer>().material;
        effect1Handler.GetComponent<MeshRenderer>().enabled = false;
        effect2Handler.GetComponent<MeshRenderer>().enabled = false;
        _slice = slicePlane.GetComponent<Slice>();
        r = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        TapCountUI = GameObject.FindWithTag("TapCountUI");
        tapSlider = TapCountUI.GetComponent<TapCountUI>();
        TapCountUI.SetActive(false);
        EndGameUI = GameObject.FindWithTag("EndGameUI");
        EndGameUI.SetActive(false);
        slicePlane.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if(InteractWithUI()){
            return;
        }
        Move();
        if(isTapping){
            if(Input.GetMouseButtonDown(0)){
                tapSlider.Increase(); 
                TapCount +=1;
            }

            return;
        }
        if(!isEnabled) return;
        if(isAttacking) return;
        if(Input.GetMouseButton(0)){
            Count();
            Time.timeScale = 0.28f;
        }
        if(Input.GetMouseButtonUp(0)){
            Attack(TimePassed);
            TimePassed = 0;
            Time.timeScale = 1f;
        }


    }



    private bool InteractWithUI()
    {
       if(EventSystem.current.IsPointerOverGameObject()){
           return true;
       }
       return false;
    }

    private void Move()
    {
        if(!isMoving) animator.SetTrigger("run");
        isMoving = true;
        r.velocity = new Vector3(0,0,speed);
    }
    private void Count()
    {
        TimePassed += Time.unscaledDeltaTime;
    }
    private void Attack(float time)
    {
        isMoving = false;
        isAttacking = true;
        if(time >= timesForAttacks[0]){
            animator.SetTrigger("swordattack");
        }else{
            animator.SetTrigger("swordattack");
        }
        _slice.sliceY = time;
        _slice.sliceX = time;
        _slice.sliceZ = time;
    }

    public void PlaneOpen()
    {
        slicePlane.SetActive(true);
    }
 //Animation Event
    public void SliceAttack(){

        slicePlane.GetComponent<Slice>().SliceAttack();
    }
    public void StopAttack(){
        slicePlane.SetActive(false);
        isAttacking = false;
        animator.SetTrigger("run");
        effect1Handler.GetComponent<MeshRenderer>().enabled = false;
        effect2Handler.GetComponent<MeshRenderer>().enabled = false;
    }

    public void HitFrame(){
        SledgeHammer.GetComponent<SledgeHammer>().Hit();
    }
    public void StopHammerHit(){
        StartCoroutine(EndGame(1.4f));
    }

    public void StartSwordEffects()
    {
        effect1Handler.GetComponent<MeshRenderer>().enabled = true;
        effect2Handler.GetComponent<MeshRenderer>().enabled = true;
        effect1.SetFloat("Vector1", 1.5f);
    }

    public IEnumerator EndGame(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Time.timeScale = 0;
        EndGameUI.SetActive(true);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "EndGameBuff"){
            TapCountUI.SetActive(true);
            sword.SetActive(false);
            isTapping = true;
            SledgeHammer.SetActive(true);
        }
        if(other.tag == "EndGame"){
            SledgeHammer.GetComponent<SledgeHammer>().StartAttack(TapCount);
            animator.SetTrigger("Hammer");
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "ThrowObject"){
            other.transform.parent = gameObject.transform;
            other.gameObject.GetComponent<Rigidbody>().Sleep();
            other.gameObject.GetComponent<ThrowObject>().isThrowing = false;
        }
    }



}

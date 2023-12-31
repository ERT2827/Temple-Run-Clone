using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{

    [Header("Player stats")]
    // [SerializeField] private int hp = 1;
    // public int currentScore;
    public bool isDead = false;

    // [SerializeField] private Text scoreCounter;
    
    
    [Header("Player Movement")]
    [SerializeField] private GameObject playerOBJ;
    [SerializeField] private float maxtravel = 2.5f;
    [SerializeField] private float sideSpeed;
    
    [SerializeField] private float jumpHeight = 2;
    [SerializeField] private float jumpTime = 2;
    bool upwards = true;
    public bool isJumping = false;

    public bool isSliding = false;

    public bool isBoosting = false;


    [SerializeField] private float ducktime = 2f;

    [SerializeField] private GameObject detector;
    groundChecker GS;

    [Header("Level Movement")]
    [SerializeField] private GameObject currentSection;
    [SerializeField] private GameObject nextSection;

    [SerializeField] private Transform sectionSpawn;
    [SerializeField] private GameObject sectionPref;

    [Header("Mobile controls")]
    private Vector2 startTouchPos;
    private Vector2 endTouchPos;
    Vector3 startGyr;
    Vector3 lastgyr;
    Vector3 gyrAng;
    [SerializeField] private float gyrMax = 40;
    // [SerializeField] private float gyrSensitivity = 2;

    float gyrOperator;
    float gyrOffset;

    bool maxOverflow = false;
    bool minOverflow = false; 

    // [Header("Gyro Debug")]
    // public Text gyrovals;

    public Animator anim;

    public float moveSpeed = 10f; // Adjust the value as needed

    [Header("Powerup Functionality")]
    private LevelGenerate level;
    [SerializeField] float boostDuration = 5f;
    [SerializeField] float boostSpeed = 10f;



    // Start is called before the first frame update
    void Start()
    {
        GS = detector.GetComponent<groundChecker>();
        level = GameObject.Find("levelmanager").GetComponent<LevelGenerate>();

        
        Input.gyro.enabled = true;

        gyrOperator = gyrMax / 1.7f;

        StartCoroutine(setGyro());
        Debug.Log(startGyr);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GS.isGrounded && !isJumping && !isBoosting)
        {
            Rigidbody RB = playerOBJ.GetComponent<Rigidbody>();
            
            RB.useGravity = true;
        }else if((GS.isGrounded && !isJumping && !isSliding) || isBoosting){
            Rigidbody RB = playerOBJ.GetComponent<Rigidbody>();
            
            RB.useGravity = false;

            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.003f, gameObject.transform.position.z);
            playerOBJ.transform.position = new Vector3(gameObject.transform.position.x, 1.08f, gameObject.transform.position.z);
        }else if((GS.isGrounded && !isJumping && isSliding) || isBoosting){
            Rigidbody RB = playerOBJ.GetComponent<Rigidbody>();
            
            RB.useGravity = false;

            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.003f, gameObject.transform.position.z);
            playerOBJ.transform.position = new Vector3(gameObject.transform.position.x, 0.5f, gameObject.transform.position.z);
        }
        
        #if UNITY_ANDROID

        //Mobile Controls
        if (Input.touchCount >0 && Input.GetTouch(0).phase == TouchPhase.Began){
            startTouchPos = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPos = Input.GetTouch(0).position;

            if (endTouchPos.y > startTouchPos.y){
                upwards = true;
                StartCoroutine(jumpTimer());
            }else if (endTouchPos.y < startTouchPos.y)
            {
                StartCoroutine(duck());
            }
        }

        //gyro calc
        gyrAng = Input.gyro.attitude.eulerAngles;
        
        // gyrovals.text = startGyr.x.ToString() + " X " + gyrAng.x.ToString() + " Offset " + gyrOffset.ToString();

        
        
        if (Mathf.Abs(gyrAng.y - startGyr.y) < gyrMax)
        {
            float deltaAngle;
            
            if(gyrAng.y < maxtravel && maxOverflow){
                float fakeAng = gyrAng.y + (360 - maxtravel);
                deltaAngle = fakeAng - gyrAng.y;
            }else if(gyrAng.y > (360 - maxtravel) && minOverflow){
                float fakeAng = gyrAng.y - (360 - maxtravel);
                deltaAngle = fakeAng - gyrAng.y;
            }else{
                deltaAngle = startGyr.y - gyrAng.y;
            }
            
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, deltaAngle / gyrOperator);


        }else if(Mathf.Abs(gyrAng.y - startGyr.y) > gyrMax && gameObject.transform.position.z > 0){
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 1.7f);
        }else if(Mathf.Abs(gyrAng.y - startGyr.y) > gyrMax && gameObject.transform.position.z < 0){
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1.7f);
        }else{
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
        }

        #endif

        #if UNITY_STANDALONE_WIN

        //PC Backup
        if (Input.GetMouseButtonDown(0)){
            startTouchPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endTouchPos = Input.mousePosition;

            if (endTouchPos.y > startTouchPos.y)
            {
                upwards = true;
                StartCoroutine(jumpTimer());
            }else if (endTouchPos.y < startTouchPos.y)
            {
                StartCoroutine(duck());
            }
        }
        
        if(Input.GetKey(KeyCode.S)){
            StartCoroutine(duck());
        }

        if(Input.GetAxisRaw("Horizontal") != 0){
            if(Input.GetAxisRaw("Horizontal") > 0){
                sideMove(false);
            }else{
                sideMove(true);
            }
        }

        if(Input.GetButton("Jump") && !isJumping){
            upwards = true;
            StartCoroutine(jumpTimer());
        }

        #endif

        #if UNITY_EDITOR

        //PC Backup
        if (Input.GetMouseButtonDown(0)){
            startTouchPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endTouchPos = Input.mousePosition;

            if (endTouchPos.y > startTouchPos.y)
            {
                upwards = true;
                StartCoroutine(jumpTimer());
            }else if (endTouchPos.y < startTouchPos.y)
            {
                StartCoroutine(duck());
            }
        }
        
        if(Input.GetKey(KeyCode.S)){
            StartCoroutine(duck());
        }

        if(Input.GetAxisRaw("Horizontal") != 0){
            if(Input.GetAxisRaw("Horizontal") > 0){
                sideMove(false);
            }else{
                sideMove(true);
            }
        }

        if(Input.GetButton("Jump") && !isJumping){
            upwards = true;
            StartCoroutine(jumpTimer());
        }

        #endif

        if(isJumping){
            jump();
        }

        anim.SetBool("slide", isSliding);
    }

    private void FixedUpdate() {
        // currentScore += 1;

        // scoreCounter.text = "Score: " + currentScore.ToString();
    }

    void sideMove(bool direction){
        if(direction == true && gameObject.transform.position.z < maxtravel){
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, (gameObject.transform.position.z + (Time.deltaTime * sideSpeed)));
        }else if(direction == false && gameObject.transform.position.z > -maxtravel){
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, (gameObject.transform.position.z - (Time.deltaTime * sideSpeed)));
        }
    }

    IEnumerator duck(){
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 0.5f, gameObject.transform.localScale.z);   
        isSliding = true;

        yield return new WaitForSeconds(ducktime);

        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 1f, gameObject.transform.localScale.z); 
        isSliding = false;  
    }

    void jump(){        
        if(upwards){
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (jumpHeight * Time.deltaTime), gameObject.transform.position.z);
        }else{
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - (jumpHeight * Time.deltaTime), gameObject.transform.position.z);
        }
    }

    IEnumerator jumpTimer(){
        isJumping = true;
        
        yield return new WaitForSeconds(jumpTime/2);
        upwards = false;

        yield return new WaitForSeconds(jumpTime/2);
        isJumping = false;
    }

    

    IEnumerator setGyro(){
        yield return new WaitForSeconds(0.1f);

        startGyr = Input.gyro.attitude.eulerAngles;

        if(startGyr.y + gyrMax > 360){
            maxOverflow = true;
        }else if(startGyr.y - gyrMax < 0){
            minOverflow = true;
        }
    }
    
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Level"))
    //     {
    //         hp = 0;
    //     }
    // }

    public void startboost(){
        if (!isBoosting)
        {
            StartCoroutine(ActivateBoost());
        }
    }

    IEnumerator ActivateBoost()
    {
        isBoosting = true;

        Debug.Log("REAL!");
        
        float originalSpeed = level.runSpeed;
        playerController playerC = GameObject.Find("player").GetComponent<playerController>();
        var playerRend = GameObject.Find("playerOBJ").GetComponent<Renderer>();

        level.runSpeed = originalSpeed + boostSpeed;
        playerC.isBoosting = true;
        playerC.isSliding = true;

        playerRend.material.SetColor("_Color", Color.green);

        yield return new WaitForSeconds(boostDuration);

        level.runSpeed = originalSpeed;
        playerC.isBoosting = false;
        playerC.isSliding = false;
        playerC.isJumping = false;

        playerRend.material.SetColor("_Color", Color.white);
        isBoosting = false;
    }
 }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private GameObject playerOBJ;
    [SerializeField] private float maxtravel = 2.5f;
    [SerializeField] private float sideSpeed;

    [SerializeField] private float runSpeed = 3;

    [SerializeField] private float jumpHeight = 2;
    [SerializeField] private float jumpTime = 2;
    bool upwards = true;
    bool isJumping = false;

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
    [SerializeField] private float gyrMax = 60;
    // [SerializeField] private float gyrSensitivity = 2;

    float gyrOperator;

    [Header("Gyro Debug")]
    public Text gyrovals;

    
    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;

        gyrOperator = gyrMax / 2.2f;

        StartCoroutine(setGyro());
        // Debug.Log(startGyr);
    }

    // Update is called once per frame
    void Update()
    {
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
                duck();
            }
        }
        
        gyrovals.text = startGyr.x.ToString() + " X " + gyrAng.x.ToString();


        gyrAng = Input.gyro.attitude.eulerAngles;
        
        if (Mathf.Abs(gyrAng.x - startGyr.x) < gyrMax)
        {
            float deltaAngle = startGyr.x - gyrAng.x;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, deltaAngle / gyrOperator);


        }else if(Mathf.Abs(gyrAng.x - startGyr.x) > gyrMax && gameObject.transform.position.z > 0){
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 2.2f);
        }else if(Mathf.Abs(gyrAng.x - startGyr.x) > gyrMax && gameObject.transform.position.z < 0){
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -2.2f);
        }else{
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
        }

        //PC Backup
        // if (Input.GetMouseButtonDown(0)){
        //     startTouchPos = Input.mousePosition;
        // }

        // if (Input.GetMouseButtonUp(0))
        // {
        //     endTouchPos = Input.mousePosition;

        //     if (endTouchPos.y > startTouchPos.y)
        //     {
        //         upwards = true;
        //         StartCoroutine(jumpTimer());
        //     }else if (endTouchPos.y < startTouchPos.y)
        //     {
        //         duck();
        //     }
        // }
        
        // if(Input.GetKey(KeyCode.S)){
        //     duck();
        // }else{
        //     gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 1f, gameObject.transform.localScale.z);   

        // }

        // if(Input.GetAxisRaw("Horizontal") != 0){
        //     if(Input.GetAxisRaw("Horizontal") > 0){
        //         sideMove(false);
        //     }else{
        //         sideMove(true);
        //     }
        // }

        // if(Input.GetButton("Jump") && !isJumping){
        //     upwards = true;
        //     StartCoroutine(jumpTimer());
        // }

        if(isJumping){
            jump();
        }
    }

    void sideMove(bool direction){
        if(direction == true && gameObject.transform.position.z < maxtravel){
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, (gameObject.transform.position.z + (Time.deltaTime * sideSpeed)));
        }else if(direction == false && gameObject.transform.position.z > -maxtravel){
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, (gameObject.transform.position.z - (Time.deltaTime * sideSpeed)));
        }
    }

    void duck(){
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 0.5f, gameObject.transform.localScale.z);   
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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Level"))
        {
            hp = 0;
        }
    }
}

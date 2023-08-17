using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Touch Controls")]
    private Vector2 startTouchPos;
    private Vector2 endTouchPos;

    Vector3 lastgyr;
    Vector3 gyrAng;
    [SerializeField] private float gyrSensitivity = 2;

    
    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //This code controls the movement of the floor.
        //It's supposed to be expanded upon
        //Contact me if you need help with it.
        
        if(nextSection == null){
            nextSection = Instantiate(sectionPref, sectionSpawn);
        }else if(currentSection.transform.position.x < -26){
            Destroy(currentSection);
            currentSection = nextSection;
            nextSection = Instantiate(sectionPref, sectionSpawn);
        }else{
            currentSection.transform.position = new Vector3(currentSection.transform.position.x - (runSpeed * Time.deltaTime), currentSection.transform.position.y, currentSection.transform.position.z);  
            nextSection.transform.position = new Vector3(nextSection.transform.position.x - (runSpeed * Time.deltaTime), nextSection.transform.position.y, nextSection.transform.position.z);   
 
        }

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

        gyrAng = Input.gyro.attitude.eulerAngles;

        if(Mathf.Abs(gyrAng.x - lastgyr.x) > gyrSensitivity){
            if(gyrAng.x > lastgyr.x){
                sideMove(false);
                lastgyr = gyrAng;
            }else{
                sideMove(true);
                lastgyr = gyrAng;
            }
        }


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
}

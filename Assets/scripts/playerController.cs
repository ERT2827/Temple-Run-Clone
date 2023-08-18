using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private GameObject playerOBJ;
    [SerializeField] private float maxtravel = 2.5f;
    [SerializeField] private float sideSpeed;


    [SerializeField] private float jumpHeight = 2;
    [SerializeField] private float jumpTime = 2;
    bool upwards = true;
    bool isJumping = false;

    int hp = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetKey(KeyCode.S)){
            duck();
        }else{
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 1f, gameObject.transform.localScale.z);   

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Level"))
        {
            hp = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathScript : MonoBehaviour
{
    
    playerController playerC;
    [SerializeField] private GameObject playerBase;
    
    // Start is called before the first frame update
    void Start()
    {
        playerC = playerBase.GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

private void OnTriggerEnter(Collider other) {
        // Debug.Log(other);

        obstacleType obstaType = other.gameObject.GetComponent<obstacleType>();

        if(obstaType != null){
            if(obstaType.Type == obstType.fall){
                death();
            }else if (obstaType.Type == obstType.jump && !playerC.isJumping)
            {
                death();
            }else if (obstaType.Type == obstType.duck && !playerC.isSliding)
            {
                death();
            }else if (obstaType.Type == obstType.sidestep)
            {
                sidestepFail();
            }
        }
    }

    public void death(){
        playerC.isDead = true;
        
        // PlayerPrefs.SetInt("endScore", playerC.currentScore);

        SceneManager.LoadScene("endscreen", LoadSceneMode.Single);
        
        Debug.Log("Dead");

    }
    
    void slowDown(){
        Debug.Log("Slowed");
    }

    void sidestepFail(){
        if (Physics.Raycast(gameObject.transform.position, transform.TransformDirection(Vector3.forward), .1f))
        {
            death();
        }else{
            slowDown();
        }
    }
}

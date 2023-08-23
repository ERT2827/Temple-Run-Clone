using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathScript : MonoBehaviour
{
    
    playerController playerC;
    [SerializeField] private GameObject playerBase;

    beastcontroller beastC;
    [SerializeField] private GameObject beastBase;
    
    // Start is called before the first frame update
    void Start()
    {
        playerC = playerBase.GetComponent<playerController>();
        beastC = beastBase.GetComponent<beastcontroller>();
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
            }else if (obstaType.Type == obstType.trip)
            {
                trip();
            }
        }
    }

    public void death(){
        playerC.isDead = true;
        
        // PlayerPrefs.SetInt("endScore", playerC.currentScore);

        SceneManager.LoadScene("endscreen", LoadSceneMode.Single);
        
        Debug.Log("Dead");

    }
    
    void trip(){
        if(beastC.areAttacking){
            death();
        }else{
            StartCoroutine(beastC.beastAttack());
        }
    }

}

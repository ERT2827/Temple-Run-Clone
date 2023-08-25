using System.Collections;
using UnityEngine;

public class BoostPowerUp : MonoBehaviour
{
    
    // public float boostDistance = 10f;


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController playerC = other.transform.parent.GetComponent<playerController>();
            playerC.startboost();
            gameObject.SetActive(false);
        }
    }

    

    

}

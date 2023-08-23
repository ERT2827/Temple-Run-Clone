using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundChecker : MonoBehaviour
{
    public bool isGrounded = true;

    List<GameObject> groundPieces = new List<GameObject>();

    private void Update() {
        int grounds = 0;

        // Debug.Log(isGrounded);

        foreach (GameObject i in groundPieces)
        {
            if(i.tag == "ground"){
                grounds += 1;
            }
        }

        if(grounds > 0){
            isGrounded = true;
        }else if(grounds == 0){
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        groundPieces.Add(other.gameObject);
        Debug.Log("enter");
    }

    private void OnTriggerExit(Collider other) {
        groundPieces.Remove(other.gameObject);
        Debug.Log("exit");
    }
}

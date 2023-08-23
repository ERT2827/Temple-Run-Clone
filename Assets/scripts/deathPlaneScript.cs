using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathPlaneScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        deathScript DS = other.GetComponent<deathScript>();

        if(DS != null){
            DS.death();
        }
    }
}

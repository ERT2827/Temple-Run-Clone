using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollecter : MonoBehaviour
{

    Ui ui;

    private void Start()
    {
        ui = GameObject.Find("UI").GetComponent<Ui>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collectcoin();
        }
    }

    public void Collectcoin()
    {
        ui.coins++;
        Destroy(gameObject);
    }

   
    
}

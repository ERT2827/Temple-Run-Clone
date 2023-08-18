using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gyroDebug : MonoBehaviour
{
    public Text gyrovals;
    Vector3 startGyr;


    private void Start() {
        // Input.gyro.enabled = true;
        
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 gyrAng = Input.gyro.attitude.eulerAngles;

        gyrovals.text = startGyr.x.ToString() + " X " + gyrAng.x.ToString();
    }
}

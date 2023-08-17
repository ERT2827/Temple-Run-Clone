using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gyroDebug : MonoBehaviour
{
    public Text gyrovals;

    private void Start() {
        Input.gyro.enabled = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 gyrAng = Input.gyro.attitude.eulerAngles;

        gyrovals.text = "X " + gyrAng.x.ToString() + " Y " + gyrAng.y.ToString() + " Z " + gyrAng.z.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum obstType
{
    duck,
    jump,
    turn,
    sidestep
}

public class obstacleType : MonoBehaviour{
    public obstType thisType;
}
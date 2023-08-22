using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum obstType
{
    duck,
    jump,
    turn,
    sidestep,
    fall
}

public class obstacleType : MonoBehaviour{
    public obstType Type;
}
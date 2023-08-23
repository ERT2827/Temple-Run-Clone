using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBooster : MonoBehaviour
{
    LevelGenerate level;
    public float speedMultiplier = 2f;
    public float powerUpDuration = 10f;

    private void Start()
    {
        level = GameObject.Find("levelmanager").GetComponent<LevelGenerate>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplySpeedBoost(speedMultiplier);
            StartCoroutine(DeactivateSpeedBoost());
        }
    }


    private IEnumerator DeactivateSpeedBoost()
    {
        yield return new WaitForSeconds(powerUpDuration);
        ResetSpeed();

    }

    public void ApplySpeedBoost(float multiplier)
    {
        level.runSpeed *= multiplier;
    }

    public void ResetSpeed()
    {
        level.runSpeed = 20;
    }
}

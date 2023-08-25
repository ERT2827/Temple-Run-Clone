using System.Collections;
using UnityEngine;

public class BoostPowerUp : MonoBehaviour
{
    public float boostDuration = 5f;
    public float boostSpeed = 20f;
    public float boostDistance = 10f;

    private bool boostActivated = false;
    private LevelGenerate level;

    private void Start()
    {
        level = GameObject.Find("levelmanager").GetComponent<LevelGenerate>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyPowerUp(level);
            gameObject.SetActive(false);
        }
    }

    public void ApplyPowerUp(LevelGenerate levelScript)
    {
        if (!boostActivated)
        {
            boostActivated = true;
            StartCoroutine(ActivateBoost(levelScript));
        }
    }

    public IEnumerator ActivateBoost(LevelGenerate levelScript)
    {
        float originalSpeed = levelScript.runSpeed;
        Rigidbody playerRigidbody = levelScript.GetComponent<Rigidbody>();

        levelScript.runSpeed = boostSpeed;
        playerRigidbody.useGravity = false;

        Vector3 boostEndPosition = levelScript.transform.position + Vector3.forward * boostDistance;

        float startTime = Time.time;
        while (Time.time - startTime < boostDuration)
        {
            float progress = (Time.time - startTime) / boostDuration;
            levelScript.transform.position = Vector3.Lerp(levelScript.transform.position, boostEndPosition, progress);
            yield return null;
        }

        // Gradually interpolate speed back to original speed over a cooldown period
        float cooldownStartTime = Time.time;
        while (Time.time - cooldownStartTime < boostDuration)
        {
            float cooldownProgress = (Time.time - cooldownStartTime) / boostDuration;
            levelScript.runSpeed = Mathf.Lerp(boostSpeed, originalSpeed, cooldownProgress);
            yield return null;
        }

        levelScript.runSpeed = originalSpeed; // Ensure the speed is set back to the original speed
        playerRigidbody.useGravity = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerate : MonoBehaviour
{
    [Header("Level Movement")]
    [SerializeField] private GameObject currentSection;
    [SerializeField] private GameObject nextSection;


    [SerializeField] private float runSpeed = 3;

    [SerializeField] private Transform sectionSpawn;
    [SerializeField] private GameObject[] sectionPref;

    [SerializeField]
    int numberOfPlainPlanes = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //This code controls the movement of the floor.
        //It's supposed to be expanded upon
        //Contact me if you need help with it.

        if (nextSection == null)
        {
           // int rand = Random.Range(0, 3);
            nextSection = Instantiate(sectionPref[3], sectionSpawn);
        }
        else if (currentSection.transform.position.x < -26)
        {
            int rand;
            Destroy(currentSection);
            currentSection = nextSection;

            if (numberOfPlainPlanes == 1)
            {
                rand = 3;
                nextSection = Instantiate(sectionPref[rand], sectionSpawn);
                numberOfPlainPlanes = 0;
            } else
            {
                do
                {
                    rand = Random.Range(0, 5);
                } while (rand == 3);
                nextSection = Instantiate(sectionPref[rand], sectionSpawn);
                numberOfPlainPlanes++;
            }
            
        }
        else
        {
            currentSection.transform.position = new Vector3(currentSection.transform.position.x - (runSpeed * Time.deltaTime), currentSection.transform.position.y, currentSection.transform.position.z);
            nextSection.transform.position = new Vector3(nextSection.transform.position.x - (runSpeed * Time.deltaTime), nextSection.transform.position.y, nextSection.transform.position.z);

        }


    }
}

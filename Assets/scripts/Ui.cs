using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour
{
    playerController player;

    public int score;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player").GetComponent<playerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }
}

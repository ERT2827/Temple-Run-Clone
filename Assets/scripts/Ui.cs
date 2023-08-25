using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour
{
    playerController player;

    public int score;
    public int multiplyer = 1;
    public Text scoreText;

    public Text coinsText;
    public int coins = 0;
    int coinMult = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player").GetComponent<playerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        score++;

        int functionalScore = score * multiplyer;
        scoreText.text = "Score: " + functionalScore.ToString() + "\nMultiplyer: " + multiplyer.ToString();

        coinsText.text = "Coin: " + coins.ToString();

        if(coins - coinMult >= 10){
            coinMult = coins;
            multiplyer += 1;
        }
    }
}

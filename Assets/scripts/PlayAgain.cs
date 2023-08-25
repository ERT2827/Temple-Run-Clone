using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayAgain : MonoBehaviour
{
    [SerializeField] private Text results;
    
    // Start is called before the first frame update
    void Start()
    {
        int lastScore;
        int highScore;

        lastScore = PlayerPrefs.GetInt("endScore");
        highScore = PlayerPrefs.GetInt("highScore");
        
        if(lastScore > highScore){
            results.text = "NEW HIGH SCORE \n" + lastScore.ToString();
            PlayerPrefs.SetInt("highScore", lastScore);
        }else{
            results.text = "Your Score: " + lastScore.ToString() + "\n High Score " + highScore.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadPlayScene()
    {
        SceneManager.LoadScene("samplescene", LoadSceneMode.Single);
    }
}

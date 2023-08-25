using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public Animator anim;

    public void Play()
    {
        StartCoroutine(LoadPlayScene());
    }

     IEnumerator LoadPlayScene()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(1);
    }
}

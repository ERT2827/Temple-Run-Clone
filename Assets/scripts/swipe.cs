using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swipe : MonoBehaviour
{
    public Sprite pageIMG;
    public List<Sprite> Numbers;
    
    Vector2 startTouchPos;
    Vector2 endTouchPos;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.touchCount >0 && Input.GetTouch(0).phase == TouchPhase.Began){
        //     startTouchPos = Input.GetTouch(0).position;
        // }

        // if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        // {
        //     endTouchPos = Input.GetTouch(0).position;

        //     if (true)
        //     {
                
        //     }
        // }
    }
}

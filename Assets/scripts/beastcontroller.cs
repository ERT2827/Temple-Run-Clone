using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class beastcontroller : MonoBehaviour
{
    Animator anim;

    public bool areAttacking;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        StartCoroutine(startrun());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator beastAttack(){
        anim.SetBool("isAttacking", true);
        areAttacking = true;

        yield return new WaitForSeconds(3f);

        areAttacking = false;
        anim.SetBool("isAttacking", false);
    }

    IEnumerator startrun(){
        yield return new WaitForSeconds(4f);

        anim.SetBool("isAttacking", false);
    }
}

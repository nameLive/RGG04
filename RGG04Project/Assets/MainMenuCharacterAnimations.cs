using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCharacterAnimations : MonoBehaviour {

    Animator animator;

    bool run;
    
    void Start() {

        animator = GetComponent<Animator>();
        
        StartCoroutine(DelayToStartAnAnimation(7f, "IsBeingChased", true));
    }

    //--------------------------------------

    IEnumerator DelayToStartAnAnimation(float delay, string animation, bool Start) {

        yield return new WaitForSeconds(delay);

        run = true;

        animator.SetBool(animation, Start);
    }

    void Update() {

        if (run)
            transform.Translate(new Vector3(7.5f * Time.deltaTime, 0, 0), Space.World);
    }
}

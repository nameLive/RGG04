using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BootMenu : MonoBehaviour {

    GameManager gameManager;

    public Image FGLogo;
    
    public float LogoFadeInLength;
    
    public float LogoStayLength;
    
    public float LogoFadeOutLength;


    //-------------------------------

    void Start() {

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        StartCoroutine(FadeInLogo());
    }

    //-------------------------------
    // Fades in FG Logo

    IEnumerator FadeInLogo() {

        FGLogo.CrossFadeAlpha(255, LogoFadeInLength, true);

        yield return new WaitForSeconds(LogoFadeInLength);

        StartCoroutine(FadeOutLogo());
    }

    //-------------------------------
    // Stays on logo for abit, then Fades Out Logo then calls BootMenuFinished

    IEnumerator FadeOutLogo() {

        yield return new WaitForSeconds(LogoStayLength);

        FGLogo.CrossFadeAlpha(0, LogoFadeOutLength, true);

        yield return new WaitForSeconds(LogoFadeOutLength);

        gameManager.BootMenuFinished();
    }
}

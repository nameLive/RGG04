using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour, PauseInterface
{

    public void Paused() {

        Time.timeScale = 0;
    }

    public void UnPaused() {

        Time.timeScale = 1;
    }
}

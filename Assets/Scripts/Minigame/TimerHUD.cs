using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerHUD : MonoBehaviour {

    [SerializeField]
    private Timer timer;
    [SerializeField]
    private Text timerUI;
	
	void Update () {
        timerUI.text = "Time: " + timer.currDelay.ToString("0.0");
	}
}

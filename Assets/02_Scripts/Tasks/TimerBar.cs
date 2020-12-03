using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    Image timerBar;
    public float maxTime;
    float timeLeft;

    private void Start()
    {
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
    }

    private void Update()
    {
        if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }
    }
}

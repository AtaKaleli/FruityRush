using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameTimer;

    // Update is called once per frame
    void Update()
    {
        gameTimer.text = "Timer: " + GameManager.instance.timer.ToString("#,#");
    }
}

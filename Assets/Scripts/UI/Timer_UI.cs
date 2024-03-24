using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameTimer;
    [SerializeField] private TextMeshProUGUI collectedFruits;

    // Update is called once per frame
    void Update()
    {
        gameTimer.text = "Timer: " + GameManager.instance.timer.ToString("#,#");
        collectedFruits.text = PlayerManager.instance.collectedFruits.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ingame_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameTimer;
    [SerializeField] private TextMeshProUGUI collectedFruits;

    private void Start()
    {
        GameManager.instance.levelNumber = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer.text = "Timer: " + GameManager.instance.timer.ToString("#,#");
        collectedFruits.text = PlayerManager.instance.collectedFruits.ToString();
    }
}

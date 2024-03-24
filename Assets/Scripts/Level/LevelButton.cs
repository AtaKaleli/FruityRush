using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelName;
    [SerializeField] private TextMeshProUGUI bestTime;
    [SerializeField] private TextMeshProUGUI collectedFruits;
    [SerializeField] private TextMeshProUGUI allFruits;



    public void DisplayLevelInfo(int levelNumber)
    {
        levelName.text = "Level " + levelNumber;
        bestTime.text = "Best time: " + PlayerPrefs.GetFloat("Level " + GameManager.instance.levelNumber + "BestTime", 999).ToString("#0.00") + " sec";
        collectedFruits.text = PlayerPrefs.GetInt("Level" + levelNumber + "FruitsCollected").ToString();
        allFruits.text = " / " + PlayerPrefs.GetInt("Level" + GameManager.instance.levelNumber + "TotalFruits").ToString();
    }
    
}

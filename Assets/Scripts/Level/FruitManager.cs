using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [SerializeField] private Transform[] fruitPosition;
    [SerializeField] private GameObject fruitPrefab;
    private int fruitIndex;

    private void Start()
    {
        fruitPosition = GetComponentsInChildren<Transform>();
        int levelNumber = GameManager.instance.levelNumber;
        
        for (int i = 1; i < fruitPosition.Length; i++)
        {
            GameObject newFruit = Instantiate(fruitPrefab, fruitPosition[i]);

            if (fruitIndex == Enum.GetNames(typeof(FruitType)).Length)
            {
                fruitIndex = 0;
            }
            newFruit.GetComponent<Fruit_Item>().FruitSetup(fruitIndex);
            fruitIndex++;

            fruitPosition[i].GetComponent<SpriteRenderer>().sprite = null;

            
        }


        int totalAmountOfFruits = PlayerPrefs.GetInt("Level" + levelNumber + "TotalFruits");

        if (totalAmountOfFruits != fruitPosition.Length - 1)
            PlayerPrefs.SetInt("Level" + levelNumber + "TotalFruits", fruitPosition.Length - 1);
    }

}

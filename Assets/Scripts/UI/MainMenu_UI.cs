using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_UI : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;

    private void Start()
    {
        bool isNewGame = PlayerPrefs.GetInt("Level" + 2 + "Unlocked") == 1;
        continueButton.SetActive(isNewGame);
        Time.timeScale = 1;
    }
    public void SwitchMenuTo(GameObject uiMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false); // switch off everything
        }

        uiMenu.SetActive(true); // switch on what we need
    }


    public void SetGameDifficuly(int i)
    {
        GameManager.instance.levelDifficulty = i;
    }
}

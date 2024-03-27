using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_UI : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    [SerializeField] private VolumeController[] volumeController;

    private void Awake()
    {
        AudioManager.instance.PlayBGM(0);
    }
    private void Start()
    {
        bool isNewGame = PlayerPrefs.GetInt("Level" + 2 + "Unlocked") == 1;
        continueButton.SetActive(isNewGame);
        Time.timeScale = 1;

        for (int i = 0; i < volumeController.Length; i++)
        {
            volumeController[i].GetComponent<VolumeController>().SetupVolumeSlider();
        }

        
    }
    public void SwitchMenuTo(GameObject uiMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false); // switch off everything
        }

        AudioManager.instance.PlaySFX(4);
        uiMenu.SetActive(true); // switch on what we need
    }


    public void SetGameDifficuly(int i)
    {
        GameManager.instance.levelDifficulty = i;
    }
}

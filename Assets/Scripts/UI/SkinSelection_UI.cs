using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkinSelection_UI : MonoBehaviour
{
    
    [SerializeField] private bool[] skinPurchased;
    [SerializeField] private int[] skinPrice;
    private int skinID;
    private int previousID;



    [Header("Components")]
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private Animator anim;
    [SerializeField] private TextMeshProUGUI bankText;


    public void SetupSkinInfo()
    {
        skinPurchased[0] = true;
        bankText.text = PlayerPrefs.GetInt("TotalFruitsCollected").ToString();

        for (int i = 1; i < skinPurchased.Length; i++)
        {
            bool skinUnlocked = PlayerPrefs.GetInt("SkinPurchased" + i) == 1;
            if (skinUnlocked)
                skinPurchased[i] = true;
        }

        if (skinPurchased[skinID])
        {
            buyButton.SetActive(false);
            selectButton.SetActive(true);
        }
        else
        {
            buyButton.SetActive(true);
            selectButton.SetActive(false);
        }

        if (!skinPurchased[skinID])
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Price: " + skinPrice[skinID].ToString();


    }


    private void Start()
    {

        
      

        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0); // set the weight of each layer to 0
        }

        
        anim.SetLayerWeight(skinID, 1); // set chosen fruit type layer weight to 1
        SetupSkinInfo();

    }

    public bool EnoughMoney()
    {
        int totalFruits = PlayerPrefs.GetInt("TotalFruitsCollected");

        if(totalFruits > skinPrice[skinID])
        {
            totalFruits -= skinPrice[skinID];
            PlayerPrefs.SetInt("TotalFruitsCollected", totalFruits);

            AudioManager.instance.PlaySFX(5);
            return true;
        }
        AudioManager.instance.PlaySFX(6);
        return false;
    }

    public void SetDefaultID()
    {
        skinID = 0;
    }
    private void OnEnable()
    {
        SetupSkinInfo();
    }

    private void OnDisable()
    {
        selectButton.SetActive(false);
    }

    public void NextSkin()
    {
        AudioManager.instance.PlaySFX(4);
        previousID = skinID;
        skinID++;
        if (skinID > 3)
            skinID = 0;

        anim.SetLayerWeight(previousID, 0); // set chosen fruit type layer weight to 1
        anim.SetLayerWeight(skinID, 1); // set chosen fruit type layer weight to 1
        SetupSkinInfo();
    }

    public void PreviousSkin()
    {

        AudioManager.instance.PlaySFX(4);
        previousID = skinID;
        skinID--;
        if (skinID < 0)
            skinID = 3;
        anim.SetLayerWeight(previousID, 0); // set chosen fruit type layer weight to 1
        anim.SetLayerWeight(skinID, 1); // set chosen fruit type layer weight to 1
        SetupSkinInfo();

    }


    public void Buy()
    {
        if (EnoughMoney())
        {

            PlayerPrefs.SetInt("SkinPurchased" + skinID, 1);
            SetupSkinInfo();
        }
        
    }

    public void Equip()
    {
       
        
        PlayerManager.instance.chosenSkinID = skinID;
        
    }

    public void SwitchSelectButton(GameObject newButton)
    {
        selectButton = newButton;
    }


}

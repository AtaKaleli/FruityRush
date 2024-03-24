using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkinSelection_UI : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private int skinID;
    private int previousID;

    [SerializeField] private bool[] skinPurchased;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private int[] skinPrice;

    [SerializeField] private TextMeshProUGUI bankText;

    public void SetupSkinInfo()
    {
        if (skinPurchased[skinID])
        {
            buyButton.SetActive(false);
            equipButton.SetActive(true);
        }
        else
        {
            buyButton.SetActive(true);
            equipButton.SetActive(false);
        }

        if (!skinPurchased[skinID])
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Price: " + skinPrice[skinID].ToString();


    }

    private void Start()
    {
        skinPurchased[0] = true;

        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0); // set the weight of each layer to 0
        }

        
        anim.SetLayerWeight(skinID, 1); // set chosen fruit type layer weight to 1
        SetupSkinInfo();

        bankText.text = PlayerPrefs.GetInt("TotalFruitsCollected").ToString();
    }

    public void SetDefaultID()
    {
        skinID = 0;
    }

    public void NextSkin()
    {
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
        skinPurchased[skinID] = true;
        SetupSkinInfo();
    }

    public void Equip()
    {
       
        
        PlayerManager.instance.chosenSkinID = skinID;
        
    }


}

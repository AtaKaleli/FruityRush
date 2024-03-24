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
    [SerializeField] private GameObject selectButton;
    [SerializeField] private int[] skinPrice;

    [SerializeField] private TextMeshProUGUI bankText;

    public void SetupSkinInfo()
    {
        skinPurchased[0] = true;
        bankText.text = PlayerPrefs.GetInt("TotalFruitsCollected").ToString();

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

    private void OnEnable()
    {
        SetupSkinInfo();
    }

    private void OnDisable()
    {
        selectButton.SetActive(false);
    }

    private void Start()
    {
        skinPurchased[0] = true;
        bankText.text = PlayerPrefs.GetInt("TotalFruitsCollected").ToString();

        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0); // set the weight of each layer to 0
        }

        
        anim.SetLayerWeight(skinID, 1); // set chosen fruit type layer weight to 1
        SetupSkinInfo();

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

    public void SwitchSelectButton(GameObject newButton)
    {
        selectButton = newButton;
    }


}

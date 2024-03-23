using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelection_UI : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private int skinID;
    private int previousID;

    private void Start()
    {
        

        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0); // set the weight of each layer to 0
        }

        
        anim.SetLayerWeight(skinID, 1); // set chosen fruit type layer weight to 1
    }

    

    public void NextSkin()
    {
        previousID = skinID;
        skinID++;
        if (skinID > 3)
            skinID = 0;
        anim.SetLayerWeight(previousID, 0); // set chosen fruit type layer weight to 1
        anim.SetLayerWeight(skinID, 1); // set chosen fruit type layer weight to 1
    }

    public void PreviousSkin()
    {
        previousID = skinID;
        skinID--;
        if (skinID < 0)
            skinID = 3;
        anim.SetLayerWeight(previousID, 0); // set chosen fruit type layer weight to 1
        anim.SetLayerWeight(skinID, 1); // set chosen fruit type layer weight to 1
    }

}

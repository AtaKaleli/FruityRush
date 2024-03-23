using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelection_UI : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private int skinID;

    public void NextSkin()
    {
        skinID++;
        if (skinID > 3)
            skinID = 0;
        anim.SetInteger("skinID", skinID);
    }

    public void PreviousSkin()
    {
        skinID--;
        if (skinID < 0)
            skinID = 3;
        anim.SetInteger("skinID", skinID);
    }

}

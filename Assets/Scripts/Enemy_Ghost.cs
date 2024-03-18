using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ghost : Enemy
{

    [Header("Ghost Info")]
    [SerializeField] private float activeTime;
                     private float activeTimeCounter;

    

    protected override void Start()
    {
        base.Start();
    }



    public override void Damage()
    {
        base.Damage();
    }


}

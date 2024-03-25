using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FruitType
{
    apple,
    banana,
    cherry,
    kiwi,
    melon,
    orange,
    pineapple,
    strawberry
}

public class Fruit_Item : MonoBehaviour
{

    [SerializeField] protected SpriteRenderer sr;
    [SerializeField] private Sprite[] fruitImage;
    public FruitType myFruitType;
    [SerializeField] protected Animator anim;



    public void FruitSetup(int fruitIndex)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0); // set the weight of each layer to 0
        }

        anim.SetLayerWeight(fruitIndex, 1); // set chosen fruit type layer weight to 1
    }

    /*
    private void OnValidate() // this function works even before the start function
    {
        sr.sprite = fruitImage[((int)myFruitType)];
    }*/

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        

        if(collision.GetComponent<Player>() != null)
        {
            PlayerManager.instance.collectedFruits++;
            Destroy(gameObject);
        }
    }
}

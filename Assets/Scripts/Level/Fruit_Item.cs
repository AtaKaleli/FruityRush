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

    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite[] fruitImage;
    public FruitType myFruitType;
    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();

        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0); // set the weight of each layer to 0
        }

        anim.SetLayerWeight(((int)myFruitType), 1); // set chosen fruit type layer weight to 1
    }

    private void OnValidate() // this function works even before the start function
    {
        sr.sprite = fruitImage[((int)myFruitType)];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if(collision.GetComponent<Player>() != null)
        {
            PlayerManager.instance.collectedFruits++;
            Destroy(gameObject);
        }
    }
}

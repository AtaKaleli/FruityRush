using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit_Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            collision.GetComponent<Player>().fruitCounter++;
            Destroy(gameObject);
        }
    }
}

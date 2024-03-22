using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_Background : MonoBehaviour
{
    [SerializeField] private Vector2 changeSpeed;
    [SerializeField] private MeshRenderer mesh;

    private void Update()
    {
        mesh.material.mainTextureOffset += changeSpeed * Time.deltaTime;
    }
}

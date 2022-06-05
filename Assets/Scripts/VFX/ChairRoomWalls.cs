using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairRoomWalls : MonoBehaviour
{

    Material mat;

    public float textureSpeed;

    void Start()
    {
        mat = GetComponent<Material>();
    }

    void Update()
    {
        mat.mainTextureOffset.Set(mat.mainTextureOffset.x, mat.mainTextureOffset.y + textureSpeed);
    }
}

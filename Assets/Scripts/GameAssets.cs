using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameAssets : MonoBehaviour
{
    public static GameAssets Instance;
    public Tile tileToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }
}

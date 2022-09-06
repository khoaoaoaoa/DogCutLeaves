using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class SpawnTileRandomly : MonoBehaviour
{
    [SerializeField] GameObject WhichObjToGetCellBounds;
    [SerializeField] int numberOfTiles;
    Tilemap tileMap_C;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomTile(numberOfTiles);
    }
    void GenerateRandomTile(int numberOfTiles)
    {

        List<Vector3> xyPositionOfExistedTile = new List<Vector3>();
        tileMap_C = WhichObjToGetCellBounds.GetComponent<Tilemap>();
        for (int i = tileMap_C.cellBounds.xMin; i < tileMap_C.cellBounds.xMax; i++)
        {
            for (int j = tileMap_C.cellBounds.yMin; j < tileMap_C.cellBounds.yMax; j++)
            {
                Vector3Int localTilePosition = new Vector3Int(i, j, (int)tileMap_C.transform.position.z);
                Vector3 tilePosition = tileMap_C.CellToWorld(localTilePosition);
                bool isTiled = tileMap_C.HasTile(localTilePosition);
                if (isTiled)
                {
                    xyPositionOfExistedTile.Add(tilePosition);

                }
            }
        }
        for (int i = 0; i < numberOfTiles; i++)
        {
            var positionToSpawn = new Vector3(Random.Range(tileMap_C.cellBounds.xMin, tileMap_C.cellBounds.xMax), Random.Range(tileMap_C.cellBounds.yMin, tileMap_C.cellBounds.yMax), tileMap_C.transform.position.z);
            foreach (var Vector in xyPositionOfExistedTile)
            {
                if (positionToSpawn == Vector)
                {
                    positionToSpawn = new Vector3(Random.Range(tileMap_C.cellBounds.xMin, tileMap_C.cellBounds.xMax), Random.Range(tileMap_C.cellBounds.yMin, tileMap_C.cellBounds.yMax), tileMap_C.transform.position.z);
                }
            }
            tileMap_C.SetTile(tileMap_C.WorldToCell(positionToSpawn), GameAssets.Instance.tileToSpawn /*Whatever tile you like*/ );
        }
    }
}


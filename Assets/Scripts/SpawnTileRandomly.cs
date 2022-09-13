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
            var positionToSpawn = new Vector3(Random.Range(tileMap_C.cellBounds.xMin + 2, tileMap_C.cellBounds.xMax - 2), Random.Range(tileMap_C.cellBounds.yMin + 2, tileMap_C.cellBounds.yMax - 2), tileMap_C.transform.position.z);
            for (int z = 0; z < xyPositionOfExistedTile.Count; z++)
            {
                if (positionToSpawn == xyPositionOfExistedTile[z])
                {
                    positionToSpawn = new Vector3(Random.Range(tileMap_C.cellBounds.xMin + 2, tileMap_C.cellBounds.xMax - 2), Random.Range(tileMap_C.cellBounds.yMin + 2, tileMap_C.cellBounds.yMax - 2), tileMap_C.transform.position.z);
                    z = 0;
                }
            }
            xyPositionOfExistedTile.Add(positionToSpawn);
            tileMap_C.SetTile(tileMap_C.WorldToCell(positionToSpawn), GameAssets.Instance.tileToSpawn /*Whatever tile you like*/ );
        }
    }
}


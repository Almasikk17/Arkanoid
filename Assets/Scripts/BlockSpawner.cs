using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public Block Block;
    public int Rows;
    public int Colomns;
    public float XOffset;
    public float YOffset;
    public Vector3 SpawnPosition;

    private void Start()
    {
        SpawnBlocks();
    }

    private void SpawnBlocks()
    {
        for(int i = 0; i < Rows; i++)
        {
            for(int v = 0; v < Colomns; v++)
            {
                Vector3 position = SpawnPosition + new Vector3(v * XOffset, i* YOffset, 0);
                Instantiate(Block, position, Quaternion.identity);
            }
        }
    }
}

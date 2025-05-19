using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockSpawner : MonoBehaviour
{
    [Header("- - - - -Grid Blocks- - - - -")]
    public Block Block;
    public int Rows;
    public int Colomns;
    public float XOffset;
    public float YOffset;
    public Vector3 SpawnPosition;


    [Header("- - - - -Spiral Blocks- - - - -")]
    public int SpiralBlocksCount;
    public float RadiusInterval;
    public float AngleInterval;
    public float SpiralSpeed;
    public float StartRadius;
    public Vector3 SpiralSpawnPosition;

    private Vector3 _lastBlockPosition; 

    [Header("- - - - -Circle Blocks- - - - -")]
    public float Radius;
    public int CircleBlocksCount;
    public Vector3 CircleSpawnPosition;

    [Header("- - - - -Square Blocks- - - - -")]
    public int SquareBlockCount;
    public Vector3 SquareSpawnPosition;

    public GameManager GameManager;
    
    private List<Block> _createdBlocks = new List<Block>();

    public void SpawnGridBlocks()
    {
        Debug.Log("Блоки первого уровня:");
        for(int i = 0; i < Rows; i++)
        {
            for(int v = 0; v < Colomns; v++)
            {
                Vector3 position = SpawnPosition + new Vector3(v * XOffset, i* YOffset, 0);
                Block block = Instantiate(Block, position, Quaternion.identity);
                _createdBlocks.Add(block);
                GameManager.SetBlocks(_createdBlocks);
            }
        }
    }

    public void SpawnSpiralBlocks()
    {
        Debug.Log("Блоки второго уровня:");
        float currentRadius = StartRadius;
        float currentAngle = 0f;
        for(int i = 0; i < SpiralBlocksCount; i++)
        {
            float angleRad = currentAngle * Mathf.Deg2Rad;
            float x = currentRadius * Mathf.Cos(angleRad);
            float y = currentRadius * Mathf.Sin(angleRad);
            Vector3 spawnPosition = new Vector3(x, y, 0) + SpiralSpawnPosition;
            Block block = Instantiate(Block, spawnPosition, Quaternion.identity);
            _createdBlocks.Add(block);
            GameManager.SetBlocks(_createdBlocks);
            currentRadius += RadiusInterval; 
            currentAngle += AngleInterval;
        }
    }

    public void SpawnCircleBlocks()
    {
        Debug.Log("Блоки третьего уровня:");
        float angleStep = 360 / CircleBlocksCount;
        for(int i = 0; i < CircleBlocksCount; i++)
        {
            float angleDeg = angleStep * i;
            float angleRad = angleDeg * Mathf.Deg2Rad;
            float x = Radius * Mathf.Cos(angleRad);
            float y = Radius * Mathf.Sin(angleRad);
            Vector3 spawnPosition = new Vector3(x, y, 0) + CircleSpawnPosition;
            Block block = Instantiate(Block, spawnPosition, Quaternion.identity);
            _createdBlocks.Add(block);
            GameManager.SetBlocks(_createdBlocks);
        }
    }

    private void SpawnZBlocks()
    {
        int blocksPerLine = 5;
        float blocksSpacing  = 2;
        Vector3 startPosition = transform.position; 
        
        for(int z = 0; z < blocksPerLine; z++)
        {
            Vector3 spawnPosition = startPosition + new Vector3(z * blocksSpacing, 0, 0);
            Instantiate(Block, spawnPosition, Quaternion.identity);
        }

        for(int z = 1; z < blocksPerLine; z++)
        {
            float x = startPosition.x + (blocksPerLine - 1) * blocksSpacing - z * blocksSpacing;
            float y = startPosition.y - z * blocksSpacing;
            Vector3 spawnPosition = new Vector3(x, y, 0);
            Instantiate(Block, spawnPosition, Quaternion.identity);
        }

        Vector3 bottomPosition = (startPosition + new Vector3(startPosition.x - 2, 0, 0)) + new Vector3(0, -blocksPerLine * blocksSpacing, 0);
        for(int z = 0; z < blocksPerLine; z++)
        {
            Vector3 spawnPosition = bottomPosition + new Vector3(z * blocksSpacing, 0, 0);
            Instantiate(Block, spawnPosition, Quaternion.identity);
        }
    }

    private void SpawnSquareBlocks()
    {
        int blocksSpacing = 1;
        Vector3 startPosition = SquareSpawnPosition;

        for(int i = 0; i < SquareBlockCount; i++)
        {
            Vector3 spawnPosition = startPosition + new Vector3(i * blocksSpacing, 0, 0);
            Instantiate(Block, spawnPosition, Quaternion.identity);
        }

        for(int i = 2; i < SquareBlockCount; i++)
        {
            Vector3 spawnPosition = startPosition + new Vector3(i * blocksSpacing -1, 4, 0);
            Instantiate(Block, spawnPosition, Quaternion.identity);
        }


        for(int i = 1; i < SquareBlockCount; i++)
        {
            Vector3 spawnPosition = startPosition +  new Vector3(0, i * blocksSpacing, 0);
            Instantiate(Block, spawnPosition, Quaternion.identity);
        }

        for(int i = 1; i < SquareBlockCount; i++)
        {
            Vector3 spawnPosition = startPosition + new Vector3(4, i * blocksSpacing, 0);
            Instantiate(Block, spawnPosition, Quaternion.identity);
        }
    }

/*
    private LevelNumber GetCurrenLevel()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        switch(level)
        {
            case 0:
                return LevelNumber.level1;

            case 1:
                return LevelNumber.level2;

            case 2:
                return LevelNumber.level3;

            default:
                return LevelNumber.level1;
        }
    }

    private void OrderSpawnBlocksLevel(LevelNumber level)
    {
        switch(level)
        {
            case LevelNumber.level1:
                SpawnGridBlocks();
                break;

            case LevelNumber.level2:
                //SpawnSpiralBlocks();
                SpawnSpiralBlocks();
                break;

            case LevelNumber.level3:
                SpawnCircleBlocks();
                break;
        }
    }
    */
}

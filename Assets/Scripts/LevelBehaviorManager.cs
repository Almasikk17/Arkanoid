using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBehaviorManager : MonoBehaviour
{
    public static LevelBehaviorManager Instance {get; set;}

    [SerializeField] private Block _block;
    [SerializeField] private BlockSpawner _spawner;

    private List<(int behaviourIndex, int spawnIndex)> _usedCombinations = new List<(int, int)>();
    public delegate void BlockBehaviour(Block block);
    private delegate void SpawnShape();
    private BlockBehaviour _currentBehaviour;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        SetupLevel(levelIndex);
    }

    private void SetupLevel(int levelIndex)
    {
        if (levelIndex >= 3)
        {
            Debug.Log("vse urovni proideni");
            return;
        }

        BlockBehaviour[] behaviours = new BlockBehaviour[]
        {
            (Block block) => block.GridBlocksBehaviour(), 
            (Block block) => block.SpiralBlocksBehaviour(),
            (Block block) => block.CircleBlocksBehaviour()
        };

        SpawnShape[] shapes = new SpawnShape[]
        {
            _spawner.SpawnGridBlocks,
            _spawner.SpawnSpiralBlocks,
            _spawner. SpawnCircleBlocks
        };

        int behaviourIndex, spawnIndex; 

        do
        {
            behaviourIndex = Random.Range(0, 3);
            spawnIndex = Random.Range(0, 3);
        }
        while(_usedCombinations.Contains((behaviourIndex, spawnIndex)));

        _usedCombinations.Add((behaviourIndex, spawnIndex));

        _currentBehaviour = behaviours[behaviourIndex];
        shapes[spawnIndex]();

        Debug.Log($"level: {levelIndex + 1}, behaviour: {behaviourIndex + 1}, spawn: {spawnIndex + 1}");
    }

    public BlockBehaviour GetBehaviour()
    {
        return _currentBehaviour;
    }
    
}

/*
    Делегат в С# - это указатель на метод. Он позволяет передовать методы как параметры, хранить их в переменных и вызвать позже.По сути этот способ делает код гибким
    и динамичным, особенно когда нужно указать, какой код должен выполниться в определенный момент.

    Action, Func, Predicate -  это современные делегаты 
    в современном C# редко пишут кастомные делегады с нуля,а так как есть generic(встроенные) типы:
    Action - делегат который используется для методов, которые ничего не возвращают(Void). Может принимать до 16 параметров.
    Func - делегат который используется для методов, которые возвращают значения.Последний тип в <> - это возвращаемый тип.
    Predicate - специальный делегат который используется для методов, возвращающих bool(используется в фильтрации).

    Action<string> action = (text)=> Debug.Log(text);
    action("Hello");

    Funk<int, int, int> sum = (a, b)=> a + b;
    Debug.Log(sum(2, 2)); 

    Predicate<int> isEven = (x)=> x % 2 == 0;
    Debug.Log(isEven(42)); //выведет true  
*/

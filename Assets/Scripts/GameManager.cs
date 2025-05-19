using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public Action OnLevelEnded;
    public int BallLives; 
    public TMP_Text LivesCountText;
    public Ball Ball;
    public Platform Platform;
    public bool GameIsOn;
    public GameObject AttemptsOverPanel;
    public GameObject NextLevelPanel;
    public Button[] RestartButton;
    public Button[] ExitToMenuButton;
    public Button NextLevelButton;

    private List<Block> _blocks = new List<Block>();
    private Block _block;

    private void Start()
    {
        AttemptsOverPanel.SetActive(false);
        Block.OnBlockDestroyed += BlocksCountHandler;
        OnLevelEnded += GameLevelHandler;
        NextLevelButton.onClick.AddListener(()=> SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1)); 
        LivesCountText.text = $"Lives: {BallLives.ToString()}";
        ScaleButtons();
    }

    private void ScaleButtons()
    {
        foreach(var button in ExitToMenuButton)
        {
            button.onClick.AddListener(() => button.transform.DOPunchScale(Vector3.one * 0.2f, 0.3f, 10, 1).OnComplete(() => SceneManager.LoadScene(2)));
        
        }
        
        foreach(var button in RestartButton)
        {
            button.onClick.AddListener(()=> SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        }
    }

    public void SetBlocks(List<Block> blocks)
    {
        _blocks = blocks;
    }

    private void BlocksCountHandler(Block block)
    {        
       StartCoroutine(DelayRemoveBlock());
    }

    private IEnumerator DelayRemoveBlock()
    {
        yield return new WaitForEndOfFrame();
         for(int i = _blocks.Count - 1; i >= 0; i--)
        {
            if(_blocks[i] == null)
            {
                Debug.Log("Minus block, youhoooooo!");
                _blocks.RemoveAt(i);
            }
        }
        if(_blocks.Count == 0)
        {
            NextLevelPanel.SetActive(true);
            RectTransform nextLevelPanelTransform = NextLevelPanel.GetComponent<RectTransform>();
            Vector2 startNextLevelPanelPosition = new Vector2(nextLevelPanelTransform.anchoredPosition.x, Screen.height);
            nextLevelPanelTransform.anchoredPosition = startNextLevelPanelPosition;
            nextLevelPanelTransform.DOAnchorPos(Vector2.zero, 1).SetEase(Ease.InOutBounce);
            AttemptsOverPanel.SetActive(false);
            GameLevelHandler();
        }
    }    

    public void BallLivesHandler(int live)
    {
        if(BallLives == 0)
        {
            AttemptsOverPanel.SetActive(true);
            AttemptsOverPanel.GetComponent<Animator>().SetTrigger("play");
            NextLevelPanel.SetActive(false);
            GameLevelHandler();
            return;
        }
        BallLives -= live;
        LivesCountText.text = $"Lives: {BallLives.ToString()}";
    }    

    private void GameLevelHandler()
    {
        OnLevelEnded -= GameLevelHandler;
        LivesCountText.gameObject.SetActive(false);
        Debug.Log("Игра окончена");
        Platform.enabled = false;
        GameIsOn = false;
        Ball.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Ball.GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    private void OnDisable()
    {
        Block.OnBlockDestroyed -= BlocksCountHandler;
    }
}

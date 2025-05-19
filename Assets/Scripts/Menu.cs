using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class Menu : MonoBehaviour
{
    public Button PlayButton;
    public Button ExitButton;
    public TMP_Text TitleText;
    public CanvasGroup MenuPanel;

    private Vector3 _startPositionPlay;
    private Vector3 _startPositionExit;

    private void Awake()
    {
        RectTransform playButtonTransform = PlayButton.GetComponent<RectTransform>();
        _startPositionPlay = playButtonTransform.anchoredPosition;
        playButtonTransform.anchoredPosition = new Vector2(-Screen.width, playButtonTransform.anchoredPosition.y);

        RectTransform exitButtonTransform = ExitButton.GetComponent<RectTransform>();
        _startPositionExit = exitButtonTransform.anchoredPosition;
        exitButtonTransform.anchoredPosition = new Vector2(Screen.width, exitButtonTransform.anchoredPosition.y);

        playButtonTransform.DOAnchorPos(_startPositionPlay, 1f).SetEase(Ease.OutExpo);
        exitButtonTransform.DOAnchorPos(_startPositionExit, 1f).SetEase(Ease.OutExpo);

        MenuPanel.alpha = 0;
        MenuPanel.DOFade(1, 1);
    }

    public void Start()
    {
        PlayButton.onClick.AddListener(()=>  MenuPanel.DOFade(0, 1).OnComplete(() => SceneManager.LoadScene(0)));
        TitleText.DOFade(0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
}

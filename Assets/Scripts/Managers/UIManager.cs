using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private UIPanelController uiPanelController;

    [SerializeField] private RectTransform arrow;
    [SerializeField] private LevelPanelController levelPanelController;
    [SerializeField] private TextMeshProUGUI leveltext;
    [SerializeField] private TextMeshProUGUI totalScore;
    [SerializeField] private IdlePanelController idlePanelController;
    #endregion
    #region private
    private int _multiplerScore;
    private int _levelScore;

    #endregion
    #endregion


    private void Awake()
    {
        OnUpdateLevelText();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onGameInit += OnUpdateLevelText;
        ScoreSignals.Instance.onUpdateScore += UpdateScoreText; 
        UISignals.Instance.onOpenPanel += OnOpenPanel;
        UISignals.Instance.onClosePanel += OnClosePanel;
        CoreGameSignals.Instance.onPlay += OnPlay;
        CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
        CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
        CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onGameInit -= OnUpdateLevelText;
        ScoreSignals.Instance.onUpdateScore -= UpdateScoreText; // her binaya griþde burayý tetikle scorun azalcak 
        UISignals.Instance.onOpenPanel -= OnOpenPanel;
        UISignals.Instance.onClosePanel -= OnClosePanel;
        CoreGameSignals.Instance.onPlay -= OnPlay;
        CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
        CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
        CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void OnOpenPanel(UIPanelTypes panelParam)
    {
        uiPanelController.OpenPanel(panelParam);
    }

    private void OnClosePanel(UIPanelTypes panelParam)
    {
        uiPanelController.ClosePanel(panelParam);
    }

    private void OnPlay()
    {
        UISignals.Instance.onClosePanel?.Invoke(UIPanelTypes.StartPanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.LevelPanel);
    }

    private void OnLevelFailed()
    {
        UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.FailPanel);
    }
    
    private void OnLevelSuccessful()
    {
        UISignals.Instance.onClosePanel?.Invoke(UIPanelTypes.LevelPanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.IdlePanel);
    }

    private void OnChangeGameState(GameStates _gameState)
    {
        switch (_gameState)
        {
            case GameStates.Roullette:
                OnOpenPanel(UIPanelTypes.RoulettePanel);
                CursorMovementOnRoulette();
                break;
            case GameStates.Idle:
                OnOpenPanel(UIPanelTypes.IdlePanel);
                OnClosePanel(UIPanelTypes.RoulettePanel);
                OnClosePanel(UIPanelTypes.LevelPanel);
                break;
            case GameStates.Runner:
                OnOpenPanel(UIPanelTypes.StartPanel);
                break;
            case GameStates.Failed:
                OnClosePanel(UIPanelTypes.LevelPanel);
                OnOpenPanel(UIPanelTypes.FailPanel);
                break;
        }
    }

    private void CursorMovementOnRoulette()
    {
        Sequence _sequence = DOTween.Sequence();
        _sequence.Join(arrow.transform.DORotate(new Vector3(0, 0, 30), 1).SetEase(Ease.Linear))
            .SetLoops(-1, LoopType.Yoyo);
        _sequence.Join(arrow.transform.DOLocalMoveX(-200, 1).SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo));
    }

    private void OnUpdateLevelText()
    {
        leveltext.text = "LEVEL " + (1 + CoreGameSignals.Instance.onGetLevelID?.Invoke()).ToString();

    }
    
    //public void GetTotalScoreData(List<int> ScoreValues)
    //{
    //    string _currentTotalScore = ScoreValues[0].ToString();
    //    UpdateTotalScore(_currentTotalScore);
    //}
    
    public void UpdateScoreText()
    {

        int currentTotalScrore = ScoreSignals.Instance.onGetScore(ScoreVariableType.TotalScore);
        totalScore.text= currentTotalScrore.ToString();
        
    }
    
    public void Play()
    {
        CoreGameSignals.Instance.onPlay?.Invoke();

    }

    public void NextLevel()
    {
        CoreGameSignals.Instance.onChangeGameState.Invoke(GameStates.Runner);
        CoreGameSignals.Instance.onNextLevel?.Invoke();
        UISignals.Instance.onClosePanel?.Invoke(UIPanelTypes.IdlePanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.StartPanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.LevelPanel);
        SaveSignals.Instance.onRunnerSaveData?.Invoke(); //savede 
        SaveSignals.Instance.onIdleSaveData?.Invoke();//Savede
        CameraSignals.Instance.onNextlevelCameraInit?.Invoke();  //kamera poziyonu için
        OnUpdateLevelText();
    }
    public void RetryLevel()
    {
        CoreGameSignals.Instance.onReset?.Invoke();
        UISignals.Instance.onClosePanel?.Invoke(UIPanelTypes.FailPanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.StartPanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.LevelPanel);
        SaveSignals.Instance.onRunnerSaveData?.Invoke(); //savede 

    }
    public void RestartButton()
    {
        UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.StartPanel);
        CoreGameSignals.Instance.onReset?.Invoke();
    }
    public void EnterIdle()
    {
        
        CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Idle);
        CameraSignals.Instance.onSetCameraState(CameraStates.Idle);
        _levelScore = ScoreSignals.Instance.onGetScore(ScoreVariableType.LevelScore);
        _multiplerScore = _levelScore * 3; // burda deðiþiklik yapcak gelen deðere göre
        ScoreSignals.Instance.onSetScore?.Invoke(ScoreVariableType.TotalScore,_multiplerScore);
        UpdateScoreText();
        ScoreSignals.Instance.onGetScore(ScoreVariableType.TotalScore);


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    //public float CurrentScore;

    #endregion

    #region Serialized Variables

    [SerializeField]
    private PlayerMovementController playerMovementController;
    [SerializeField]
    private PlayerPhysicsController playerPhysicsController;
    [SerializeField]
    private PlayerAnimationController playerAnimationController;
    [SerializeField]
    private PlayerMeshController playerMeshController;
    [SerializeField]
    private PlayerTextController playerTextController;
    [SerializeField]
    //private ColorTypes _currentColor;

    #endregion

    #region Private Variables

    private PlayerData Data;
    private GameStates _currentGameState = GameStates.Runner;


    #endregion

    #endregion

    private void Awake()
    {
        Data = GetPlayerData();
    }
    private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").PlayerData;

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void SubscribeEvents()
    {
        
        InputSignals.Instance.onIdleInputTaken += OnGetIdleInputValues;
        InputSignals.Instance.onInputDragged += OnGetRunnerInputValues;
        ScoreSignals.Instance.onUpdateScore += OnUpdateScoreText;
        CoreGameSignals.Instance.onPlay += OnPlay;
        CoreGameSignals.Instance.onReset += OnReset;
        CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
        CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
        CoreGameSignals.Instance.onGetGameState += OnChangeGameState;
        PlayerSignals.Instance.onIncreaseScale += OnIncreaseSize;
        InputSignals.Instance.onSidewaysEnable += OnSidewaysEnable;

        // PlayerSignal.Instance.onChangeVerticalSpeed += OnChangeVerticalSpeed;  Büyük ihtimal kullanmýycam
        //ScoreSignals.Instance.onUpdateScore += OnUpdateScore; Kullanmayabilirim

    }

    private void UnSubscribeEvents()
    {
        InputSignals.Instance.onIdleInputTaken -= OnGetIdleInputValues;
        InputSignals.Instance.onInputDragged -= OnGetRunnerInputValues;
        ScoreSignals.Instance.onUpdateScore -= OnUpdateScoreText;
        CoreGameSignals.Instance.onPlay -= OnPlay;
        CoreGameSignals.Instance.onReset -= OnReset;
        CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
        CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
        CoreGameSignals.Instance.onGetGameState -= OnChangeGameState;
        PlayerSignals.Instance.onIncreaseScale -= OnIncreaseSize;
        InputSignals.Instance.onSidewaysEnable -= OnSidewaysEnable;
    }

    private void OnPlay()
    {
        playerMovementController.EnableMovement();
    }
    private void OnReset()
    {
        //OnUpdateScore(0); //HÝÇ KULLANMAYABÝLRÝÝM
        playerMovementController.OnReset();
    }
    private void OnLevelSuccessful()
    {
        playerMovementController.IsReadyToPlay(false);
    }

    private void OnLevelFailed()
    {
        playerMovementController.IsReadyToPlay(false);
    }

    private void OnChangeGameState(GameStates arg0)
    {

        playerMovementController.CurrentGameState = arg0;
        _currentGameState = arg0;
        Debug.Log("Idle Scora gecti" + _currentGameState);
        //OnUpdateScoreText(new List<int>(){ScoreSignals.Instance.onGetScore.Invoke(ScoreVariableType.TotalScore)}); //KULLANMAYABÝLÝRÝM
        if (arg0 == GameStates.Idle)
        {
            playerMovementController.EnableIdleMovement();
        }
    }

    //public void OnChangeVerticalSpeed(float _verticalSpeed) Büyük ihtimal kullanýlmýycak
    //{
    //    playerMovementController.ChangeVerticalMovement(_verticalSpeed);
    //}

    public void OnSidewaysEnable(bool isSidewayEnable)
    {
        playerMovementController.SetSidewayEnabled(isSidewayEnable);
    }

    private void OnGetIdleInputValues(IdleInputParams inputParam)
    {
        playerMovementController.UpdateIdleInputValue(inputParam);
    }
    private void OnGetRunnerInputValues(RunnerHorizontalInputParams inputParam)
    {
        playerMovementController.UpdateRunnerInputValue(inputParam);
    }

    public void StopAllMovement()
    {
        playerMovementController.StopAllMovement();
      //  ChangeAnimation(PlayerAnimationTypes.Idle);
    }

    public void OnIncreaseSize()
    {
        playerMeshController.IncreasePlayerSize();
    }

    public void OnUpdateScoreText(List<int> _currentScores)
    {
        switch (_currentGameState)
        {
            case GameStates.Idle:
                Debug.Log("Idle Scora gecti");
             //   playerTextController.UpdatePlayerScore(_currentScores[0]);
                break;
            case GameStates.Runner:
              //  playerTextController.UpdatePlayerScore(_currentScores[1]);

                break;
            case GameStates.Failed:
                CloseScoreText(true);
                StopAllMovement();

                break;

        }

    }
    public void CloseScoreText(bool _visiblitystate)
    {
      //  playerTextController.CloseScoreText(_visiblitystate);
    }
}

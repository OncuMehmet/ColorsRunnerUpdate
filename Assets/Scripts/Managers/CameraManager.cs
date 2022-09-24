using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Threading.Tasks;

public class CameraManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    public CinemachineStateDrivenCamera StateCam;

    #endregion
    #region Serialized Variables
    
    [SerializeField]public Animator cameraAnimator;
    [SerializeField] private LookCinemachineAxis lookCinemachineAxis;


    #endregion

    #region Private Variables

    [SerializeField] private Vector3 _initialPosition;
    private CameraStates _currentCameraState = CameraStates.Runner;
    private PlayerManager _playerManager;
    #endregion

    #endregion

   

    private void Awake()
    {
        GetInitialPosition();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay += OnPlay;
        CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
        CameraSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
        CoreGameSignals.Instance.onReset += OnReset;

        CoreGameSignals.Instance.onLevelInitialize += OnPlay; //Level managerdan yazdým
       // LevelSignals.Instance.onNextLevel += OnNextLevel;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay -= OnPlay;
        CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
        CameraSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
        CoreGameSignals.Instance.onReset -= OnReset;

        CoreGameSignals.Instance.onLevelInitialize -= OnPlay; //Level managerdan yazdým
      //LevelSignals.Instance.onNextLevel -= OnNextLevel;
    }



        private void OnDisable()
    {
        UnsubscribeEvents();
    }
    private void GetInitialPosition()
    {
        _initialPosition = transform.localPosition;
    }

    private void OnPlay()
    {
        SetCameraTarget();
    }
    private void SetCameraTarget()
    {
        CameraSignals.Instance.onSetCameraTarget?.Invoke(CameraStates.Runner);
    }
    private void OnSetCameraTarget(CameraStates Currentstate)
    {
        if (!_playerManager)
        {
            _playerManager = FindObjectOfType<PlayerManager>();
        }
        if (Currentstate == CameraStates.Runner)
        {
            StateCam.Follow = _playerManager.transform;
            StateCam.LookAt = null;
            lookCinemachineAxis.enabled = true;
        }
        else if(Currentstate == CameraStates.Idle)
        {
            StateCam.LookAt = _playerManager.transform;
            lookCinemachineAxis.enabled = false;
        }
        cameraAnimator.Play(Currentstate.ToString());
    }

    void OnChangeGameState(GameStates currentGameState)
    {
        switch (currentGameState)
        {
            case GameStates.Roullette:
                _currentCameraState = CameraStates.Idle;
                OnSetCameraTarget(_currentCameraState);
                break;
            case GameStates.Runner:
                _currentCameraState = CameraStates.Runner;
                OnSetCameraTarget(_currentCameraState);
                break;
        }
    }

    private void OnNextLevel()
    {
        CameraTargetSetting();
    }
    private async void CameraTargetSetting()
    {
        await Task.Delay(50);
        SetCameraTarget();
    }
    private void OnReset()
    {
        CameraTargetSetting();
    }
    
    //public async void deneme()//KAMERE GEÇÝÞÝNÝ TEST ETTÝM
    //{
    //    await Task.Delay(5000);
    //    CameraSignals.Instance.onSetCameraTarget?.Invoke(CameraStates.Idle);
    //}
    
}

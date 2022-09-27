using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    [Header("InputData")] public InputData Data;

    #endregion
    #region Serialized Variables
    
    [SerializeField] private bool isFirstTimeTouchTaken = false;

    #endregion

    #region Private Variables

    private PlayerInputSystem _playerInput;
    private float _currentVelocity; 
    private Vector3 _moveVector;
    private Vector3 _playerMovementValue;

    #endregion

    #endregion

    private void Awake()
    {
        Data = GetInputData();
        InitialSettings();
    }

    private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").InputData;

    private void OnEnable()
    {
        _playerInput.Runner.Enable();
        SubscribeEvents();
    }

    private void InitialSettings()
    {
        _playerInput = new PlayerInputSystem();
        _playerMovementValue = Vector3.zero;
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onGetGameState += OnChangeInputType;
        
        CoreGameSignals.Instance.onReset += OnReset;
        _playerInput.Runner.MouseDelta.performed += OnPlayerInputMouseDeltaPerformed;
        _playerInput.Runner.MouseDelta.canceled += OnPlayerInputMouseDeltaCanceled;
        _playerInput.Runner.MouseLeftButton.started += OnMouseLeftButtonStart;
        _playerInput.Idle.JoyStick.performed += OnPlayerInputJoyStickPerformed;
        _playerInput.Idle.JoyStick.canceled += OnPlayerInputJoyStickCanceled;
        _playerInput.Idle.JoyStick.started += OnPlayerInputJoyStickStart;
    }

    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onGetGameState -= OnChangeInputType;
        CoreGameSignals.Instance.onReset -= OnReset;
        _playerInput.Runner.MouseDelta.performed -= OnPlayerInputMouseDeltaPerformed;
        _playerInput.Runner.MouseDelta.canceled -= OnPlayerInputMouseDeltaCanceled;
        _playerInput.Runner.MouseLeftButton.started -= OnMouseLeftButtonStart;
        _playerInput.Idle.JoyStick.performed -= OnPlayerInputJoyStickPerformed;
        _playerInput.Idle.JoyStick.canceled -= OnPlayerInputJoyStickCanceled;
        _playerInput.Idle.JoyStick.started -= OnPlayerInputJoyStickStart;
    }

    void OnPlayerInputMouseDeltaPerformed(InputAction.CallbackContext context)
    {
        InputSignals.Instance.onSidewaysEnable?.Invoke(true);

        _playerMovementValue = new Vector3(context.ReadValue<Vector2>().x, 0f, 0f);
        Vector2 mouseDeltaPos = new Vector2(context.ReadValue<Vector2>().x, 0f);

        if (mouseDeltaPos.x > Data.RunnerHorizontalInputSpeed)
            _moveVector.x = Data.RunnerHorizontalInputSpeed / 10f * mouseDeltaPos.x;

        else if (mouseDeltaPos.x < -Data.RunnerHorizontalInputSpeed)
            _moveVector.x = -Data.RunnerHorizontalInputSpeed / 10f * -mouseDeltaPos.x;
        else
            _moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity,
                Data.RunnerClampSpeed);

        InputSignals.Instance.onInputDragged?.Invoke(new RunnerHorizontalInputParams()
        {
            RunnerXValue = _moveVector.x,
            RunnerClampValues = new Vector2(Data.RunnerClampSides.x, Data.RunnerClampSides.y)
        });

    }

    void OnPlayerInputMouseDeltaCanceled(InputAction.CallbackContext context)
    {
        InputSignals.Instance.onSidewaysEnable?.Invoke(false);
        _playerMovementValue = new Vector3(context.ReadValue<Vector2>().x, 0f, 0f);
    }

    void OnMouseLeftButtonStart(InputAction.CallbackContext cntx)
    {
       if (isFirstTimeTouchTaken == false)
        {
            isFirstTimeTouchTaken = true;
            CoreGameSignals.Instance.onPlay?.Invoke();
       }
    }

    private void OnPlayerInputJoyStickStart(InputAction.CallbackContext context)
    {
        _playerMovementValue = new Vector3(context.ReadValue<Vector2>().x, 0f, context.ReadValue<Vector2>().y);

        InputSignals.Instance.onIdleInputTaken?.Invoke(new IdleInputParams()
        {
            IdleXValue = _playerMovementValue.x * Data.IdleInputSpeed,
            IdleZValue = _playerMovementValue.z * Data.IdleInputSpeed
        });
    }

    private void OnPlayerInputJoyStickPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("joystick");
        _playerMovementValue = new Vector3(context.ReadValue<Vector2>().x, 0f, context.ReadValue<Vector2>().y);
        InputSignals.Instance.onIdleInputTaken?.Invoke(new IdleInputParams()
        {

            IdleXValue = _playerMovementValue.x * Data.IdleInputSpeed,
            IdleZValue = _playerMovementValue.z * Data.IdleInputSpeed
        });
        Debug.Log("Sinyal sonrasýjoystick");
    }
    private void OnPlayerInputJoyStickCanceled(InputAction.CallbackContext context)
    {
        _playerMovementValue = Vector3.zero;
        InputSignals.Instance.onIdleInputTaken?.Invoke(new IdleInputParams()
        {
            IdleXValue = _playerMovementValue.x,
            IdleZValue = _playerMovementValue.z
        });
    }

    private void OnDisable()
    {
        _playerInput.Runner.Disable();
        UnSubscribeEvents();
    }

    private void OnChangeInputType(GameStates _currentGameState)
    {
        switch (_currentGameState)
        {
            case GameStates.Idle:
                _playerInput.Runner.Disable();
                
                _playerInput.Idle.Enable();
                Debug.Log("IDLE AÇILDI ");
                break;
            case GameStates.Runner:
                _playerInput.Idle.Disable();
                _playerInput.Runner.Enable();
                break;
            default:

                break;
        }

    }

    private void OnReset()
    {
        isFirstTimeTouchTaken = false;
    }

   
}

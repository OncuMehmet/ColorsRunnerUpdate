using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class StackManager : MonoBehaviour
{
    #region Self Variables
    #region Serialized Variables

    [SerializeField]
    private GameObject collectablePrefab;
    [SerializeField]
    private int initAmount = 4;
    [SerializeField]
    private List<GameObject> stackList;
    [SerializeField]
    private Transform tempHolder;

    #endregion

    #region Private Variables
    private Transform _playerManager;
    #endregion
    #endregion
    private void Start()
    {
        OnInitalStackSettings();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        StackSignals.Instance.onIncreaseStack += OnIncreaseStack;
        CoreGameSignals.Instance.onReset += OnReset;
        CoreGameSignals.Instance.onGameInitLevel += FindPlayer;
        StackSignals.Instance.onDecreaseStackRoullette += OnDecreaseStackRoullette;
        StackSignals.Instance.onDroneArea += OnDroneAreaDecrease;
        StackSignals.Instance.onDoubleStack += OnDoubleStack;
        StackSignals.Instance.onDecreaseStack += OnDecreaseStack;
        CoreGameSignals.Instance.onPlay += OnInitRunAnimation;
        StackSignals.Instance.onAnimationChange += OnChangeAnimationInStack;
        StackSignals.Instance.onColorChange += OnChangeColor;
    }
    
    private void UnSubscribeEvents()
    {
        StackSignals.Instance.onIncreaseStack -= OnIncreaseStack;
        CoreGameSignals.Instance.onReset -= OnReset;
        CoreGameSignals.Instance.onGameInitLevel -= FindPlayer;
        StackSignals.Instance.onDecreaseStackRoullette -= OnDecreaseStackRoullette;
        StackSignals.Instance.onDroneArea -= OnDroneAreaDecrease;
        StackSignals.Instance.onDoubleStack -= OnDoubleStack;
        StackSignals.Instance.onDecreaseStack -= OnDecreaseStack;
        CoreGameSignals.Instance.onPlay -= OnInitRunAnimation;
        StackSignals.Instance.onAnimationChange -= OnChangeAnimationInStack;
        StackSignals.Instance.onColorChange -= OnChangeColor;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void FixedUpdate()
    {
        LerpStack();
    }

    private void LerpStack()
    {
        if (stackList.Count > 0)
        {
            if (stackList[0] != null && _playerManager != null)
            {
                stackList[0].transform.position = new Vector3(
               Mathf.Lerp(stackList[0].transform.position.x, _playerManager.transform.position.x, .2f),
               Mathf.Lerp(stackList[0].transform.position.y, _playerManager.transform.position.y, .2f),
               Mathf.Lerp(stackList[0].transform.position.z, _playerManager.transform.position.z - .8f, .2f));
                Quaternion _toPlayerRotation = Quaternion.LookRotation(_playerManager.transform.position - stackList[0].transform.position);
               
                stackList[0].transform.rotation = Quaternion.Slerp(_playerManager.transform.rotation, _toPlayerRotation, 1f);
            }
           

            if (stackList.Count > 1)
            {
                for (int index = 1; index < stackList.Count; index++)
                {
                    stackList[index].transform.position = new Vector3(
                        Mathf.Lerp(stackList[index].transform.position.x, stackList[index - 1].transform.position.x, .2f),
                        Mathf.Lerp(stackList[index].transform.position.y, stackList[index - 1].transform.position.y, .2f),
                        Mathf.Lerp(stackList[index].transform.position.z, stackList[index - 1].transform.position.z - .8f, .2f));
                    Quaternion toRotation = Quaternion.LookRotation(stackList[index - 1].transform.position - stackList[index].transform.position);
                    toRotation = Quaternion.Euler(0, toRotation.eulerAngles.y, 0);
                    stackList[index].transform.rotation = Quaternion.Slerp(stackList[index - 1].transform.rotation, toRotation, 1f);
                }
            }


        }
    }
    
    private void FindPlayer()
    {
       
        _playerManager = FindObjectOfType<PlayerManager>().transform;
    }

    private void OnIncreaseStack(GameObject _currentGameObject)
    {
        ScoreSignals.Instance.onChangeScore(ScoreTypes.IncScore, ScoreVariableType.LevelScore);
        _currentGameObject.transform.SetParent(transform);
        stackList.Add(_currentGameObject);
        
    }

    private void OnInitRunAnimation()
    {
        OnChangeAnimationInStack(CollectableAnimationTypes.Run);
        Debug.Log("RunANÝMATÝON");
    }
    private void OnChangeAnimationInStack(CollectableAnimationTypes _currentAnimation)
    {
        for (int i = 0; i < stackList.Count; i++)
        {
            stackList[i].GetComponent<CollectableManager>().ChangeAnimationOnController(_currentAnimation);
        }
    }

    private void DeleteStack()
    {
        for (int i = 0; i < stackList.Count; i++)
        {
            
            stackList[i].transform.SetParent(null);
            Destroy(stackList[i]);
            ScoreSignals.Instance.onChangeScore(ScoreTypes.DecScore, ScoreVariableType.LevelScore);

        }
    }

    private void OnInitalStackSettings()
    {//deger datadan
        FindPlayer();
        OnChangeStack(initAmount);
        Debug.Log("ONCHANGE STACK");
        StackSignals.Instance.onAnimationChange?.Invoke(CollectableAnimationTypes.Crouch);
    }

    private void OnChangeStack(int initAmount)
    {
        for (int i = 0; i < initAmount; i++)
        {
            var _gameObject = Instantiate(collectablePrefab, Vector3.back * i, Quaternion.identity);
            OnIncreaseStack(_gameObject);
           _gameObject.GetComponent<CollectableManager>().ChangeAnimationOnController(CollectableAnimationTypes.Crouch);
        }


    }

    private async void OnDroneAreaDecrease(int index)
    {
        ScoreSignals.Instance.onChangeScore(ScoreTypes.DecScore, ScoreVariableType.LevelScore);
        stackList[index].transform.parent = tempHolder;
        stackList.RemoveAt(index);
        stackList.TrimExcess();
        if (stackList.Count == 0)
        {
            await Task.Delay(4000);
            DroneAreaSignals.Instance.onDroneCheckStarted?.Invoke();
            await Task.Delay(2000);
            DroneAreaSignals.Instance.onDroneCheckCompleted?.Invoke();
        }

    }
    
    private void OnDecreaseStackRoullette(int _removedIndex)
    {
        if (stackList[_removedIndex] is null)
        {
            return;
        }
        stackList[_removedIndex].SetActive(false);
        stackList.RemoveAt(_removedIndex);
        stackList.TrimExcess();
        if (stackList.Count == 0)
        {
           CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Roullette);
        }

    }

    private void OnDoubleStack()
    {
        OnChangeStack(stackList.Count * 2);
    }
    
    private void OnDecreaseStack(int _removedIndex)
    {
        ScoreSignals.Instance.onChangeScore(ScoreTypes.DecScore, ScoreVariableType.LevelScore);
        if (stackList[_removedIndex] is null)
        {
            return;
        }
        stackList[_removedIndex].transform.parent = tempHolder;
        
        stackList.RemoveAt(_removedIndex);
        stackList.TrimExcess();
        if (stackList.Count == 0)
        {
            CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Failed);
        }

    }

    private void OnChangeColor(ColorTypes colorType)
    {
        for (int i = 0; i < stackList.Count; i++)
        {
            stackList[i].GetComponent<CollectableManager>().ChangeColor(colorType);
        }
    }

    private async void OnReset()
    {
        DeleteStack();
        stackList.Clear();
        stackList.TrimExcess();
        await Task.Delay(200);
        StackSignals.Instance.onStackInit.Invoke();
        OnInitalStackSettings();
    }

}

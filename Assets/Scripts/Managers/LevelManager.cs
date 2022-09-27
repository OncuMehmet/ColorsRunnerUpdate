using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class LevelManager : MonoBehaviour
{
    #region Self Variables
    #region Serialized Variables

    [Space] [SerializeField] private GameObject levelHolder;
    [Space] [SerializeField] private GameObject idleLevelHolder;
    
    #endregion
    #region Private Variables

    private int _levelID;
    private int _idleLevelID;
    private LevelLoaderCommand levelLoader;
    private IdleLevelLoaderCommand idleLevelLoader;
    private ClearActiveLevelCommand levelClearer;

    #endregion

    #endregion
    private void Awake()
    {

        _idleLevelID = GetActiveIdleLevel();
        GetCommandComponents();
        OnInitializeLevel();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize += OnInitializeLevel;
        
        CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
        CoreGameSignals.Instance.onNextLevel += OnNextLevel;
        CoreGameSignals.Instance.onReset += OnReset;
        CoreGameSignals.Instance.onGetLevelID += OnGetLevelID;
        CoreGameSignals.Instance.onGetIdleLevelID += OnGetIdleLevelID;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize -= OnInitializeLevel;
       
        CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
        CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
        CoreGameSignals.Instance.onReset -= OnReset;
        CoreGameSignals.Instance.onGetLevelID -= OnGetLevelID;
        CoreGameSignals.Instance.onGetIdleLevelID -= OnGetIdleLevelID;

    }
    
    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void GetCommandComponents()
    {
        levelLoader = GetComponent<LevelLoaderCommand>();
        idleLevelLoader = GetComponent<IdleLevelLoaderCommand>();
        levelClearer = GetComponent<ClearActiveLevelCommand>();
    }
    
    private int GetActiveIdleLevel()
    {
        return _idleLevelID;
    }
    
    private void Start()
    {
      //_levelID = GetActiveLevel();
        OnInitializeIdleLevel();
    }

    private void OnNextLevel()
    {
        _levelID++;
        
        SaveSignals.Instance.onRunnerSaveData?.Invoke();
        CoreGameSignals.Instance.onReset?.Invoke();
    }
    private void OnNextIdleLevel()
    {
        _idleLevelID++;
       
        CoreGameSignals.Instance.onReset?.Invoke();
        
        CoreGameSignals.Instance.onLevelIdleInitialize?.Invoke();
    }
    private async void OnReset()
    {
        await Task.Delay(50);
        CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
        
        CoreGameSignals.Instance.onLevelInitialize?.Invoke();
        CoreGameSignals.Instance.onLevelIdleInitialize?.Invoke();
    }
    private int OnGetLevelID()
    {
        return _levelID;
    }
    private int OnGetIdleLevelID()
    {
        return _idleLevelID;
    }

    private void OnInitializeLevel()
    {
        var newLevelData = _levelID % Resources.Load<CD_Level>("Data/CD_Level").LevelData.LevelAmount;
        levelLoader.InitializeLevel(newLevelData, levelHolder.transform);
    }
    private void OnInitializeIdleLevel()
    {
        var newLevelData = _idleLevelID % Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevel.CityData.Count;
        idleLevelLoader.InitializeIdleLevel(newLevelData, idleLevelHolder.transform);
    }
    private void OnClearActiveLevel()
    {
        levelClearer.ClearActiveLevel(levelHolder.transform);
    }
    private void OnClearActiveIdleLevel()
    {
        levelClearer.ClearActiveLevel(idleLevelHolder.transform);
    }

}

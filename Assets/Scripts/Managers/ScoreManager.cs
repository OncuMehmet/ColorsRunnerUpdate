using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SocialPlatforms.Impl;


public class ScoreManager : MonoBehaviour
{
    #region Self Variables

    #region Private Variables

    private int _totalScore;
    private int _levelScore;
    private List<int> _scoreVariables = new List<int>(Enum.GetNames(typeof(ScoreVariableType)).Length);
    
    #endregion

    #region Serialized Variables


    #endregion
    #endregion

    private void Awake()
    {
        InitScoreValues();
       // _totalScore= SaveSignals.Instance.onRunnerGameLoad().Score; //Duruma göre bakýcam
        print(_totalScore);
    }

    private void OnEnable()
    {

        SubscribeEvents();
    }

    private void SubscribeEvents()
    {

        ScoreSignals.Instance.onChangeScore += OnChangeScore;
        StackSignals.Instance.onStackInit += OnReset;
        //ScoreSignals.Instance.onAddLevelTototalScore += OnAddLevelToTotalScore;
        ScoreSignals.Instance.onGetScore += OnGetScore;
        ScoreSignals.Instance.onSetScore += OnSetScore;
    }

    private void UnSubscribeEvents()
    {
        ScoreSignals.Instance.onChangeScore -= OnChangeScore;
        StackSignals.Instance.onStackInit -= OnReset;
        //ScoreSignals.Instance.onAddLevelTototalScore -= OnAddLevelToTotalScore;
        ScoreSignals.Instance.onGetScore -= OnGetScore;
        ScoreSignals.Instance.onSetScore -= OnSetScore;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void InitScoreValues()
    {
        for (int i = 0; i < Enum.GetNames(typeof(ScoreVariableType)).Length; i++)
        {
            _scoreVariables.Add(0);
        }
    }
    
    private int OnGetScore(ScoreVariableType _scoreVarType)
    {
        switch (_scoreVarType)
        {
            case ScoreVariableType.TotalScore:
                return _totalScore;
                break;
            case ScoreVariableType.LevelScore:
                return _levelScore;
                break;
            default:
                return 0;

                break;
        }
        
    }
    
    private void OnChangeScore(ScoreTypes _scoreType, ScoreVariableType _scoreVarType) //deðiþiklik yaptým binalar burdan tetiklencek
    {
        switch (_scoreVarType)
        {
            case ScoreVariableType.LevelScore:
                switch (_scoreType)
                {
                    case ScoreTypes.DecScore:
                        _levelScore--;
                        ScoreSignals.Instance.onUpdateScore?.Invoke();
                        break;
                    case ScoreTypes.IncScore:
                        _levelScore++;
                        ScoreSignals.Instance.onUpdateScore?.Invoke();
                      
                        break;
                    case ScoreTypes.DoubleScore:
                        _levelScore += _scoreVariables[(int)_scoreVarType];
                        ScoreSignals.Instance.onUpdateScore?.Invoke();
                        break;

                }
                break;

            case ScoreVariableType.TotalScore:
                switch (_scoreType)
                {
                    case ScoreTypes.DecScore:
                        _totalScore--;
                        ScoreSignals.Instance.onUpdateScore?.Invoke();
                        break;
                    case ScoreTypes.IncScore:
                        _totalScore++;
                        ScoreSignals.Instance.onUpdateScore?.Invoke();
                        break;
                    case ScoreTypes.DoubleScore:
                        _totalScore += _scoreVariables[(int)_scoreVarType];
                        ScoreSignals.Instance.onUpdateScore?.Invoke();
                        break;

                }
                break;
        }
    }
    
    private void OnReset()
    {
        _levelScore = 0;
        SaveSignals.Instance.onRunnerSaveData?.Invoke();
    }

    private void OnSetScore(ScoreVariableType scoreVariableType,int value)
    {
        switch (scoreVariableType)
        {
            case ScoreVariableType.TotalScore:
                _totalScore += value;
                break;
            case ScoreVariableType.LevelScore:
                _levelScore += value;
                break;
            default:
                break;
        }
    }

}

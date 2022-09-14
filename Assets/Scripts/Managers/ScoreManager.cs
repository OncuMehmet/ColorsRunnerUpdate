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


    private void OnEnable()
    {

        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        
        
        StackSignals.Instance.onStackInit += OnReset;
        
    }

    private void UnSubscribeEvents()
    {
      
        StackSignals.Instance.onStackInit -= OnReset;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void OnReset()
    {
        _scoreVariables[1] = 0;
        ScoreSignals.Instance.onUpdateScore?.Invoke(_scoreVariables);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ScoreSignals : MonoSingleton<ScoreSignals>
{
    // public UnityAction<List<int>> onUpdateScore = delegate { };
    public UnityAction onUpdateScore = delegate { };
    public UnityAction<ScoreTypes, ScoreVariableType> onChangeScore = delegate { };
    public Func<ScoreVariableType, int> onGetScore = delegate { return 0; };
    public UnityAction onAddLevelTototalScore = delegate { };
    public UnityAction <ScoreVariableType,int> onSetScore = delegate { };
}

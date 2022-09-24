using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class CoreGameSignals : MonoSingleton<CoreGameSignals>
{
    public UnityAction onPlay = delegate { };
    public UnityAction onReset = delegate { };
    public UnityAction<GameStates> onGetGameState = delegate { };
    public UnityAction<GameStates> onChangeGameState = delegate { };
    public UnityAction onLevelSuccessful = delegate { };
    public UnityAction onLevelFailed = delegate { };
    public UnityAction onLevelInitialize = delegate { };
    public UnityAction onLevelIdleInitialize = delegate { };
    public UnityAction onClearActiveLevel = delegate { };
    public UnityAction onGameInitLevel = delegate { };
    public UnityAction onGameInit = delegate { };
    public Func<int> onGetLevelID = delegate { return 0; };
    public Func<int> onGetIdleLevelID = delegate { return 0; };
    public UnityAction onNextLevel = delegate { };





    protected override void Awake()
    {
        base.Awake();
    }
}

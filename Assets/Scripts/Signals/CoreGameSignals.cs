using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoreGameSignals : MonoSingleton<CoreGameSignals>
{
    public UnityAction onPlay = delegate { };
    public UnityAction onReset = delegate { };
    public UnityAction<GameStates> onGetGameState = delegate { };
    public UnityAction<GameStates> onChangeGameState = delegate { };
    public UnityAction onLevelSuccessful = delegate { };
    public UnityAction onLevelFailed = delegate { };
    public UnityAction onGameInitLevel = delegate { };





    protected override void Awake()
    {
        base.Awake();
    }
}

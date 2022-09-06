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






    protected override void Awake()
    {
        base.Awake();
    }
}

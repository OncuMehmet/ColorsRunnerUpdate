using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputSignals : MonoSingleton<InputSignals>
{
    public UnityAction<RunnerHorizontalInputParams> onInputDragged = delegate { };
    public UnityAction<bool> onSidewaysEnable = delegate { };
    public UnityAction<IdleInputParams> onIdleInputTaken = delegate { };


}

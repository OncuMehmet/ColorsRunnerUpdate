using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StackSignals : MonoSingleton<StackSignals>
{
    public UnityAction onStackInit = delegate { };
    public UnityAction<GameObject> onIncreaseStack = delegate { };
    public UnityAction<CollectableAnimationTypes> onAnimationChange = delegate { };
    public UnityAction<int> onDroneArea = delegate { };
    public UnityAction<int> onDecreaseStackRoullette = delegate { };
    public UnityAction onDoubleStack = delegate { };
    public UnityAction<int> onDecreaseStack = delegate { };
    public UnityAction<ColorTypes> onColorChange = delegate { };

}

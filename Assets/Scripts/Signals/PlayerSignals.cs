using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSignals : MonoSingleton<PlayerSignals>
{

    public UnityAction onIncreaseScale = delegate { };

}

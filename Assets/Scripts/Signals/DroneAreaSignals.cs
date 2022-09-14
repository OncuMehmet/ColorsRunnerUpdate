using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DroneAreaSignals : MonoSingleton<DroneAreaSignals>
{
    public UnityAction onDroneCheckStarted = delegate { };
    public UnityAction onDroneCheckCompleted = delegate { };

}
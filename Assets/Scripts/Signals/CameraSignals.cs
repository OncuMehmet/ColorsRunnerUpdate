using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraSignals : MonoSingleton<CameraSignals>
{
    public UnityAction<CameraStates> onSetCameraState = delegate { };
    public UnityAction<CameraStates> onSetCameraTarget = delegate { };
}

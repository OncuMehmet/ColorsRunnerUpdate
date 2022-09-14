using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleSignals : MonoSingleton<ParticleSignals>
{
    public UnityAction<Vector3> onPlayerDeath = delegate { };
}

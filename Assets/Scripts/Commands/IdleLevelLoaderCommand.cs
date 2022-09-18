using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleLevelLoaderCommand : MonoBehaviour
{
    public void InitializeIdleLevel(int _idleLevelID, Transform levelHolder)
    {
        Instantiate(Resources.Load<GameObject>($"Prefabs/IdleLevelPrefabs/IdleLevel {_idleLevelID}"), levelHolder);
        CoreGameSignals.Instance.onGameInit?.Invoke();

    }
}

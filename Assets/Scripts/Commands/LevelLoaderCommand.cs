using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoaderCommand : MonoBehaviour
{
    public void InitializeLevel(int _levelID, Transform levelHolder)
    {
        Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/level {_levelID}"), levelHolder);
        CoreGameSignals.Instance.onGameInitLevel?.Invoke();
    }
}

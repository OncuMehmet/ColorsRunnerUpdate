using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleManager : MonoBehaviour
{
    private int _currentIdleLevelId;




    private void OnGetSaveData()
    {

        _currentIdleLevelId = CoreGameSignals.Instance.onGetIdleLevelID.Invoke();


    }
}

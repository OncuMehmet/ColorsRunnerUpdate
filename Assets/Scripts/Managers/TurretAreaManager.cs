using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TurretAreaManager : MonoBehaviour
{
    #region Self Region
    #region Serialized Variables

    [SerializeField] private List<TurretAreaController> turretList;
    
    #endregion
    #region Private Variables

    private List<GameObject> _targetList = new List<GameObject>();
    private TurretStates _turretState;
    #endregion
    #endregion

    public void Start()
    {
        InvokeRepeating(nameof(KillFromTargetList), 0, 0.5f);
    }

    private void FixedUpdate()
    {
        CheckTurretState();
        
    }

    private void CheckTurretState()
    {
        foreach (var _turret in turretList)
        {
            switch (_turretState)
            {
                case TurretStates.Search:
                    _turret.StartSearchRotation();
                    break;
                case TurretStates.Warned:
                    _turret.StartWarnedRotation(_targetList[0]);
                    break;
            }
        }
    }

    public void ResetTurretArea()
    {
        CancelInvoke(nameof(KillFromTargetList));
        _targetList.Clear();
        _turretState = TurretStates.Search;
    }

    public void KillFromTargetList()
    {
        if (_targetList.Count != 0)
        {
            GameObject _currentTarget = _targetList[0];
            _targetList.RemoveAt(0);
            _currentTarget.GetComponent<CollectableManager>().DelayedDeath(true);
            StackSignals.Instance.onDecreaseStack(0);
            _turretState = _targetList.Count > 0 ? ChangeTurretState(TurretStates.Warned) : ChangeTurretState(TurretStates.Search);
           
        }

    }
    public void AddTargetToList(GameObject _other)
    {
        _targetList.Add(_other);
         ChangeTurretState(TurretStates.Warned);
    }

    private TurretStates ChangeTurretState(TurretStates _currentState)
    {
        return _turretState = _currentState;
    }

    public void CheckShutDownCondition()
    {
        if (_targetList.Count != 0)
        {
            float _relativeDistance = transform.position.z - _targetList[0].transform.position.z;
            if ((turretList[0].transform.localScale.z / 2) < _relativeDistance)
            {
               
                ResetTurretArea();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAreaManager : MonoBehaviour
{
    #region Self Region
    
    #region Serialized Variables
    [SerializeField]
    private List<TurretAreaController> turretList;



    #endregion

    #region Private Variables

    private List<GameObject> _targetList = new List<GameObject>();
    //private TurretStates _turretState;

    #endregion

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KillFromTargetList()
    {
        if (_targetList.Count != 0)
        {
            GameObject _currentTarget = _targetList[0];
            _targetList.RemoveAt(0);
            _currentTarget.GetComponent<CollectableManager>().DelayedDeath(true);
            StackSignals.Instance.onDecreaseStack(0);
            //_turretState = _targetList.Count > 0 ? ChangeTurretState(TurretStates.Warned) : ChangeTurretState(TurretStates.Search);
            //foreach (var turret in turretList)
            //{
            //    turret.FireTurretAnimation();
            //}
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAreaPhysicController : MonoBehaviour
{
    #region Self Variables
    #region Serialized Variables

    [SerializeField]
    private TurretAreaManager turretAreaManager;
    
    #endregion
    #endregion

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Collected"))
        {
            turretAreaManager.ResetTurretArea();

        }
    }
}

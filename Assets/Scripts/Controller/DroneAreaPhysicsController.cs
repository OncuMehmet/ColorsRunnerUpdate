using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAreaPhysicsController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables
    [SerializeField]
    private DroneColorAreaManager droneColorAreaManager;


    #endregion


    #endregion
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collected"))
        {
            if (other.GetComponentInParent<CollectableManager>().CurrentColorType == droneColorAreaManager.CurrentColorType)
            {
                droneColorAreaManager.matchType = MatchType.Match;
            }

        }
    }
}

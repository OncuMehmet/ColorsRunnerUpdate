using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearActiveLevelCommand : MonoBehaviour
{
    public void ClearActiveLevel(Transform levelHolder)
    {
        Destroy(levelHolder.GetChild(0).gameObject);
    }
    
}

using System;
using UnityEngine;


[Serializable]
public class IdleBuildingData
{
    public int BuildingId;
    public bool IsDepended;
    public int PayedAmount;
    public int TotalRequiredAmount;
    public bool IsCompleted;
    public SideObjectData SideObjectData;
    public bool IsSideObjectActive = false;

}

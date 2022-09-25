using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class MainBuildingData 
{
    public string BuildingName;
    public BuildingComplateState CompleteState = BuildingComplateState.Uncompleted;
    public int Price;
    public int PayedAmount;
}

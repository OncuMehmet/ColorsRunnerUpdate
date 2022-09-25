using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SaveIdleGameDataParams
{
    public int IdleLevel;
    public int CollectablesCount;
    public List<int> MainPayedAmount;
    public List<int> SidePayedAmount;
    public List<BuildingComplateState> MainBuildingState;
    public List<BuildingComplateState> SideBuildingState;
}

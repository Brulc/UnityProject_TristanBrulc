using System.Collections.Generic;
using UnityEngine;

public class LaneInfo
{
    [HideInInspector] public bool occupation;
    [HideInInspector] public bool teamUpOccupation;
    public LaneType type;
    public int laneID;
    public List<MinionInfo> minionsInLane;
    public LaneInfo(LaneType type, int index)
    {
        this.type = type;
        laneID = index;
        occupation = false;
        teamUpOccupation = false;
        minionsInLane = new();
    }
}

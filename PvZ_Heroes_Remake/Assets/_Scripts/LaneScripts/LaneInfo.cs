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
    public bool CheckEmpty(CardInfo card)
    {
        foreach ( MinionInfo minion in minionsInLane )
        {
            if ( minion.card.cardTeam == card.cardTeam ) return false;
        }
        return true;
    }
}

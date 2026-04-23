using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    #region Player and Player Hands definition
    [HideInInspector] public Player manPlayer;
    [HideInInspector] public Player womanPlayer;
    #endregion

    private List<LaneInfo> lanes;

    #region Game data
    private const int healthAmount = 20;
    private const int startingCards = 4;

    #endregion
    
    #region Databases
    [SerializeField] private HeroDatabase ManHeroDatabase;
    [SerializeField] private HeroDatabase WomanHeroDatabase;
    #endregion

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void Initialize()
    {
        // Creating lanes
        lanes = new();
        lanes.Add(new(LaneType.Heights, 0));
        for (int i = 1; i < 4; i++ )
            lanes.Add(new(LaneType.Ground, i));
        lanes.Add(new(LaneType.Water, 4));

        UIManager.Instance.CreateLanes(lanes);

        // Assigning Heroes
        if ( SessionChoices.Instance.chosenHero.team == Team.Man)
        {
            manPlayer = new(SessionChoices.Instance.chosenHero, SessionChoices.Instance.deck, healthAmount);
            womanPlayer = GeneratePlayer(Team.Woman);

            manPlayer.InitializeHand(startingCards);
            womanPlayer.InitializeHand(startingCards);

            UIManager.Instance.InitializeHand(manPlayer.hand);
        }
        else
        {
            womanPlayer = new(SessionChoices.Instance.chosenHero, SessionChoices.Instance.deck, healthAmount);
            manPlayer = GeneratePlayer(Team.Man);

            manPlayer.InitializeHand(startingCards);
            womanPlayer.InitializeHand(startingCards);

            UIManager.Instance.InitializeHand(womanPlayer.hand);
        }
        

        UIManager.Instance.DrawHeroes();
    }
    private Player GeneratePlayer(Team team)
    {
        Player tmp;
        HeroDatabase database = (team == Team.Man)? ManHeroDatabase:WomanHeroDatabase;
        HeroInfo tmpHero = database.heroList[(int)UnityEngine.Random.Range( 0, database.heroList.Count )];
        Deck tmpDeck = new();
        tmpDeck.deckList = tmpHero.premadeDecks[(int)UnityEngine.Random.Range(0, tmpHero.premadeDecks.Count)].deckList;
        tmp = new(
            tmpHero,
            tmpDeck,
            healthAmount
        );
        return tmp;
    }
    
    public void SpawnMinion(CardInfo card, LaneInfo laneInfo)
    {
        MinionInfo newMinion = new(card);
        laneInfo.minionsInLane.Add(newMinion);
        UIManager.Instance.SpawnMinion(newMinion, laneInfo);
    }
    public void SpawnMinionEvent(PointerEventData eventData, LaneInfo laneInfo)
    {
        GameObject dropped = eventData.pointerDrag;
        CardInfo droppedCard = dropped.GetComponent<CardDisplay>().card;
        MinionInfo newMinion = new(droppedCard);
        laneInfo.minionsInLane.Add(newMinion);
        UIManager.Instance.SpawnMinion(newMinion, laneInfo);
        Destroy(dropped);

        if (droppedCard.cardTeam == Team.Man) manPlayer.mana -= droppedCard.cost;
        else womanPlayer.mana -= droppedCard.cost;
        UIManager.Instance.UpdateHeroes();
    }
    public bool ResolveBattle()
    {
        foreach ( var lane in lanes )
        {
            if ( ResolveLane(lane) ) return true;
        }
        return false;
    }
    private bool ResolveLane(LaneInfo lane)
    {
        List<MinionInfo> manMinions = new();
        List<MinionInfo> womanMinions = new();
        // init minions in team
        foreach( var minion in lane.minionsInLane )
        {
            if ( minion.card.cardTeam == Team.Man)
                manMinions.Add(minion);
            else 
                womanMinions.Add(minion);
        }


        if ( womanMinions.Count == 0)
        {
            foreach ( var minion in manMinions )
                minion.Attack(womanPlayer);
        }
        else
        {
            foreach ( var minion in manMinions )
                minion.Attack(womanMinions[0]);
        }
        UIManager.Instance.UpdateHeroes();
        Debug.Log("It is a good day to be NOT dead!");
        if ( DidHeroDie() ) return true;

        if ( manMinions.Count == 0)
        {
            foreach ( var minion in womanMinions )
            {
                minion.Attack(manPlayer);
                //UIManager.Instance.
            }
        }
        else
        {
            foreach ( var minion in womanMinions )
                minion.Attack(manMinions[0]);
        }

        UIManager.Instance.UpdateHeroes();
        if ( DidHeroDie() ) return true;
        CleanUpLane(lane);
        return false;
    }
    public Player GetWinner()
    {
        Debug.Log("Choosing winner...");
        return ( womanPlayer.health <= 0 )? manPlayer:womanPlayer;
    }
    private bool DidHeroDie()
    {
        if ( womanPlayer.IsDead() || manPlayer.IsDead() )
        {
            Debug.Log("The heavy is dead!");
            GameManager.Instance.phase = GamePhase.Game_Over;
            return true;
        }
        return false;
    }
    private void CleanUpLane(LaneInfo lane)
    {
        for (int i = lane.minionsInLane.Count - 1; i >= 0; i--)
        {
            var minion = lane.minionsInLane[i];

            if (minion.IsDead())
            {
                RemoveMinion(minion, lane);
            }
        }
    }
    private void RemoveMinion(MinionInfo minion, LaneInfo lane)
    {
        lane.minionsInLane.Remove(minion);

        UIManager.Instance.RemoveMinion(minion);
    }
    public void DrawCards()
    {
        manPlayer.DrawCard();
        womanPlayer.DrawCard();
        if ( SessionChoices.Instance.chosenHero.team == Team.Man)
            UIManager.Instance.AddCard(manPlayer.hand[manPlayer.hand.Count-1]);
        else
            UIManager.Instance.AddCard(womanPlayer.hand[womanPlayer.hand.Count-1]);
        ResolveAbilities(AbilityType.Card_Drawn);
    }
    public void ResolveAbilities(AbilityType type)
    {
        
    }
    public void GiveMana ( int mana )
    {
        womanPlayer.mana = mana;
        manPlayer.mana = mana;
        UIManager.Instance.UpdateHeroes();
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private Button turnButton;

    #region Parents
    [SerializeField] private GameObject cardsParent;
    [SerializeField] private GameObject minions;
    [SerializeField] private GameObject lanes;
    [SerializeField] private GameObject playerPosition;
    [SerializeField] private GameObject enemyPosition;
    #endregion
    
    #region Prefab refs
    [SerializeField] private LaneDisplay referenceLane;
    [SerializeField] private CardDisplay referenceCard;
    [SerializeField] private HeroDisplay referenceHero;
    [SerializeField] private MinionDisplay referenceMinion;
    #endregion

    #region Lists
    private HeroDisplay player;
    private HeroDisplay enemy;
    private List<CardDisplay> handCards;
    private List<LaneDisplay> laneList;
    private List<MinionDisplay> minionList;
    #endregion

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        laneList = new();
        handCards = new();
        minionList = new();
    }
    public void DrawHeroes()
    {
        if ( SessionChoices.Instance.chosenHero.team == Team.Man )
        {
            player = Instantiate(referenceHero, playerPosition.transform);
            player.Initialize(BoardManager.Instance.manPlayer);
            enemy = Instantiate(referenceHero, enemyPosition.transform);
            enemy.Initialize(BoardManager.Instance.womanPlayer);
        }
        else
        {
            player = Instantiate(referenceHero, playerPosition.transform);
            player.Initialize(BoardManager.Instance.womanPlayer);
            enemy = Instantiate(referenceHero, enemyPosition.transform);
            enemy.Initialize(BoardManager.Instance.manPlayer);
        }
    }

    public void InitializeHand(List<CardInfo> cards)
    {
        foreach ( CardInfo card in cards )
        {
            CardDisplay newCard = Instantiate(referenceCard, cardsParent.transform );
            newCard.Initialize(card);
            handCards.Add(newCard);
        }
    }
    public void AddCard(CardInfo card)
    {
        CardDisplay newCard = Instantiate(referenceCard, cardsParent.transform );
        newCard.Initialize(card);
        handCards.Add(newCard);
    }
    public void CreateLanes ( List<LaneInfo> lanesToCreate )
    {
        foreach ( var lane in lanesToCreate )
        {
            LaneDisplay newLane = Instantiate(referenceLane, lanes.transform );
            newLane.Initialize(lane);
            laneList.Add(newLane);
        }
    }
    public void SpawnMinion(MinionInfo minionInfo, LaneInfo lane)
    {
        foreach ( var ld in laneList )
        {
            if ( ld.laneInfo.laneID == lane.laneID )
            {
                MinionDisplay minion = Instantiate(referenceMinion, ld.dropBox.transform);
                minion.Initialize(minionInfo);
                minionList.Add(minion);
                minionInfo.minionDisplay = minion.gameObject;
                break;
            }
        }
        HideDropBoxes();
    }
    public void RemoveMinion(MinionInfo minion)
    {
        Destroy(minion.minionDisplay);
    }
    public void ShowTrickTarget (Team team)
    {
        foreach ( var minion in minionList )
        {
            if ( minion.minion.card.cardTeam == team )
                minion.ShowTrickTarget();
        }
    }
    public void HideTrickTarget ()
    {
        foreach ( var minion in minionList )
        {
            minion.HideTrickTarget();
        }
    }
    public void ShowDropBoxes()
    {
        foreach ( var lane in laneList )
        {
            lane.ShowDropBox();
        }
    }
    public void HideDropBoxes()
    {
        foreach ( var lane in laneList )
        {
            lane.HideDropBox();
        }
    }
    public void UpdateTurnButton()
    {
        switch(GameManager.Instance.phase)
        {
            case GamePhase.Man_Minions:
                if (SessionChoices.Instance.chosenTeam == Team.Man)
                {
                    turnButton.interactable = true;
                    turnButton.image.color = Color.limeGreen;
                } 
                else
                {
                    turnButton.interactable = false; 
                }
                break;
            case GamePhase.Woman_play:
                if (SessionChoices.Instance.chosenTeam == Team.Woman)
                {
                    turnButton.interactable = true;
                    turnButton.image.color = Color.limeGreen;
                } 
                else
                {
                    turnButton.interactable = false; 
                }
                break;
            case GamePhase.Man_Tricks:
                if (SessionChoices.Instance.chosenTeam == Team.Man)
                {
                    turnButton.interactable = true;
                    turnButton.image.color = Color.limeGreen;
                } 
                else
                {
                    turnButton.interactable = false; 
                }
                break;
            default:

                break;
        }
    }
    public void UpdateHeroes()
    {
        player.UpdateHealth();
        enemy.UpdateHealth();
    }
}

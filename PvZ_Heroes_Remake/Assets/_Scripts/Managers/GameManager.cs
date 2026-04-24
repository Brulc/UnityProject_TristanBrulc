using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GamePhase phase;
    public event Action<Player> OnGameOver;
    public int turnCount;
    public IPlayerController manPlay;
    public IPlayerController womanPlay;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        turnCount = 0;
    }
    void Start()
    {
        BoardManager.Instance.Initialize();
        phase = GamePhase.Start_Of_Turn;
        Gameloop();
    }
    public void AdvanceTurn()
    {
        int next = ((int)phase + 1) % (Enum.GetValues(typeof(GamePhase)).Length - 1 );
        phase = (GamePhase)next;
        //Debug.Log(phase);
        UIManager.Instance.UpdateTurnButton();
        Gameloop();
    }

    public void Gameloop()
    {
        switch(phase)
        {
            case GamePhase.Start_Of_Turn:
                turnCount++;
                Debug.Log(phase);
                BoardManager.Instance.GiveMana(turnCount);
                BoardManager.Instance.DrawCards();
                AdvanceTurn();
                break;

            case GamePhase.Man_Minions:
                Debug.Log(phase);
                manPlay.Play();
                break;

            case GamePhase.Woman_play:
                Debug.Log(phase);
                womanPlay.Play();
                //AdvanceTurn();      // tmp solution
                break;

            case GamePhase.Man_Tricks:
                Debug.Log(phase);
                manPlay.Play();
                break;

            case GamePhase.Battle:
                Debug.Log(phase);
                if ( BoardManager.Instance.ResolveBattle() ) Gameloop();
                AdvanceTurn();
                break;

            case GamePhase.End_Of_Turn:
                Debug.Log(phase);
                AdvanceTurn();
                break;

            case GamePhase.Game_Over:
                Debug.Log("Game over");
                OnGameOver?.Invoke( BoardManager.Instance.GetWinner() );
                break;

            default: break;
        }
    }

    public bool[] GetCurrentPlayability(CardInfo card)
    {
        switch(phase)
        {
            case GamePhase.Man_Minions:
                if ( card.cardTeam == Team.Man && card.type == CardType.Minion )
                    return new bool[] {true, false};
                break;
            case GamePhase.Woman_play:
                if ( card.cardTeam == Team.Woman )
                    return new bool[] {true, true};
                break;
            case GamePhase.Man_Tricks:
                if ( card.cardTeam == Team.Man && card.type == CardType.Trick )
                    return new bool[] {false, true};
                break;
            default:
                return new bool[] {false, false};  
        }
        return new bool[] {false, false};
    }
}

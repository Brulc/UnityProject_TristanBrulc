using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GamePhase phase;
    public event Action<Player> OnGameOver;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        BoardManager.Instance.Initialize();
        phase = GamePhase.Man_Minions;
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
                Debug.Log(phase);
                BoardManager.Instance.DrawCards();
                AdvanceTurn();
                break;

            case GamePhase.Man_Minions:
                Debug.Log(phase);
                break;

            case GamePhase.Woman_play:
                Debug.Log(phase);
                AdvanceTurn();      // tmp solution
                break;

            case GamePhase.Man_Tricks:
                Debug.Log(phase);
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

    
}

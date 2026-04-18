using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GamePhase phase;

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
        int next = ((int)phase + 1) % System.Enum.GetValues(typeof(GamePhase)).Length;
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
                AdvanceTurn();
                break;
            case GamePhase.Man_Tricks:
                Debug.Log(phase);
                break;
            case GamePhase.Battle:
                Debug.Log(phase);
                BoardManager.Instance.ResolveBattle();
                AdvanceTurn();
                break;
            case GamePhase.End_Of_Turn:
                Debug.Log(phase);
                AdvanceTurn();
                break;
            default: break;
        }
    }
    
}

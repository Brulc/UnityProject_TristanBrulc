using UnityEngine;


public class SessionChoices : MonoBehaviour
{
    public static SessionChoices Instance;
    public Team chosenTeam;
    public HeroInfo chosenHero;
    [SerializeField] private HeroInfo defaultManHero;
    [SerializeField] private HeroInfo defaultWomanHero;
    public Deck deck;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        deck = new();
        ChooseManTeam();
    }
    public void ChooseWomanTeam() 
    { 
        chosenTeam = Team.Woman; 
        chosenHero = defaultWomanHero; 
        deck = new(defaultWomanHero.premadeDecks[0].deckList); 
    }
    public void ChooseManTeam() 
    { 
        chosenTeam = Team.Man;
        chosenHero = defaultManHero;
        deck = new(defaultManHero.premadeDecks[0].deckList); 
    }
}

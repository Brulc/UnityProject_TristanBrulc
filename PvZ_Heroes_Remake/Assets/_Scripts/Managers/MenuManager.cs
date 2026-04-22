using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    /*
    * 0 main menu
    * 1 battle menu
    * 2 deck builder
    * 3 battle scene
    */
    const int mainMenu = 0;
    const int battleMenu = 1;
    const int deckBuilder = 2;
    const int battleScene = 3;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void ToBattleMenu()
    {
        SceneManager.LoadScene(battleMenu); // load battle menu
    }
    public void StartGame()
    {
        SceneManager.LoadScene(battleScene); // load battle
    }
    public void SettingsMenu()
    {
        
    }
    public void DeckBuilder()
    {
        SceneManager.LoadScene(deckBuilder);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

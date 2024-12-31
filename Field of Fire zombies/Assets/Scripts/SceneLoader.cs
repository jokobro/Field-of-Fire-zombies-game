using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ReturnMainmenu()
    {
        creditsPanel.SetActive(false);
    }

    public void loadCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }

    public void EndGame()
    {
        //toevoegen van de logica wanneer speler op endgame knopt drukt
    }
}

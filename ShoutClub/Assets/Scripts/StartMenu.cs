using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button startGame;
    [SerializeField] private Button quit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        startGame.onClick.AddListener(StartGame);
        quit.onClick.AddListener(Quit);
    }

    void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    void Quit()
    {
        Application.Quit();
    }
}

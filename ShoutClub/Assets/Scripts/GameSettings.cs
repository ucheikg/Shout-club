using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameSettings : MonoBehaviour
{
    [SerializeField] private Button resume;
    [SerializeField] private Button quitGame;
    [SerializeField] private GameObject pauseScreen;
    bool isPaused;

    void Start()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        resume.onClick.AddListener(QuitGame);
        quitGame.onClick.AddListener(Resume);

        if (Input.GetKey(KeyCode.Escape))
        {

            if (!isPaused)
            {
                pauseScreen.SetActive(true);
                Time.timeScale = 0f;
                isPaused = true;
            }
            if (isPaused)
            {
                pauseScreen.SetActive(false);
                Time.timeScale = 1f;
                isPaused = false;
            }
        }
    }
    
    public void QuitGame()
    {
        NetworkManager.Singleton.Shutdown();
        SceneManager.LoadScene("Start Menu");
    }

    public void Resume()
    {

        if (isPaused)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }

    }


}

using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    void Start()
    {
        Invoke("ReturnToMenu", 5f);
    }


    void ReturnToMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }
}

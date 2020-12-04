using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    void Start()
    {
        if (UIManager.Instance == null)
        {
            SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        }
    }
}
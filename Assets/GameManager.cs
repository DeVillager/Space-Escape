using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public GameObject levelLoadScreen;
    public GameObject pauseMenu;
    public Animator anim;
    private PlayerInput input;
    public bool gamePaused;

    [SerializeField] private GameObject firstInPauseMenu;
    // public TextMeshProUGUI dialogText;
    // public TextMeshProUGUI dialogName;

    protected override void Awake()
    {
        base.Awake();
        levelLoadScreen.SetActive(true);
        // ShadeOut();
    }

    private void OnEnable()
    {
        // DontDestroyOnLoad(gameObject);
        // SceneManager.sceneLoaded += OnSceneLoaded;
        input = Player.Instance.controller.input;
        input.Player.Pause.performed += HandlePause;
    }

    private void OnDisable()
    {
        // SceneManager.sceneLoaded -= OnSceneLoaded;
        input.Player.Pause.performed -= HandlePause;
    }

    private void HandlePause(InputAction.CallbackContext obj)
    {
        if (!gamePaused)
        {
            Paused();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenu.SetActive(false);
        }
        gamePaused = !gamePaused;
    }
    
    public void Paused()
    {
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstInPauseMenu);
        //todo: Do not use timescale = 0 or 1
        Time.timeScale = 0;
    }
    
    public void Continue()
    {
        gamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 1;
    }
    
    public void Options()
    {
        Debug.Log("Options");
    }
    
    public void MainMenu()
    {
        Debug.Log("Loading Main Menu");
        SceneManager.LoadScene("MainMenu");
    }
    
    public void Quit()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
    
    
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     // ShadeOut();
    // }
    //
    // private void ShadeOut()
    // {
    //     // anim.SetTrigger("ShadeOut");
    // }
    //
    // private void ShadeIn()
    // {
    // }
}
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
    public GameObject optionsMenu;
    public GameObject helpPage;
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
    }

    private void OnEnable()
    {
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
        if (gamePaused)
            Continue();
        else
            Pause();
    }
    
    public void Pause()
    {
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstInPauseMenu);
        Time.timeScale = 0;
    }
    
    public void Continue()
    {
        gamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 1;

        // Hide all menus on continue
        optionsMenu.SetActive(false);
        helpPage.SetActive(false);
    }
    
    public void Options()
    {
        if (optionsMenu.activeSelf)
        {
            Debug.Log("Closing Options");
            optionsMenu.SetActive(false);
        }
        else
        {
            Debug.Log("Opening Options");
            optionsMenu.SetActive(true);

            // Disable others
            helpPage.SetActive(false);
        }
    }

    public void HelpPage()
    {
        if (helpPage.activeSelf)
        {
            Debug.Log("Closing Help");
            helpPage.SetActive(false);
        }
        else
        {
            Debug.Log("Opening Help");
            helpPage.SetActive(true);

            // Disable others
            optionsMenu.SetActive(false);
        }
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
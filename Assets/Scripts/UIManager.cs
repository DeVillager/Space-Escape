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
    public GameObject creditsPage;
    public GameObject confirmQuit;
    
    private PlayerInput input;
    public bool gamePaused;

    [SerializeField] private GameObject firstInPauseMenu;
    // public TextMeshProUGUI dialogText;
    // public TextMeshProUGUI dialogName;
    public Animator anim;

    private List<GameObject> pages;
    
    public void StartGame()
    {
        // levelLoadScreen.SetActive(true);
        SceneManager.LoadScene("GameTommi");
    }
    
    protected override void Awake()
    {
        base.Awake();
        // levelLoadScreen.SetActive(true);
        pages = new List<GameObject> {optionsMenu, helpPage, creditsPage, confirmQuit};
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;  
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstInPauseMenu);
        Time.timeScale = 0;
    }
    
    public void Continue()
    {
        gamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 1;

        // Hide all menus on continue
        foreach (GameObject p in pages)
        {
            p.SetActive(false);
        }
    }

    public void OpenPage(GameObject page)
    {
        if (page.activeSelf)
        {
            Debug.Log("Closing " + page.name);
            page.SetActive(false);
        }
        else
        {
            Debug.Log("Opening " + page.name);
            // Disable all, then enable page
            foreach (GameObject p in pages)
            {
                p.SetActive(false);
            }
            page.SetActive(true);
        }    
    }
    
    public void Options()
    {
        OpenPage(optionsMenu);
    }

    public void HelpPage()
    {
        OpenPage(helpPage);
    }
    
    public void CreditsPage()
    {
        OpenPage(creditsPage);
    }
    
    
    public void MainMenu()
    {
        Debug.Log("Loading Main Menu");
        SceneManager.LoadScene("MainMenu");
    }
    
    public void ConfirmQuit()
    {
        OpenPage(confirmQuit);
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ShadeOut();
    }
    
    public void ShadeOut()
    {
        anim.SetTrigger("ShadeOut");
    }
    
    public void ShadeIn()
    {
        anim.SetTrigger("ShadeIn");
    }
}
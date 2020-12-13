using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public GameObject levelLoadScreen;
    // public GameObject pauseMenu;
    public Animator anim;
    private PlayerInput input;
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
        SceneManager.sceneLoaded += OnSceneLoaded;
        input = Player.Instance.controller.input;
        input.Player.Cancel.performed += HandlePause;
        input.Player.Quit.performed += Quit;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        input.Player.Cancel.performed -= HandlePause;
        input.Player.Quit.performed -= Quit;
    }
    
    // For debugging
    private void Quit(InputAction.CallbackContext obj)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Quitting game");
        Application.Quit();
    }

    // TODO Make pause open up PauseMenu, that has Restart etc. instead
    private void HandlePause(InputAction.CallbackContext obj)
    {
        // Restart();
    }
    
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ShadeOut();
    }

    private void ShadeOut()
    {
        // anim.SetTrigger("ShadeOut");
    }

    private void ShadeIn()
    {
    }
}
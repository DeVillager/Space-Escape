using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuUI : Singleton<MainMenuUI>
{
    public GameObject levelLoadScreen;
    
    public GameObject optionsMenu;
    public GameObject helpPage;
    public GameObject creditsPage;
    public GameObject confirmQuit;
    
    private PlayerInput input;

    [SerializeField] private GameObject firstInPauseMenu;
    public Animator anim;
    public List<GameObject> pages;
    
    public void StartGame()
    {
        // levelLoadScreen.SetActive(true);
        SceneManager.LoadScene("GameTommi");
    }

    protected override void Awake()
    {
        base.Awake();
        pages.Add(optionsMenu);
        pages.Add(helpPage);
        pages.Add(creditsPage);
        pages.Add(confirmQuit);
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
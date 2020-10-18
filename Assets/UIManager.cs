using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public GameObject levelLoadScreen;
    public Animator anim;

    protected override void Awake()
    {
        base.Awake();
        levelLoadScreen.SetActive(true);
        // ShadeOut();
    }

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
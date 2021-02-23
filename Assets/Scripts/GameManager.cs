using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //Change to proper LightManager etc
    public GameObject[] levelLights;

    // [Serializable]
    // private struct LightList
    // {
    //     public GameObject[] Lights;
    // } 
    
    void Start()
    {
        if (UIManager.Instance == null)
        {
            SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        }
        LevelLightsOff();
    }

    public void LevelLightsOn()
    {
        SwitchLights(levelLights, true);
    }
    
    public void LevelLightsOff()
    {
        SwitchLights(levelLights, false);
    }

    public void SwitchLights(GameObject[] lights, bool lightOn)
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(lightOn);
        }
    }

    public void ShadeOut()
    {
        UIManager.Instance.ShadeOut();
    }
    
    public void ShadeIn()
    {
        UIManager.Instance.ShadeIn();
    }
}
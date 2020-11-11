using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    // TODO Fix commented issues
    //panels
    public GameObject pauseMenu;
    public GameObject dialoguePanel;
    public GameObject optionsPanel;
    public GameObject controlsPanel;

    public GameObject shadeScreen;
    // public Slider musicSlider;
    // public Slider soundSlider;

    //UI elements
    public GameObject firstInPauseMenu;
    public GameObject firstInMainMenu;
    public Button dialogueButton;
    public GameObject firstInOptions;
    public GameObject optionsButton;
    public GameObject firstInControls;
    private TextMeshProUGUI shadeScreenText;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstInMainMenu);
        // shadeScreenText = shadeScreen.GetComponentInChildren<TextMeshProUGUI>();
        // dialogueButton.onClick.AddListener(DialogueManager.Instance.DisplayNextSentence);
        SetSliderValues();
    }

    private void SetSliderValues()
    {
        // musicSlider.value = AudioSettings.Instance.MusicVolume;
        // soundSlider.value = AudioSettings.Instance.SFXVolume;
    }

    public void NewGame()
    {
        SceneManager.LoadScene("First Level");
    }

    public void Pause()
    {
        if (dialoguePanel.activeInHierarchy)
        {
            return;
        }

        //If game isnt paused
        // if (!GameManager.Instance.gamePaused)
        // {
        //     PauseGame();
        // }
        //If game was paused, return to game or move to previous panel
        else
        {
            if (pauseMenu.activeInHierarchy)
            {
                Resume();
            }
            else if (optionsPanel.activeInHierarchy)
            {
                GoToPauseMenu();
            }
            else if (controlsPanel.activeInHierarchy)
            {
                GoToOptions();
            }
        }
    }

    private void PauseGame()
    {
        // GameManager.Instance.gamePaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstInPauseMenu);
    }

    public void Resume()
    {
        // GameManager.Instance.gamePaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ShowDialogue()
    {
        Time.timeScale = 0;
        dialoguePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(dialogueButton.gameObject);
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        // GameManager.Instance.Restart();
    }

    public void Quit()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void Save()
    {
        // SaveManager.Instance.Save();
        Resume();
    }

    public void Load()
    {
        Time.timeScale = 1;
        // GameManager.Instance.loadData = true;
        // GameManager.Instance.Restart();
    }

    public IEnumerator LoadDelayed(float time)
    {
        yield return new WaitForSeconds(time);
        Load();
    }

    public IEnumerator LoadSceneDelayed(string level, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(level);
    }

    private void GoToOptions()
    {
        optionsPanel.SetActive(true);
        pauseMenu.SetActive(false);
        controlsPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstInOptions);
    }

    private void GoToPauseMenu()
    {
        optionsPanel.SetActive(false);
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(optionsButton);
    }

    public void GoToMainMenu()
    {
        // GameManager.Instance.gamePaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToControls()
    {
        optionsPanel.SetActive(false);
        controlsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstInControls);
    }

    public void MusicOnOff()
    {
    }

    public void ShadeIn()
    {
        // shadeScreen.GetComponentInChildren<TextMeshProUGUI>().text = "";
        shadeScreen.GetComponent<Animator>().SetTrigger("ShadeIn");
    }

    public void ShadeOut(string nextLevel)
    {
        shadeScreenText.text = nextLevel;
        shadeScreen.GetComponent<Animator>().SetTrigger("ShadeOut");
    }

    public void MusicVolumeLevel(float newMusicVolume)
    {
        // AudioSettings.Instance.MusicVolumeLevel(newMusicVolume);
    }

    public void SFXVolumeLevel(float newSFXVolume)
    {
        // AudioSettings.Instance.SFXVolumeLevel(newSFXVolume);
    }
}
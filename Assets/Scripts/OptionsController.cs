using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsController : MonoBehaviour
{
    [Header("FOV")]
    public Slider fovSlider;
    public TMP_InputField fovInputField;

    public float Fov
    {
        get => fov;
        set
        {
            value = Mathf.Clamp(value, fovMin, fovMax);
            fov = value;
            SetFov(value);
        }
    }
    private float fov = 80f;
    private float fovMin = 60f;
    private float fovMax = 110f;

    [Header("Mouse Sensitivity")]
    public Slider sensSlider;
    public TMP_InputField sensInputField;

    public float MouseSensitivity
    {
        get => mouseSensitivity;
        set
        {
            value = Mathf.Clamp(value, mouseSensMin, mouseSensMax);
            mouseSensitivity = value;
            SetMouseSens(value);
        }
    }
    private float mouseSensitivity = 1f;
    private float mouseSensMin = 0.1f;
    private float mouseSensMax = 10f;


    private bool updatingOptions = false;

    private void OnEnable()
    {
        fovSlider.onValueChanged.AddListener(delegate { FovChanged(); });
        fovInputField.onValueChanged.AddListener(delegate { FovChanged(); });
        fovInputField.characterValidation = TMP_InputField.CharacterValidation.Integer;

        sensSlider.onValueChanged.AddListener(delegate { MouseSensChanged(); });
        sensInputField.onValueChanged.AddListener(delegate { MouseSensChanged(); });
        sensInputField.characterValidation = TMP_InputField.CharacterValidation.Decimal;



        // Get Field of View
        if (PlayerPrefs.HasKey("Option_Fov"))
            Fov = PlayerPrefs.GetFloat("Option_Fov");
        else
            if (Player.Instance != null)
            Fov = Player.Instance.controller.headCamera.fieldOfView;
        else
            Fov = 80f;

        // Get Mouse Sensitivity
        if (PlayerPrefs.HasKey("Option_MouseSensitivity"))
            MouseSensitivity = PlayerPrefs.GetFloat("Option_MouseSensitivity");
        else
            if (Player.Instance != null)
            MouseSensitivity = Player.Instance.controller.mouseLook.mouseSensitivity;
        else
            MouseSensitivity = 1f;

        // Update control values
        UpdateUIValues();
    }

    private void UpdateUIValues()
    {
        updatingOptions = true;

        fovSlider.maxValue = fovMax;
        fovSlider.minValue = fovMin;
        fovSlider.value = Fov;
        fovInputField.text = Mathf.Round(Fov).ToString();

        sensSlider.maxValue = mouseSensMax;
        sensSlider.minValue = mouseSensMin;
        sensSlider.value = MouseSensitivity;
        sensInputField.text = (Mathf.Round(MouseSensitivity * 10) / 10).ToString();

        updatingOptions = false;
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }

    private void SetFov(float value)
    {
        if (Player.Instance != null)
            Player.Instance.controller.headCamera.fieldOfView = value;

        PlayerPrefs.SetFloat("Option_Fov", value);
    }

    private void FovChanged()
    {
        if (updatingOptions)
            return;

        updatingOptions = true;

        if (!Mathf.Approximately(fovSlider.value, Fov))
        {
            Fov = Mathf.Round(fovSlider.value);
            fovInputField.text = Mathf.Round(Fov).ToString();
        }
        else if (float.TryParse(fovInputField.text, out float result) && !Mathf.Approximately(result, Fov))
        {
            Fov = Mathf.Round(result);
            fovSlider.value = Fov;
        }

        updatingOptions = false;
    }

    private void SetMouseSens(float value)
    {
        if (Player.Instance != null)
            Player.Instance.controller.mouseLook.mouseSensitivity = value;

        PlayerPrefs.SetFloat("Option_MouseSensitivity", value);
    }

    private void MouseSensChanged()
    {
        if (updatingOptions)
            return;

        updatingOptions = true;

        if (!Mathf.Approximately(sensSlider.value, MouseSensitivity))
        {
            MouseSensitivity = sensSlider.value;
            sensInputField.text = (Mathf.Round(MouseSensitivity * 10) / 10).ToString();
        }
        else if (float.TryParse(sensInputField.text, out float result) && !Mathf.Approximately(result, MouseSensitivity))
        {
            MouseSensitivity = result;
            sensSlider.value = MouseSensitivity;
        }

        updatingOptions = false;
    }
}

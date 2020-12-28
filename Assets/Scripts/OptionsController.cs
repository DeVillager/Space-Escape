using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsController : MonoBehaviour
{
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

    private bool updatingOptions = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        fovSlider.onValueChanged.AddListener(delegate { FovChanged(); });
        fovInputField.onValueChanged.AddListener(delegate { FovChanged(); });
        fovInputField.characterValidation = TMP_InputField.CharacterValidation.Integer;



        // Get Field of View
        if (PlayerPrefs.HasKey("Option_Fov"))
            Fov = PlayerPrefs.GetFloat("Option_Fov");
        else
            if (Player.Instance != null)
            Fov = Player.Instance.controller.headCamera.fieldOfView;
        else
            Fov = 80f;

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

        updatingOptions = false;
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }

    private void SetFov(float value)
    {
        if (Player.Instance != null)
            Player.Instance.controller.headCamera.fieldOfView = Fov;

        PlayerPrefs.SetFloat("Option_Fov", value);
    }

    private void FovChanged()
    {
        if (updatingOptions)
            return;

        updatingOptions = true;

        if (!Mathf.Approximately(fovSlider.value, Fov))
        {
            Fov = fovSlider.value;
            fovInputField.text = Mathf.Round(Fov).ToString();
        }
        else if (float.TryParse(fovInputField.text, out float result) && !Mathf.Approximately(result, Fov))
        {
            Fov = result;
            fovSlider.value = Fov;
        }

        updatingOptions = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlickerLight : MonoBehaviour
{
    public float minFlickerSpeed = 0.1f;
    public float maxFlickerSpeed = 1.0f;
    private Light _light;

    private void Awake()
    {
        _light = GetComponent<Light>();
    }

    private void Start()
    {
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
        _light.enabled = !_light.enabled;
        StartCoroutine(Flicker());
    }





}

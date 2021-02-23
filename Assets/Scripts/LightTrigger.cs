using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : EventTrigger
{
    
    public void LightsOn()
    {
        SwitchLights(true);
    }
    
    public void LightsOff()
    {
        SwitchLights(false);
    }

    private void SwitchLights(bool lightOn)
    {
        foreach (Transform light in transform)
        {
            light.gameObject.SetActive(lightOn);
        }
    }
    
    
}

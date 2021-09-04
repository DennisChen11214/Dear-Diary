using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

//Creates a pulsing light effect on an object to indicate that it should be picked up
public class LightPulse : MonoBehaviour
{
    private Light2D pulseLight;
    [SerializeField] float intensity;

    void Start()
    {
        pulseLight = GetComponent<Light2D>();
        //The light pulses every 2.5 seconds
        InvokeRepeating("Pulse", 0, 2.5f);    
    }

    void Pulse(){
        StartCoroutine("LightPulsing");
    }

    //Interpolates the intensity of the light on and off
    IEnumerator LightPulsing(){
        float aTime = 1.0f;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            pulseLight.intensity = Mathf.Lerp(0,intensity, t);
            yield return null;
        }
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            pulseLight.intensity = Mathf.Lerp(intensity,0, t);
            yield return null;
        }
    }
}

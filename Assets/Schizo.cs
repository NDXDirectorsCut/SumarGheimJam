using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Schizo : MonoBehaviour
{
    public bool toggle;
    public Camera camScript;

    public float effectLerp;
    public float faceSpeed;
    public Vector2 faceMinMax;
    public PostProcessVolume ppVolume;

    public Bloom bloomEffect;
    public ChromaticAberration chromaEffect;
    public Grain grainEffect;
    public Vignette vignetteEffect;
    public ColorGrading colorEffect;


    //public Bloom bloomEffect;
    // Start is called before the first frame update
    void Start()
    {
        ppVolume.profile.TryGetSettings(out bloomEffect);
        ppVolume.profile.TryGetSettings(out chromaEffect);
        ppVolume.profile.TryGetSettings(out grainEffect);
        ppVolume.profile.TryGetSettings(out vignetteEffect);
        ppVolume.profile.TryGetSettings(out colorEffect);
    }

    // Update is called once per frame
    void Update()
    {
        float sinZeroOne = (Mathf.Sin(Time.time)/2)+.5f;
        

        if(toggle == false)
        {
            Debug.Log(bloomEffect.intensity.value);
            bloomEffect.intensity.value = Mathf.Lerp(bloomEffect.intensity.value,0,effectLerp*Time.deltaTime*60);
            chromaEffect.intensity.value = Mathf.Lerp(chromaEffect.intensity.value,0,effectLerp*Time.deltaTime*60);
            grainEffect.intensity.value = Mathf.Lerp(grainEffect.intensity.value,0,effectLerp*Time.deltaTime*60);
            vignetteEffect.intensity.value = Mathf.Lerp(vignetteEffect.intensity.value,0,effectLerp*Time.deltaTime*60);
            colorEffect.saturation.value = Mathf.Lerp(colorEffect.saturation.value,0,effectLerp*Time.deltaTime*60);
            colorEffect.contrast.value = Mathf.Lerp(colorEffect.contrast.value,0,effectLerp*Time.deltaTime*60);
            camScript.shake = Vector2.Lerp(camScript.shake,Vector2.zero,effectLerp*Time.deltaTime*60);
            //camScript.shakeSpeed = Vector2.Lerp(camScript.shakeSpeed,Vector2.zero,effectLerp*Time.deltaTime*60*2);
        }
        if(toggle == true)
        {
            bloomEffect.dirtIntensity.value = Mathf.Lerp(faceMinMax.x,faceMinMax.y,sinZeroOne*faceSpeed);

            bloomEffect.intensity.value = Mathf.Lerp(bloomEffect.intensity.value,1,effectLerp*Time.deltaTime*60/2);
            chromaEffect.intensity.value = Mathf.Lerp(chromaEffect.intensity.value,.8f,effectLerp*Time.deltaTime*60);
            grainEffect.intensity.value = Mathf.Lerp(grainEffect.intensity.value,.166f,effectLerp*Time.deltaTime*60/4);
            vignetteEffect.intensity.value = Mathf.Lerp(vignetteEffect.intensity.value,.396f,effectLerp*Time.deltaTime*60);
            colorEffect.saturation.value = Mathf.Lerp(colorEffect.saturation.value,-48,effectLerp*Time.deltaTime*60);
            colorEffect.contrast.value = Mathf.Lerp(colorEffect.contrast.value,6,effectLerp*Time.deltaTime*60);
            camScript.shake = Vector2.Lerp(camScript.shake,new Vector2(.1f,.2f),effectLerp*Time.deltaTime*60);
            camScript.shakeSpeed = Vector2.Lerp(camScript.shakeSpeed,new Vector2(5,2.5f),effectLerp*Time.deltaTime*60*2);
        }

    }
}

using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager instance;

    [Header("References")]
    public Volume globalVolume;

    private Vignette vignette;
    private Bloom bloom;
    private ColorAdjustments colorAdjustments;
    private ChromaticAberration chromaticAberration;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (globalVolume.profile.TryGet(out vignette))
        {
            //Debug.Log("Vignette effect found in the Volume Profile!");
        }
        else
        {
            Debug.LogError("Vignette effect not found in the Volume Profile!");
        }

        if (globalVolume.profile.TryGet(out bloom))
        {
            Debug.Log("Bloom effect found in the Volume Profile!");
        }
        else
        {
            //Debug.LogError("Bloom effect not found in the Volume Profile!");
        }
        if (globalVolume.profile.TryGet(out colorAdjustments))
        {
            //Debug.Log("Color Adjustments effect found in the Volume Profile!");
        }
        else
        {
            Debug.LogError("Color Adjustments effect not found in the Volume Profile!");
        }
        if (globalVolume.profile.TryGet(out chromaticAberration))
        {

        }
        else
        {
            Debug.LogError("Color Adjustments effect not found in the Volume Profile!");
        }
    }

    public void SetVignetteIntensity(float intensity)
    {
        if (vignette != null)
        {
            vignette.intensity.value = intensity;
        }
    }
    public void SetBloomIntensity(float intensity)
    {
        if (bloom != null)
        {
            bloom.intensity.value = intensity;
        }
    }
    public void SetPostExposureIntensity(float intensity)
    {

    }
    public IEnumerator PlayerGetHitEffect(UnityEngine.Color startColor, UnityEngine.Color endColor, float damageReceived, float fadeDuration)
    {
        vignette.intensity.value = 0f;
        float vignetteEndIntensity = damageReceived / 100f;

        if (startColor == endColor)
        {
            vignette.color.value = startColor;
        }
        while (vignette.intensity.value < vignetteEndIntensity)
        {
            vignette.intensity.value += 0.05f;
            yield return null;
        }
        vignette.intensity.value = vignetteEndIntensity;
        yield return new WaitForSeconds(fadeDuration);

        while (vignette.intensity.value > 0f)
        {
            vignette.intensity.value -= 0.01f;
            yield return null;
        }
        vignette.intensity.value = 0f;
    }
    public IEnumerator KaBoomEffect(float fadeDuration)
    {
        colorAdjustments.postExposure.value = 0f;
        while (colorAdjustments.postExposure.value < 20f)
        {
            colorAdjustments.postExposure.value += 0.5f;
            yield return null;
        }
        colorAdjustments.postExposure.value = 20f;
        yield return new WaitForSeconds(fadeDuration);

        while (colorAdjustments.postExposure.value > 0f)
        {
            colorAdjustments.postExposure.value -= 0.5f;
            yield return null;
        }
        colorAdjustments.postExposure.value = 0f;
    }
    public IEnumerator PlayerSpeedBuffEffect(float fadeDuration)
    {
        chromaticAberration.intensity.value = 0f;
        while (chromaticAberration.intensity.value < 1f)
        {
            chromaticAberration.intensity.value += 0.1f;
            Debug.Log("Increasing!");
            yield return null;
        }
        chromaticAberration.intensity.value = 1f;
        yield return new WaitForSeconds(fadeDuration);

        while (chromaticAberration.intensity.value > 0f)
        {
            chromaticAberration.intensity.value -= 0.1f;
            Debug.Log("Decreasing!");
            yield return null;
        }
        chromaticAberration.intensity.value = 0f;
    }
}

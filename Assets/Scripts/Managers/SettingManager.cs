using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SettingManager : MonoBehaviour
{
    public AudioMixerSnapshot whenPaused, whenStarted;
    private bool paused;
    public AudioMixer audioMixer;
    public AudioSource buttonSFX;

    public TMP_Dropdown resolutionDropdown;

    public float fadeTime = 1f;
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;
    public float topPosY, middlePosY;
    public float tweenDuration;


    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void Update()
    {
        //StartPauseBGM();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetAllVolume(float volume)
    {
        audioMixer.SetFloat("ALLMusic", Mathf.Log10(volume) * 32);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("BGMusic", Mathf.Log10(volume) * 32);
    }

    public void SetButton(float volume)
    {
        audioMixer.SetFloat("Button", Mathf.Log10(volume) * 32);
    }
    
    public void SetCheering(float volume)
    {
        audioMixer.SetFloat("Cheering", Mathf.Log10(volume) * 32);
    }

    public void SetEnvironment(float volume)
    {
        audioMixer.SetFloat("Environment", Mathf.Log10(volume) * 32);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void StartPauseBGM()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;

            if (paused)
            {
                whenPaused.TransitionTo(2);
            }
            else
            {
                whenStarted.TransitionTo(2);
            }
        }
    }

    public void ButtonSFX()
    {
        buttonSFX.Play();
    }

    public void PanelFadeIn()
    {
        canvasGroup.alpha = 0f;
        rectTransform.transform.localPosition = new Vector3(0f, -1000f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutElastic);
        canvasGroup.DOFade(1, fadeTime);
    }

    public void PanelFadeOut()
    {
        canvasGroup.alpha = 1f;
        rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, -1000f), fadeTime, false).SetEase(Ease.InOutQuint);
        canvasGroup.DOFade(0, fadeTime);
    }
}

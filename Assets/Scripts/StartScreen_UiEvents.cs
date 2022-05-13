using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen_UiEvents : MonoBehaviour
{
    [SerializeField]
    RectTransform settingDialog;
    [SerializeField]
    Slider volumeSlider;

    public void soundLevel()
    {
        AudioListener.volume = volumeSlider.value;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowSettingsPanel()
    {
        settingDialog.gameObject.SetActive(true);
    }

    public void CloseSettingPanel()
    {
        settingDialog.gameObject.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public GameObject menu, setting;

    public Dropdown resolutionsDropdown;
    Resolution[] resolutions;
    bool IsUiActive;

    public AudioMixer audioMixer;

    private void Start()
    {

        #region Resolution Setting

        int reslutionIndex = 0;
        resolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();
        List<string> resolutionString = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string item = resolutions[i].width + "x" + resolutions[i].height;
            resolutionString.Add(item);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                reslutionIndex = i;

        }
        resolutionsDropdown.AddOptions(resolutionString);
        resolutionsDropdown.value = reslutionIndex;
        resolutionsDropdown.RefreshShownValue();

        #endregion

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            setting.SetActive(false);
            menu.SetActive(true);
        }
    }

    public void SetQuality(int Qualityindex)
    {
        QualitySettings.SetQualityLevel(Qualityindex);
    }

    public void SetResolution(int Index)
    {
        Resolution tempresolution = resolutions[Index];
        Screen.SetResolution(tempresolution.width, tempresolution.height, Screen.fullScreen);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void ExitScreen()
    {
        Application.Quit();
    }

    public void GameLodeScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

}

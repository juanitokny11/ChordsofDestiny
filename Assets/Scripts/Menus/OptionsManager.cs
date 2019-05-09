using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsManager : MonoBehaviour {

	Resolution[] resolutions;
    Resolution[] screenRes;
    List<string> options;
    int currentResolutionIndex = 0;
    public TMP_Dropdown resolutionDropdown;
    public int currentcontrol = 0;
    public GameObject[] controls;
    public Text texto;

    void Start(){
        Screen.fullScreen = false;
		Resolution[] screenRes = Screen.resolutions;

		resolutions = new Resolution[4];

		resolutionDropdown.ClearOptions();

		List<string> options = new List<string>();
		for(int i= 0;i< screenRes.Length;i++){

			string option = screenRes[i].width +"x"+ screenRes[i].height;

			options.Add(option);

			if(screenRes[i].width == Screen.currentResolution.width && screenRes[i].height == Screen.currentResolution.height){
				currentResolutionIndex = i;
			}
		}
		resolutionDropdown.AddOptions(options);
		resolutionDropdown.value=currentResolutionIndex;
		resolutionDropdown.RefreshShownValue();
	}
    private void Update()
    { 
        resolutionDropdown.RefreshShownValue();
        if (!Screen.fullScreen)
            Screen.SetResolution(screenRes[currentResolutionIndex].width, screenRes[currentResolutionIndex].height, !Screen.fullScreen);
        else
            SetResolution(0);
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = Screen.currentResolution;
        Screen.SetResolution(screenRes[currentResolutionIndex].width, screenRes[currentResolutionIndex].height, Screen.fullScreen);		
    }
    public void ChangeLevel(int qualityIndex)
    {
            QualitySettings.SetQualityLevel(qualityIndex);
			//MyGameSettings.getInstance().drop.value=qualityIndex;
    }

	public void GetFullScreen(bool isFullScreen){
		Screen.fullScreen= isFullScreen;
	}
    public void MoreControls()
    {
        currentcontrol++;
        if (currentcontrol > controls.Length - 1)
            currentcontrol = 0;
    }
    public void LessControls()
    {
        currentcontrol--;
        if (currentcontrol < 0)
            currentcontrol = controls.Length - 1;
    }
}

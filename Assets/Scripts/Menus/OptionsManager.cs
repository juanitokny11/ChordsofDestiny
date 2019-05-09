using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsManager : MonoBehaviour {

	Resolution[] resolutions;
	public TMP_Dropdown resolutionDropdown;
    public int currentcontrol = 0;
    public GameObject[] controls;
    public Text texto;

    void Start(){
		 resolutions = Screen.resolutions;

		//resolutions = new Resolution[4];

		resolutionDropdown.ClearOptions();

		List<string> options = new List<string>();
		int currentResolutionIndex=0;
		for(int i= 0;i<resolutions.Length;i++){

			string option = resolutions[i].width +"x"+ resolutions[i].height;

			options.Add(option);

			if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height){
				currentResolutionIndex = i;
			}
		}
		resolutionDropdown.AddOptions(options);
		resolutionDropdown.value=currentResolutionIndex;
		resolutionDropdown.RefreshShownValue();
	}
    public void SetResolution(int resolutionIndex)
    {
		Resolution resolution =resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);		

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

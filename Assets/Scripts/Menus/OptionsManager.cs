using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour {

	Resolution[] resolutions;
	public Dropdown resolutionDropdown;
    public int currentcontrol = 0;
    public GameObject[] controls;
    public Text texto;

    void Start(){
        resolutions = Screen.resolutions;

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
    public void Update()
    {
        if (currentcontrol == 0)
        {
            controls[0].SetActive(true);
            controls[1].SetActive(false);
            controls[2].SetActive(false);
            texto.text = "Teclado y Ratón";
        }
        if (currentcontrol == 1)
        {
            controls[0].SetActive(false);
            controls[1].SetActive(true);
            controls[2].SetActive(false);
            texto.text = "Xbox";
        }
        if (currentcontrol == 2)
        {
            controls[0].SetActive(false);
            controls[1].SetActive(false);
            controls[2].SetActive(true);
            texto.text = "Ps4";
        }

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

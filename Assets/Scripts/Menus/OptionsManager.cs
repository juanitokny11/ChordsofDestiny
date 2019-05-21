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
    private void Awake()
    {
        //LanguageManager.langData.currentLanguage = LangData.Languages.English;
    }
    void Start(){
       // Debug.Log(Application.persistentDataPath);
        Screen.fullScreen = true;
		Resolution[] screenRes = Screen.resolutions;

		resolutions = new Resolution[4];
        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutions[i] = new Resolution();
        }
		//resolutionDropdown.ClearOptions();

		/*List<string> options = new List<string>();
		for(int i= 0;i< screenRes.Length;i++){

			string option = screenRes[i].width +"x"+ screenRes[i].height;

			options.Add(option);

			if(screenRes[i].width == Screen.currentResolution.width && screenRes[i].height == Screen.currentResolution.height){
				currentResolutionIndex = i;
			}
		}
		resolutionDropdown.AddOptions(options);
		resolutionDropdown.value=currentResolutionIndex;
		resolutionDropdown.RefreshShownValue();*/
	}
    private void Update()
    { 
/*         resolutionDropdown.RefreshShownValue();
        if (!Screen.fullScreen)
            Screen.SetResolution(screenRes[currentResolutionIndex].width, screenRes[currentResolutionIndex].height, !Screen.fullScreen);
        else
            SetResolution(0);*/
    }
    public void SetResolution()
    {
        if(resolutionDropdown.value == 0)
        {
            Screen.SetResolution(720, 480, Screen.fullScreen);
        }
        if(resolutionDropdown.value == 1)
        {
            Screen.SetResolution(1024, 768, Screen.fullScreen);
        }
        if (resolutionDropdown.value == 2)
        {
            Screen.SetResolution(1280, 720, Screen.fullScreen);
        }
        if (resolutionDropdown.value == 3)
        {
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
        }
       //currentResolutionIndex = 21;
       /* Resolution resolution = Screen.currentResolution;
        Screen.SetResolution(screenRes[currentResolutionIndex].width, screenRes[currentResolutionIndex].height, Screen.fullScreen);*/
        Debug.Log(Screen.currentResolution);
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

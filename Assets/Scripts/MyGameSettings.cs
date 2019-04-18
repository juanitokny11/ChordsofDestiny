using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class MyGameSettings : MonoBehaviour {

	private static MyGameSettings instance;
    public Dropdown drop;
	public AudioMixer mixer;
	public float generalVol=0.5f;
	public float musicVol=0.5f;
	public float effectsVol=0.5f;
    public bool gameStarted=false;
    public bool logoPlayed=false;
    public MenuAnim menuAnim;

    void Awake (){
		if (instance == null) {
			instance = this;

            DontDestroyOnLoad (this);
		}
        Scene MainMenu = SceneManager.GetActiveScene();
        string scenename = MainMenu.name;
        menuAnim = GameObject.FindGameObjectWithTag("MenuAnim").GetComponentInChildren<MenuAnim>();
    }
    private void Update()
    {
        Scene MainMenu = SceneManager.GetActiveScene();
        string scenename = MainMenu.name;
        if (scenename == "New Scene")
        menuAnim = GameObject.FindGameObjectWithTag("MenuAnim").GetComponentInChildren<MenuAnim>();
    }
    public static MyGameSettings getInstance(){
		return instance;
	}

}


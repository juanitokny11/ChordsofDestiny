using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public Animator cam_Anim;
    public void Start()
    {
        Cursor.visible = true;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
        Time.timeScale=1;
        Cursor.visible = false;
        MyGameSettings.getInstance().gameStarted = true;
        MyGameSettings.getInstance().logoPlayed = true;
    }
    public void EndGame()
    {
        Application.Quit();
    }
    public void Options()
    {
        cam_Anim.SetTrigger("Options");
        Cursor.visible = true;
    }
    public void Extras()
    {
        cam_Anim.SetTrigger("Extras");
        Cursor.visible = true;
    }
    public void Options_to_Menu()
    {
        cam_Anim.SetTrigger("Options_to_menu");
        Cursor.visible = true;
    }
    public void Extra_to_Menu()
    {
        Cursor.visible = true;
        cam_Anim.SetTrigger("Extras_to_options");
    }
    public void Creditos()
    {
        SceneManager.LoadScene("Credits");
         Cursor.visible = true;
        Time.timeScale = 1;
    }

    public void Title(){
        SceneManager.LoadScene("MainM");
        Cursor.visible = true;
        Time.timeScale=1;
        MyGameSettings.getInstance().gameStarted = true;
        MyGameSettings.getInstance().logoPlayed = true;
    }
	public void Victory(){
		SceneManager.LoadScene("Victoria");
        Cursor.visible = true;
    }
    public void Pause()
    {
        BeatEmupManager.instance.Pausa();
    }
    public void ChangeScene()
    {
        if (MyGameSettings.getInstance().gameStarted == false)
        {
            Title();
        }
        else
        {
            SceneManager.LoadScene("Gameplay");
            Time.timeScale = 1;
            Cursor.visible = false;
        }
    }
}

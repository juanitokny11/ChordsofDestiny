using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public Animator cam_Anim;
    public MenuAnim menuAnim;
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
        menuAnim.firstTime = true;
    }
    public void EndGame()
    {
        Application.Quit();
    }
    public void Options()
    {
        cam_Anim.SetTrigger("Options");
        Cursor.visible = true;
        menuAnim.firstTime = true;
        menuAnim.Anim = true;
    }
    public void Extras()
    {
        cam_Anim.SetTrigger("Extras");
        Cursor.visible = true;
        menuAnim.Anim = true;
        menuAnim.firstTime = true;
    }
    public void Options_to_Menu()
    {
        cam_Anim.SetTrigger("Options_to_menu");
        Cursor.visible = true;
         menuAnim.Anim = false;
    }
    public void Extra_to_Menu()
    {
        Cursor.visible = true;
        cam_Anim.SetTrigger("Extra_to_menu");
        menuAnim.Anim = false;
    }
    public void Creditos()
    {
        SceneManager.LoadScene("Credits");
         Cursor.visible = true;
        menuAnim.firstTime = true;
        Time.timeScale = 1;
    }
    public void Title(){
        SceneManager.LoadScene("NewScene");
        menuAnim.firstTime = true;
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
            SceneManager.LoadScene("Gameplay");
            Time.timeScale = 1;
            Cursor.visible = false;
    }
}

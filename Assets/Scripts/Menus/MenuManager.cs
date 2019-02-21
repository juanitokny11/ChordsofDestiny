using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public void PlayGame()
    {
        SceneManager.LoadScene("LevelDesign");
        Time.timeScale=1;
        Cursor.visible = false;
        MyGameSettings.getInstance().gameStarted = true;
    }
    public void EndGame()
    {

        Application.Quit();
    }

    public void Controles()
    {
        SceneManager.LoadScene("Controles");
        Cursor.visible = true;
        Time.timeScale=1;
    }

    
    public void Options()
    {
        SceneManager.LoadScene("options");
        Cursor.visible = true;
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
    }
	public void Victory(){
		SceneManager.LoadScene("Victoria");
        Cursor.visible = true;
    }
    public void Pause()
    {
         MyGameManager.getInstance().Pausa();
         
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

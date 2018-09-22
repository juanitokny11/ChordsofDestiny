using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public void PlayGame()
    {
        SceneManager.LoadScene("Prisionescenari");
       
    }
    public void EndGame()
    {

        Application.Quit();
    }

    public void Controles()
    {
        SceneManager.LoadScene("Controles");
        Cursor.visible = true;
    }

    
    public void Creditos()
    {
        SceneManager.LoadScene("Credits");
        Cursor.visible = true;
    }

    public void Title(){
        SceneManager.LoadScene("MainM");
        Cursor.visible = true;
    }
	public void Victory(){
		SceneManager.LoadScene("Victoria");
        Cursor.visible = true;
    }
    public void Pause()
    {
        //FPSInputManager.getInstance().Pausa();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour {

    // Use this for initialization
    public void Creditos()
    {
        SceneManager.LoadScene("Credits");
        Cursor.visible = true;
    }

    public void Title()
    {
        SceneManager.LoadScene("New Scene");
        Cursor.visible = true;
    }

}

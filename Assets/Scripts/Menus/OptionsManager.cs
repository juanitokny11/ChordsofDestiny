using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour {

	public Dropdown drop;
	public bool Fullscreen;

	void Start(){
        drop = MyGameSettings.getInstance().drop;
	}
	public void ChangeLevel(){
        MyGameSettings.getInstance().ChangeLevel();
    }
	public void ChangeResolution(){
        MyGameSettings.getInstance().ChangeResolution();
    }
}

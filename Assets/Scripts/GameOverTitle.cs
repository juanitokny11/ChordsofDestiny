using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverTitle : MonoBehaviour {

	public GameObject frase1;
	public GameObject frase2;
	public GameObject frase3;
	private int states=0;
	void Start () {
		states=Random.Range(0,2);
		if(states==0){
			frase1.SetActive(true);
			frase2.SetActive(false);
			frase3.SetActive(false);
		}else if (states==1){
			frase1.SetActive(false);
			frase2.SetActive(true);
			frase3.SetActive(false);
		}else if(states==2){
			frase1.SetActive(false);
			frase2.SetActive(false);
			frase3.SetActive(true);
		}
	}
}

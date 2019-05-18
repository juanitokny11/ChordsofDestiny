using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverTitle : MonoBehaviour {

	public GameObject frase1;
	public GameObject frase2;
	public GameObject frase3;
	public int states=0;
	void Start () {
		states=Random.Range(0,3);
		if(states==0){
            frase1.GetComponent<TextMeshProUGUI>().enabled = true;
			frase2.GetComponent<TextMeshProUGUI>().enabled = false;
            frase3.GetComponent<TextMeshProUGUI>().enabled = false;
        }
        else if (states==1){
			frase1.GetComponent<TextMeshProUGUI>().enabled = false;
            frase2.GetComponent<TextMeshProUGUI>().enabled = true;
            frase3.GetComponent<TextMeshProUGUI>().enabled = false;
        }
        else if(states==2){
			frase1.GetComponent<TextMeshProUGUI>().enabled = false;
            frase2.GetComponent<TextMeshProUGUI>().enabled = false;
            frase3.GetComponent<TextMeshProUGUI>().enabled = true;
        }
	}
}

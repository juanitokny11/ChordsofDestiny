using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movie : MonoBehaviour
{
    RawImage video;

   

    // Use this for initialization
    void Start ()
    {

        video = this.GetComponent<RawImage>();
        (video.texture as MovieTexture).Play();

    }

    // Update is called once per frame
    void Update () {
		
	}
}

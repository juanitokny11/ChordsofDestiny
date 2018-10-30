using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotationScript : MonoBehaviour {
    void Update () {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        if (horizontal != 0.0f || vertical != 0.0f) {
            var rotationDirection = new Vector3(horizontal, 0, vertical);
            transform.rotation = Quaternion.LookRotation(rotationDirection, Vector3.up);
        }
    }
}


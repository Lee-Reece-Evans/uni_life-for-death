using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade : MonoBehaviour {

    private Light lt;
    private bool fadeIn;
    private bool fadeOut;

	private void Start () {
        lt = GetComponent<Light>();
        fadeIn = true;
    }
	
	private void Update () {
        // increase and decrease light intensity
        if (lt.intensity >= 5)
        {
            fadeIn = false;
            fadeOut = true;
        }
        else if (lt.intensity <= 0)
        {
            fadeOut = false;
            fadeIn = true;
        }

        if (fadeIn) lt.intensity += 0.05f;
        else if (fadeOut) lt.intensity -= 0.05f;
	}
}

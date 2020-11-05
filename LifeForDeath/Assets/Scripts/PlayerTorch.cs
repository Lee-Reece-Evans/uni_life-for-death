using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTorch : MonoBehaviour {

    private Light torchLight;
    private ParticleSystem torchFX;

    private void Start()
    {
        torchLight = GetComponent<Light>();
        torchFX = GetComponentInChildren<ParticleSystem>();
    }

    private void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (torchLight.enabled)
            {
                torchLight.enabled = false;
                torchFX.Stop();
                torchFX.Clear(); // make effect stop instantly
            }
            else if (!torchLight.enabled)
            {
                torchLight.enabled = true;
                torchFX.Play();
            }
        }
	}
}

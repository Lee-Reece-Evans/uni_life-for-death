using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour {

    Material mat;

    public float dissolvetime;
    public float disolveInSpeed = 1.5f;
    public float disolveOutSpeed = 1.5f;

    private void Start () {
        mat = GetComponent<Renderer>().material; // get the material to be dissolved
    }

    public void DissolveOut()
    {
        dissolvetime = mat.GetFloat("_DissolveAmount"); // get the current dissolve amount
        if (dissolvetime != 1) // if it isn't completely dissolved
        {
            dissolvetime += Time.deltaTime * disolveOutSpeed; // speed variable
            mat.SetFloat("_DissolveAmount", dissolvetime); // change the float over time
        }
        if (dissolvetime > 1) // if has dissolved beyond the max amount
        {
            mat.SetFloat("_DissolveAmount", 1); // set to max value
        }
    }

    public void DissolveIn()
    {
        dissolvetime = mat.GetFloat("_DissolveAmount"); // get the current dissolve amount
        if (dissolvetime != 0) // if it isn't completely dolved
        {
            dissolvetime -= Time.deltaTime * disolveInSpeed; // set speed variable
            mat.SetFloat("_DissolveAmount", dissolvetime); // change float over time
        }
        if (dissolvetime < 0) // if has desolved beyond max amount
        {
            mat.SetFloat("_DissolveAmount", 0); // set to max amount
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuZombieController : MonoBehaviour
{
    Dissolve[] dissolveZombie; // array of scripts
    // Start is called before the first frame update
    private void Start()
    {
        dissolveZombie = GetComponentsInChildren<Dissolve>();
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (Dissolve mat in dissolveZombie) // iterate through all scripts
        {
            mat.DissolveIn();
        }
    }
}

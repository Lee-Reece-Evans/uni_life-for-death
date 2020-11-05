using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Animator anim;

    public void FadeOut()
    {
        anim.SetTrigger("FadeOut");
    }

    public void FadeFinished() // used by animation event
    {
        SceneManager.LoadScene("Gameover");
    }
}

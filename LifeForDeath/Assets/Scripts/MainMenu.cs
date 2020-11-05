using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator anim;

    private void Awake()
    {
        if (GameObject.Find("GameManager") != null)
            GameObject.Destroy(GameObject.Find("GameManager"));
    }

    // credits to https://www.youtube.com/watch?v=Oadq-IrOazg for help with implementing the fade to black transition between scenes
    public void PlayGame()
    {
        anim.SetTrigger("FadeOut");
    }

    public void FadeFinished()
    {
        SceneManager.LoadScene("Main");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

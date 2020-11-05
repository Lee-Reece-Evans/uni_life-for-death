using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameoverMenu : MonoBehaviour
{
    public Animator anim;

    public Text winorlose;
    public Text waveText;
    public Text SoulsText;
    public Text FuelText;
    public Text HeadshotsText;
    public Text AccuracyText;

    private void Start()
    {
        // make the cursor visible again
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        DisplayResults();
    }

    public void DisplayResults()
    {
        // using variables from the main scene's game manager that is an immortal object carried over to this scene
        if (GameManager.Instance.gameWon)
        {
            winorlose.text = "YOU WIN!";
            winorlose.color = Color.green;
        }
        else
        {
            winorlose.text = "YOU LOSE!";
            winorlose.color = Color.red;
        }

        waveText.text = "WAVE: " + GameManager.Instance.wave;
        SoulsText.text = "SOULS: " + GameManager.Instance.totalSouls;
        FuelText.text = "FUEL: " + GameManager.Instance.totalFuel + "%";
        HeadshotsText.text = "HEADSHOTS: " + (int)GameManager.Instance.totalHeadshots;
        AccuracyText.text = "ACCURACY: " + (int)GameManager.Instance.totalAccuracy + "%";
    }

    // credit to https://www.youtube.com/watch?v=Oadq-IrOazg for help with implementing the fade to black transition between scenes
    public void BacktoMenu()
    {
        anim.SetTrigger("FadeOut");
    }

    public void FadeFinished()
    {
        SceneManager.LoadScene("Menu");
    }
}

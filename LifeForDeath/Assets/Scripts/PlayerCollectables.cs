using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollectables : MonoBehaviour {

    public int souls;
    public int fuel;
    public bool hasShipBattery;

    public bool hasGreenKey;
    public bool hasBlueKey;
    public bool hasOrangeKey;
    public bool hasRedKey;
    public bool hasPurpleKey;

    public Text fuelText;
    public Text soulText;

    public Image greenkeyImage;
    public Image bluekeyImage;
    public Image redkeyImage;
    public Image purplekeyImage;
    public Image orangekeyImage;
    public Image batteryImage;

    // Use this for initialization
    private void Start ()
    {
        souls = 0;
        fuel = 0;

        hasShipBattery = false;

        hasGreenKey = false;
        hasBlueKey = false;
        hasOrangeKey = false;
        hasRedKey = false;
        hasPurpleKey = false;

        fuelText.text = "FUEL: 0%";
        soulText.text = "SOULS: 0";
    }

    public void AddSouls(int amount)
    {
        souls += amount;

        soulText.text = "SOULS: " + souls;
    }

    public void SubtractSouls(int amount)
    {
        souls -= amount;

        soulText.text = "SOULS: " + souls;
    }

    public void AddFuel(int amount)
    {
        if (fuel < 100)
        {
            fuel += amount;

            if (fuel > 100)
            {
                fuel = 100;
            }

            if (fuel == 100)
            {
                fuelText.color = Color.green;
            }

            fuelText.text = "FUEL: " + fuel + "%";

            GameManager.Instance.totalFuel = fuel;
        }
    }

    public void GreenKeyCollected()
    {
        hasGreenKey = true;
        greenkeyImage.enabled = true;
    }

    public void BlueKeyCollected()
    {
        hasBlueKey = true;
        bluekeyImage.enabled = true;
    }

    public void RedKeyCollected()
    {
        hasRedKey = true;
        redkeyImage.enabled = true;
    }

    public void PurpleKeyCollected()
    {
        hasPurpleKey = true;
        purplekeyImage.enabled = true;
    }

    public void OrangeKeyCollected()
    {
        hasOrangeKey = true;
        orangekeyImage.enabled = true;
    }

    public void ShipBatteryCollected()
    {
        hasShipBattery = true;
        batteryImage.enabled = true;
    }
}

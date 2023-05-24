using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FuelBar : MonoBehaviour
{
    private List<Image> fuelBarImages = new List<Image>();
    private List<float> initialFillAmounts = new List<float>();

    void Start()
    {
        GameObject[] fuelBars = GameObject.FindGameObjectsWithTag("FuelBar");
        foreach (GameObject fuelBar in fuelBars)
        {
            Image fuelBarImage = fuelBar.GetComponent<Image>();
            fuelBarImages.Add(fuelBarImage);
            initialFillAmounts.Add(fuelBarImage.fillAmount);
        }
    }

    void Update()
    {
        FuelBarAmount();
    }

    void FuelBarAmount()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < fuelBarImages.Count; i++)
            {
                float newFillAmount = fuelBarImages[i].fillAmount + 0.05f;
                fuelBarImages[i].fillAmount = Mathf.Clamp(newFillAmount, 0f, 1f);
            }
        }
    }
}



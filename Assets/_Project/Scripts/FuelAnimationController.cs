using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelAnimationController : MonoBehaviour
{
    [SerializeField] Animator CarAnimator;
    [SerializeField] GameObject FuelBar;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            FuelUnplugAnimation();
            FuelBar.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            FuelPlugAnimation();
        }
    }

    public void FuelUnplugAnimation()
    {
        // Find all objects with the "FuelObject" tag
        GameObject[] fuelObjects = GameObject.FindGameObjectsWithTag("FuelObject1");

        foreach (GameObject fuelObject in fuelObjects)
        {
            // Get the animator component from each fuel object
            Animator fuelAnimator = fuelObject.GetComponent<Animator>();

            // Trigger the "UnPlug" animation for each fuel object's animator
            fuelAnimator.SetTrigger("UnPlug");
        }

        // Trigger the "DriveOut" animation for the car animator
        CarAnimator.SetTrigger("DriveOut");
    }

        public void FuelPlugAnimation()
    {
        // Find all objects with the "FuelObject" tag
        GameObject[] fuelObjects = GameObject.FindGameObjectsWithTag("FuelObject");

        foreach (GameObject fuelObject in fuelObjects)
        {
            // Get the animator component from each fuel object
            Animator fuelAnimator = fuelObject.GetComponent<Animator>();

            // Trigger the "UnPlug" animation for each fuel object's animator
            fuelAnimator.SetTrigger("Plug");
        }

        // Trigger the "DriveOut" animation for the car animator
        CarAnimator.SetTrigger("DriveOut");
    }
}

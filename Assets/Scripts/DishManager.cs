using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DishManager : MonoBehaviour
{
    public DishRandomiser dishRandomiser; // Reference to the DishRandomiser script
    public GameObject cookedCKC; // Reference to the cooked CKC GameObject
    public GameObject cookedPKC; // Reference to the cooked PKC GameObject
    public GameObject cookedEFC; // Reference to the cooked EFC GameObject
    public GameObject cookedFFC; // Reference to the cooked FFC GameObject
    public Button goToDoneButton; // Reference to the GoToDone button

    public RawFoodTray rawFoodTray;
    public GameObject Deco;
    public GameObject rawFoods;

    private void Start()
    {
        goToDoneButton.onClick.AddListener(OnGoToDoneButtonClicked);
    }

    public void OnGoToDoneButtonClicked()
    {
        string currentDish = dishRandomiser.GetCurrentDish();

        // Deactivate all cooked dish GameObjects
        cookedCKC.SetActive(false);
        cookedPKC.SetActive(false);
        cookedEFC.SetActive(false);
        cookedFFC.SetActive(false);

        // Activate the correct cooked dish GameObject based on the current dish
        switch (currentDish)
        {
            case "CKC":
                cookedCKC.SetActive(true);
                break;
            case "PKC":
                cookedPKC.SetActive(true);
                break;
            case "EFC":
                cookedEFC.SetActive(true);
                break;
            case "FFC":
                cookedFFC.SetActive(true);
                break;
            default:
                Debug.LogError("Unknown dish: " + currentDish);
                break;
        }
        // Hide all raw food and decoration GameObjects
        HideRawFoodAndDecorations();
    }

    public void HideRawFoodAndDecorations()
    {
        // Hide all raw food GameObjects from RawFoodTray
        rawFoodTray.cabbage.SetActive(false);
        rawFoodTray.ginger.SetActive(false);
        rawFoodTray.egg.SetActive(false);
        rawFoodTray.rice.SetActive(false);
        rawFoodTray.pork.SetActive(false);
        rawFoodTray.chicken.SetActive(false);
        rawFoodTray.prawn.SetActive(false);
        rawFoodTray.fish.SetActive(false);

        // Hide all decoration GameObjects
        Deco.SetActive(false);

        // Hide all raw food GameObjects
        rawFoods.SetActive(false);
        
    }
}

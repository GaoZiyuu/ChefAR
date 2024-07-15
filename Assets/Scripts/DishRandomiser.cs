using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DishRandomiser : MonoBehaviour
{
    public Button randomizeButton; // Reference to the button
    public Button nextButton; // Reference to the next button
    public Image displayImage; // Reference to the image UI element

    // Arrays to hold the text strings and corresponding images
    private Sprite[] images = new Sprite[4];

    void Start()
    {
        // Load the images from Resources folder
        images[0] = Resources.Load<Sprite>("DishImage/CKC");
        images[1] = Resources.Load<Sprite>("DishImage/PKC");
        images[2] = Resources.Load<Sprite>("DishImage/EFC");
        images[3] = Resources.Load<Sprite>("DishImage/FFC");

        // Check if images are loaded correctly
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i] == null)
            {
                Debug.LogError("Image " + i + " is not loaded. Please check the file name and path.");
            }
        }


        // Add a listener to the button to call the Randomize function when clicked
        randomizeButton.onClick.AddListener(Randomize);

        // Initially hide the next button
        nextButton.gameObject.SetActive(false);
    }

    void Randomize()
    {
        // Generate a random index
        int randomIndex = Random.Range(0, images.Length);

        // Update the text and image to match the random index
        displayImage.sprite = images[randomIndex];

        // Show the next button
        nextButton.gameObject.SetActive(true);
    }

}




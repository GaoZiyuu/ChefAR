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
    public GameObject taskUI; // Reference to the Task UI
    public TextMeshProUGUI taskText; // Reference to the Task Text UI element

    public RawFoodTray rawFoodTray; // Reference to the RawFoodTray component

    // Arrays to hold the text strings and corresponding images
    private Sprite[] images = new Sprite[4];
    private string[] dishNames = new string[4] { "CKC", "PKC", "EFC", "FFC" };

    // Dictionary to hold the tasks for each dish
    private Dictionary<string, string> dishTasks = new Dictionary<string, string>
    {
        { "CKC", "Chicken - 0/1\nRice - 0/1\nCabbage - 0/1\nGinger - 0/1\nEgg - 0/1" },
        { "FFC", "Fish - 0/1\nRice - 0/1\nCabbage - 0/1\nGinger - 0/1\nEgg - 0/1" },
        { "PKC", "Pork - 0/1\nRice - 0/1\nCabbage - 0/1\nGinger - 0/1\nEgg - 0/1" },
        { "EFC", "Prawn - 0/1\nRice - 0/1\nCabbage - 0/1\nGinger - 0/1\nEgg - 0/1" }
    };

    private string currentDish; // Variable to keep track of the current dish

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

        // Initialize the RawFoodTray with dishTasks
        if (rawFoodTray != null)
        {
            rawFoodTray.SetDishTasks(dishTasks);
        }
        else
        {
            Debug.LogError("RawFoodTray reference is not assigned.");
        }

        // Add a listener to the button to call the Randomize function when clicked
        randomizeButton.onClick.AddListener(Randomize);
        nextButton.onClick.AddListener(ShowTask);

        // Initially hide the next button
        nextButton.gameObject.SetActive(false);
        taskUI.SetActive(false);

    }

    void Randomize()
    {
        // Generate a random index
        int randomIndex = Random.Range(0, images.Length);

        // Update the text and image to match the random index
        displayImage.sprite = images[randomIndex];
        currentDish = dishNames[randomIndex];  // Set the current dish key

        // Debug statement to confirm correct dish
        Debug.Log($"Randomized Dish Key: {currentDish}");

        // Show the next button
        nextButton.gameObject.SetActive(true);
    }

    public void ShowTask()
    {
        // Show the task UI
        taskUI.SetActive(true);

        // Update the task text based on the current dish
        if (dishTasks.ContainsKey(currentDish))
        {
            string task = dishTasks[currentDish];
            taskText.text = "Your Task:\n" + task;
            // Assuming rawFoodTray is a reference to the RawFoodTray component
            rawFoodTray.SetCurrentTask(task);

            // Debug statement to confirm task assignment
            Debug.Log($"Dish: {currentDish}, Task: {task}");
        }
        else
        {
            Debug.LogError($"Current dish '{currentDish}' is not found in dishTasks.");
        }
    }

    public string GetCurrentDish()
    {
        return currentDish;
    }

}




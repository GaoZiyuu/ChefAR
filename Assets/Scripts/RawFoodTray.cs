using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RawFoodTray : MonoBehaviour
{
    // GameObjects for ingredients
    public GameObject cabbage;
    public GameObject ginger;
    public GameObject egg;
    public GameObject rice;
    public GameObject pork;
    public GameObject chicken;
    public GameObject prawn;
    public GameObject fish;

    // Booleans to check if GameObject is set active or not
    private bool cabbageActivated = false;
    private bool gingerActivated = false;
    private bool eggActivated = false;
    private bool riceActivated = false;
    private bool porkActivated = false;
    private bool chickenActivated = false;
    private bool prawnActivated = false;
    private bool fishActivated = false;

    // GameObject button
    public GameObject doneButton;

    // Reference to the Task Text UI element
    public TextMeshProUGUI taskText;
    public GameObject messageBox; // UI panel for the message box
    public TextMeshProUGUI messageText; // UI element for showing messages

    private Dictionary<string, string> dishTasks;
    private string currentTask;
    private Dictionary<string, int> ingredientCounters = new Dictionary<string, int>
    {
        { "Fish", 0 },
        { "Pork", 0 },
        { "Chicken", 0 },
        { "Prawn", 0 },
        { "Cabbage", 0 },
        { "Ginger", 0 },
        { "Egg", 0 },
        { "Rice", 0 }
    };
    private Dictionary<string, int> requiredIngredients = new Dictionary<string, int>();

    private void Start()
    {
        DeactivateAllObjects();
        doneButton.SetActive(false);
        messageBox.SetActive(false);

        // Define tasks for each dish
        dishTasks = new Dictionary<string, string>
        {
            { "FFC", "Fish - 0/1\nRice - 0/1\nCabbage - 0/1\nGinger - 0/1\nEgg - 0/1" },
            { "PKC", "Pork - 0/1\nRice - 0/1\nCabbage - 0/1\nGinger - 0/1\nEgg - 0/1" },
            { "CKC", "Chicken - 0/1\nRice - 0/1\nCabbage - 0/1\nGinger - 0/1\nEgg - 0/1" },
            { "EFC", "Prawn - 0/1\nRice - 0/1\nCabbage - 0/1\nGinger - 0/1\nEgg - 0/1" }
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collidedObject = other.gameObject;
        string ingredient = collidedObject.tag;

        // Log the name of the collided object
        Debug.Log("Collided with: " + collidedObject.name);

        if (requiredIngredients.ContainsKey(ingredient))
        {
            ActivateIngredientObject(ingredient);
            UpdateIngredientCounter(ingredient);
            Destroy(collidedObject);
        }
        else
        {
            ShowMessage("Wrong ingredient!");
        }
    }

    void ActivateIngredientObject(string ingredient)
    {
        switch (ingredient)
        {
            case "Cabbage":
                cabbage.SetActive(true);
                cabbageActivated = true;
                break;
            case "Ginger":
                ginger.SetActive(true);
                gingerActivated = true;
                break;
            case "Egg":
                egg.SetActive(true);
                eggActivated = true;
                break;
            case "Rice":
                rice.SetActive(true);
                riceActivated = true;
                break;
            case "Pork":
                pork.SetActive(true);
                porkActivated = true;
                break;
            case "Fish":
                fish.SetActive(true);
                fishActivated = true;
                break;
            case "Chicken":
                chicken.SetActive(true);
                chickenActivated = true;
                break;
            case "Prawn":
                prawn.SetActive(true);
                prawnActivated = true;
                break;
        }
    }

    void DeactivateAllObjects()
    {
        cabbage.SetActive(false);
        ginger.SetActive(true);
        egg.SetActive(false);
        rice.SetActive(false);
        pork.SetActive(false);
        prawn.SetActive(false);
        fish.SetActive(false);
        chicken.SetActive(false);

        // Reset activation flags
        cabbageActivated = false;
        gingerActivated = false;
        eggActivated = false;
        riceActivated = false;
        porkActivated = false;
        fishActivated = false;
        prawnActivated = false;
        chickenActivated = false;
    }

    void UpdateIngredientCounter(string ingredient)
    {
        if (requiredIngredients.ContainsKey(ingredient))
        {
            ingredientCounters[ingredient]++;
            UpdateTaskText();

            // Check if all required ingredients are activated and their counts are correct
            bool allIngredientsMet = true;
            foreach (var kvp in requiredIngredients)
            {
                if (ingredientCounters[kvp.Key] < kvp.Value)
                {
                    allIngredientsMet = false;
                    break;
                }
            }
            if (allIngredientsMet)
            {
                doneButton.SetActive(true); // Activate the done button
            }
        }
        else
        {
            ShowMessage("Wrong ingredient!");
        }
    }

    void UpdateTaskText()
    {
        // Update the task text based on the current ingredient counters
        string[] lines = currentTask.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(' ');
            if (parts.Length == 3 && ingredientCounters.ContainsKey(parts[0]))
            {
                string ingredient = parts[0];
                lines[i] = $"{ingredient} - {ingredientCounters[ingredient]}/{requiredIngredients[ingredient]}";
            }
        }
        taskText.text = "Your Task:\n" + string.Join("\n", lines);
    }

    void ShowMessage(string message)
    {
        messageText.text = message;
        messageBox.SetActive(true);
        StartCoroutine(HideMessage());
    }

    IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(2f);
        messageBox.SetActive(false);
    }

    public void SetCurrentTask(string task)
    {
        currentTask = task;
        taskText.text = "Your Task:\n" + task;

        // Parse the task to get required ingredient amounts
        string[] lines = task.Split('\n');
        requiredIngredients.Clear();
        foreach (string line in lines)
        {
            string[] parts = line.Split(' ');
            if (parts.Length == 3)
            {
                string ingredient = parts[0];
                int requiredAmount = int.Parse(parts[2].Split('/')[1]);
                requiredIngredients[ingredient] = requiredAmount;
                ingredientCounters[ingredient] = 0; // Reset current counter for the ingredient
            }
        }
    }
}


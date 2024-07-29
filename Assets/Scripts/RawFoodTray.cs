using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RawFoodTray : MonoBehaviour
{
    // GameObjects for ingredients
    public GameObject cabbage;
    public GameObject radish;
    public GameObject egg;
    public GameObject rice;
    public GameObject pork;
    public GameObject chicken;
    public GameObject prawn;
    public GameObject fish;

    // Booleans to check if GameObject is set active or not
    private bool cabbageActivated = false;
    private bool radishActivated = false;
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
        { "Raddish", 0 },
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
            { "FFC", "Fish - 0/1\nRice - 0/1\nCabbage - 0/1\nRed Raddish - 0/1\nEgg - 0/1" },
            { "PKC", "Pork - 0/1\nRice - 0/1\nCabbage - 0/1\nRed Raddish - 0/1\nEgg - 0/1" },
            { "CKC", "Chicken - 0/1\nRice - 0/1\nCabbage - 0/1\nRed Raddish - 0/1\nEgg - 0/1" },
            { "EFC", "Prawn - 0/1\nRice - 0/1\nCabbage - 0/1\nRed Raddish - 0/1\nEgg - 0/1" }
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        // Log the name of the collided object
        Debug.Log("Collided with: " + collidedObject.name);

        // Check the tag of the collided object and activate the corresponding GameObject
        if (collidedObject.CompareTag("Cabbage"))
        {
            cabbage.SetActive(true);
            cabbageActivated = true;
            Debug.Log("Cabbage activated");
            Destroy(collidedObject);
            UpdateIngredientCounter("Cabbage");
        }
        else if (collidedObject.CompareTag("Raddish"))
        {
            radish.SetActive(true);
            radishActivated = true;
            Destroy(collidedObject);
            UpdateIngredientCounter("Raddish");
        }
        else if (collidedObject.CompareTag("Egg"))
        {
            egg.SetActive(true);
            eggActivated = true;
            Destroy(collidedObject);
            UpdateIngredientCounter("Egg");
        }
        else if (collidedObject.CompareTag("Rice"))
        {
            rice.SetActive(true);
            riceActivated = true;
            Destroy(collidedObject);
            UpdateIngredientCounter("Rice");
        }
        else if (collidedObject.CompareTag("Pork"))
        {
            pork.SetActive(true);
            porkActivated = true;
            Destroy(collidedObject);
            UpdateIngredientCounter("Pork");
        }
        else if (collidedObject.CompareTag("Fish"))
        {
            fish.SetActive(true);
            fishActivated = true;
            Destroy(collidedObject);
            UpdateIngredientCounter("Fish");
        }
        else if (collidedObject.CompareTag("Chicken"))
        {
            chicken.SetActive(true);
            chickenActivated = true;
            Destroy(collidedObject);
            UpdateIngredientCounter("Chicken");
        }
        else if (collidedObject.CompareTag("Prawn"))
        {
            prawn.SetActive(true);
            prawnActivated = true;
            Destroy(collidedObject);
            UpdateIngredientCounter("Prawn");
        }
        else
        {
            ShowMessage("Wrong ingredient!");
        }

        // Check if all objects are activated
        if (cabbageActivated && radishActivated && eggActivated && riceActivated && porkActivated)
        {
            doneButton.SetActive(true); // Activate the done button
        }
    }

    void DeactivateAllObjects()
    {
        cabbage.SetActive(false);
        radish.SetActive(false);
        egg.SetActive(false);
        rice.SetActive(false);
        pork.SetActive(false);
        prawn.SetActive(false);
        fish.SetActive(false);
        chicken.SetActive(false);

        // Reset activation flags
        cabbageActivated = false;
        radishActivated = false;
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
        taskText.text = string.Join("\n", lines);
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
                int requiredAmount = int.Parse(parts[2]);
                requiredIngredients[ingredient] = requiredAmount;
                ingredientCounters[ingredient] = 0; // Reset current counter for the ingredient
            }
        }
    }
}


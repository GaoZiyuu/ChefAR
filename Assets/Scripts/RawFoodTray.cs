using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RawFoodTray : MonoBehaviour
{
    //gameobjects
    public GameObject cabbage;
    public GameObject raddish;
    public GameObject egg;
    public GameObject rice;
    public GameObject pork;

    //booleans to check if gameobject is set active or not
    private bool cabbageActivated = false;
    private bool radishActivated = false;
    private bool eggActivated = false;
    private bool riceActivated = false;
    private bool porkActivated = false;

    //gameobject button
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
        { "Rice", 0 }
    };

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
        }
        else if (collidedObject.CompareTag("Raddish"))
        {
            raddish.SetActive(true);
            radishActivated = true;
            Destroy(collidedObject);
        }
        else if (collidedObject.CompareTag("Egg"))
        {
            egg.SetActive(true);
            eggActivated = true;
            Destroy(collidedObject);
        }
        else if (collidedObject.CompareTag("Rice"))
        {
            rice.SetActive(true);
            riceActivated = true;
            Destroy(collidedObject);
        }
        else if (collidedObject.CompareTag("Pork"))
        {
            pork.SetActive(true);
            porkActivated = true;
            Destroy(collidedObject);
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
        raddish.SetActive(false);
        egg.SetActive(false);
        rice.SetActive(false);
        pork.SetActive(false);

        // Reset activation flags
        cabbageActivated = false;
        radishActivated = false;
        eggActivated = false;
        riceActivated = false;
        porkActivated = false;
    }

    void UpdateIngredientCounter(string ingredient)
    {
        if (currentTask.Contains(ingredient))
        {
            ingredientCounters[ingredient]++;
            UpdateTaskText();
        }
        else
        {
            ShowMessage("Wrong ingredient! Try Again");
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
                lines[i] = $"{parts[0]} - {ingredientCounters[parts[0]]}/{parts[2]}";
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
    }

    // Start is called before the first frame update
    void Start()
    {
        DeactivateAllObjects();
        doneButton.SetActive(false);
        messageText.gameObject.SetActive(false);

        // Define tasks for each dish
        dishTasks = new Dictionary<string, string>
        {
            { "FFC", "Fish - 0/1\nRice - 0/1" },
            { "PKC", "Pork - 0/1\nRice - 0/1" },
            { "CKC", "Chicken - 0/1\nRice - 0/1" },
            { "EFC", "Prawn - 0/1\nRice - 0/1" }
        };
    }

}

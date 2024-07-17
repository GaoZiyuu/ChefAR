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

    // Start is called before the first frame update
    void Start()
    {
        DeactivateAllObjects();
        doneButton.SetActive(false);
    }

}

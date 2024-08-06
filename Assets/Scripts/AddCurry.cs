using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCurry : MonoBehaviour
{
    public GameObject curryS;
    public GameObject restartUI;
    public GameObject curryPot;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with object: " + other.gameObject.name);

        if (other.gameObject.CompareTag("Curry"))
        {
            Debug.Log("Curry GameObject has collided.");
            curryS.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(ActivateRestartUI());
            curryPot.SetActive(false);
        }
        else
        {
            Debug.Log("The object that collided does not have the 'Curry' tag.");
        }
    }

    public void Start()
    {
        curryS.SetActive(false);
        restartUI.SetActive(false);
    }

    private IEnumerator ActivateRestartUI()
    {
        yield return new WaitForSeconds(2);
        restartUI.SetActive(true);
    }
}

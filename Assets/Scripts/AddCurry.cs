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
        if (gameObject.tag == "Curry")
        {
            curryS.SetActive(true);
            Destroy(gameObject);
            restartUI.SetActive(true);
            curryPot.SetActive(false);
        }
    }

    public void Start()
    {
        curryS.SetActive(false);
        restartUI.SetActive(false);
    }
}

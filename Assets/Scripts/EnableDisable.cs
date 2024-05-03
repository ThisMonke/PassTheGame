using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisable : MonoBehaviour
{
    [Header("Set this to the gameObject you want to Enable")]
    public GameObject ObjectToEnable;
    [Header("Set this to the gameObject you want to Disable")]
    public GameObject ObjectToDisable;

    private void OnTriggerEnter(Collider other)
    {
        ObjectToEnable.SetActive(true);
        ObjectToDisable.SetActive(false);
    }

}
using System.Collections.Generic;
using UnityEngine;

public class TriggerSource : MonoBehaviour
{
    public delegate void OnTriggerSourceEnter(Collider other);
    public event OnTriggerSourceEnter OnSourceTriggerEnter;

    public delegate void OnTriggerSourceExit(Collider other);
    public event OnTriggerSourceExit OnSourceTriggerExit;

    public delegate void OnTriggerSourceStay(Collider other);
    public event OnTriggerSourceStay OnSourceTriggerStay;

    private void OnTriggerStay(Collider other)
    {
        if (OnSourceTriggerStay != null)
            OnSourceTriggerStay(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (OnSourceTriggerEnter != null)
            OnSourceTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (OnSourceTriggerExit != null)
            OnSourceTriggerExit(other);
    }
}

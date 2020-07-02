using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract bool CanInteract(GameObject player);
    public abstract void Interact(GameObject player);
    public void StopInteracting(GameObject player)
    {
    }
}

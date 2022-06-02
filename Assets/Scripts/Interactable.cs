using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // message displayed to player when looking at an interactable
    public string promptMessage;

    // called from our player script
    public void BaseInteract() 
    {
        Interact();
    }

    protected virtual void Interact() 
    {
        // written to be overwritten by subclasses
    }
    
}

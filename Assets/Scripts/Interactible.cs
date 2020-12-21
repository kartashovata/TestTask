using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactible : MonoBehaviour
{
    public abstract void Interact(); //action on click
    public abstract void OnPointerEnter(); //highlight on
    public abstract void OnPointerExit(); //highlight off
}

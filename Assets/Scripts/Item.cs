using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : Interactible
{
    [SerializeField] private string _name;
    [SerializeField] private int _price;

    public event UnityAction<Item> CalledFromShelf;
    public event UnityAction<Item> Destroyed;

    public string Name => _name;
    public int Price => _price;

    public override void Interact()
    {
        if (transform.parent.gameObject.TryGetComponent(out Register register))
        {
            //delete object from counter
            Destroyed?.Invoke(this);
            Destroy(gameObject);
        }
        else
        {
            //announce that object is called
            CalledFromShelf?.Invoke(this);
        }
    }

    public override void OnPointerEnter()
    {
        //do nothing
    }

    public override void OnPointerExit()
    {
        //do nothing
    }
}

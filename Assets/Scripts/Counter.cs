using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : Interactible
{
    [SerializeField] private List<Transform> _positions;
    [SerializeField] private Transform _shelf;
    [SerializeField] private Person _person;

    private List<Item> _items;

    private void OnEnable()
    {
        foreach (Item item in _shelf.transform.GetComponentsInChildren<Item>())
        {
            item.CalledFromShelf += OnItemCalledFromShelf;
            item.Destroyed += OnItemDestroyed;
        }
    }

    private void OnDisable()
    {
        foreach (Item item in _shelf.transform.GetComponentsInChildren<Item>())
        {
            item.CalledFromShelf -= OnItemCalledFromShelf;
            item.Destroyed -= OnItemDestroyed;
        }
    }

    public void OnItemCalledFromShelf(Item item)
    {
        if (_items.Count < _positions.Count)
        {
            //add object at the counter
            Item newItem = Instantiate(item,_positions[_items.Count]);
            _items.Add(newItem);
            _person.Say(item.Name+" was added to the counter.");
        }
        else
        {
            //notify that counter is full
            _person.Say("The counter is full. Remove something to add new items.");
        }
    }

    public void OnItemDestroyed(Item item)
    {
        _items.Remove(item);
    }


    public override void Interact()
    {
        //send number and list of objects to worker
        int totalCost = 0;
        string namesList="";
        foreach (Item item in _items)
        {
            totalCost += item.Price;
            namesList += item.Name + "\n";
        }
        _person.Say("You have on the counter "+_items.Count+" objects that cost "+totalCost+":\n"+namesList);
    }
}

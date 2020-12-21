using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register : Interactible
{
    [SerializeField] private List<Transform> _coordinates;
    [SerializeField] private Transform _shelf;
    [SerializeField] private Person _person;

    [SerializeField] private List<Position> _positions;

    private void OnEnable()
    {
        foreach (Item item in _shelf.transform.GetComponentsInChildren<Item>())
        {
            item.CalledFromShelf += OnItemCalledFromShelf;
        }

        _positions = new List<Position>();

        foreach (Transform coordinate in _coordinates)
        {
            _positions.Add(new Position(coordinate));
        }
    }

    private void OnDisable()
    {
        foreach (Item item in _shelf.transform.GetComponentsInChildren<Item>())
        {
            item.CalledFromShelf -= OnItemCalledFromShelf;
        }

        foreach (Position position in _positions)
        {
            position.ItemAssigned.CalledFromShelf -= OnItemDestroyed;
        }
    }

    public void OnItemCalledFromShelf(Item item)
    {
        Position freePosition = GetFreePosition();

        if (freePosition != null)
        {
            //add object at the counter
            Item newItem = Instantiate(item, freePosition.Coordinate.position,item.transform.rotation,transform);
            freePosition.AssignItem(newItem);
            newItem.Destroyed += OnItemDestroyed;
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
        _person.Say(item.Name + " was removed from the counter.");

        foreach (Position position in _positions)
        {
            if (position.ItemAssigned == item)
            {
                position.IsFree = true;
                return;
            }
        }        
    }

    public override void Interact()
    {
        //send number and list of objects to worker
        int totalCost = 0;
        int itemsCount = 0;
        string namesList="";

        foreach (Position position in _positions)
        {
            if (!position.IsFree)
            {
                totalCost += position.ItemAssigned.Price;
                namesList += position.ItemAssigned.Name + " "+ position.ItemAssigned.Price + "\n";
                itemsCount++;
            }
        }

        if (itemsCount == 0)
        {
            _person.Say("The counter is empty.");

        }
        else
        {
            _person.Say("You have " + itemsCount + " items on the counter that cost " + totalCost + ":\n" + namesList);
        }
    }

    public override void OnPointerEnter()
    {
        //transform.Rotate(new Vector3(0f,1f));
    }

    public override void OnPointerExit()
    {
        //transform.Rotate(new Vector3(0f, -1f));
    }

    private Position GetFreePosition()
    {
        foreach(Position position in _positions)
        {
            if (position.IsFree)
            {
                return position;
            }
        }

        return null;
    }
}

public class Position
{
    public bool IsFree;
    public Transform Coordinate;
    public Item ItemAssigned;

    public Position(Transform coordinate)
    {
        IsFree = true;
        Coordinate = coordinate;
    }

    public void AssignItem(Item item)
    {
        ItemAssigned = item;
        IsFree = false;
    }

    public void DiscardItem()
    {
        IsFree = true;
    }
}
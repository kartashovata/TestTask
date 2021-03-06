﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Person : Interactible
{
    [SerializeField] private TMP_Text _text;

    private Animator _animator;
    

    private void Start()
    {
        _animator = GetComponent<Animator>();
        Interact();
    }

    public override void Interact()
    {
        //wave
        _animator.SetTrigger("Wave");
        Say("Hello!\nClick at an item on the shelf to put it on the counter.\nClick at an item on the counter to remove it. \nClick on the counter to get info about items on it.");
    }

    public void Say(string text)
    {
        //say stuff
        _text.text = text;
    }

    public override void OnPointerEnter()
    {
        _animator.SetTrigger("LookInterested");
    }

    public override void OnPointerExit()
    {
        //do nothing
    }
}

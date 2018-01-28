using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WordButton : Button {

    public string word { get; private set; }

    public bool submitted;

    protected override void Start()
    {
        base.Start();
    }

    public void SetWord(string word)
    {
        this.word = word;
        Text text = GetComponentInChildren<Text>();
        text.text = " " + word + " ";
    }

    void AddValue()
    {
        MinigameManager.Instance.AddWordButtonValue(this);
    }

    void RemoveValue()
    {
        MinigameManager.Instance.RemoveWordButtonValue(this);
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        if (!submitted)
        {
            base.OnSubmit(eventData);
            AddValue();
        }
    }
}

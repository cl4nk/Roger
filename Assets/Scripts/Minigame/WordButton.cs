using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WordButton : Button , ICancelHandler{

    public string word { get; private set; }

    private bool submitted;

    protected override void Start()
    {
        base.Start();

        onClick.AddListener(() => AddValue());
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
            submitted = true;
        }
    }

    public void OnCancel(BaseEventData eventData)
    {
        if (submitted)
        {
            RemoveValue();
            submitted = false;
        }
    }
}

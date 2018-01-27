using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordButton : Button {

    public string word { get; private set; }

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
        onClick.RemoveAllListeners();
        onClick.AddListener(() => RemoveValue());
    }

    void RemoveValue()
    {
        MinigameManager.Instance.RemoveWordButtonValue(this);
        onClick.RemoveAllListeners();
        onClick.AddListener(() => AddValue());
    }
}

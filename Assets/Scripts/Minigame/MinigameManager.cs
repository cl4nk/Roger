using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class MinigameManager : Singleton<MinigameManager> {

    [Serializable]
    public class MessageInfo
    {
        public string sentence;
        public string[] sentenceWords;
        public string[] baitWords;
    }

    public class Messages
    {
        public MessageInfo[] messages;
    }

    public Messages messages;
    private int currentMessage = -1;

    [SerializeField]
    private GameObject buttonPrefab;

    private List<string> sentenceWord;

    [SerializeField]
    private Transform[] layouts;
    [SerializeField]
    private Transform answerLine;

    public UnityEvent OnGoodAnswer = new UnityEvent();
    public UnityEvent OnBadAnswer = new UnityEvent();

    private int nbWord = 0;
    private int nbMaxWord;

    public int messageToStart;
    public bool Debug;

    void Start()
    {
        sentenceWord = new List<string>();

        if (Debug)
        {
            LoadMessages();
            StartMessage(0);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            RemoveLastWord();
        }
        if (Input.GetButtonDown("Start"))
        {
            ValidateSentence();
        }
    }

    void AddWordButton(string word)
    {
        GameObject go = Instantiate(buttonPrefab);
        WordButton wb = go.GetComponent<WordButton>();
        wb.SetWord(word);
    }

    public void AddWordButtonValue(WordButton butt)
    {
        if (nbWord == nbMaxWord)
            return;
        nbWord++;
        UnityEngine.Debug.Log(butt.word);
        sentenceWord.Add(butt.word);

        if (butt.FindSelectableOnRight() != null)
            butt.FindSelectableOnRight().Select();
        else if (butt.FindSelectableOnLeft() != null)
            butt.FindSelectableOnLeft().Select();
        else if (butt.FindSelectableOnUp() != null)
            butt.FindSelectableOnUp().Select();
        else if (butt.FindSelectableOnDown() != null)
            butt.FindSelectableOnDown().Select();

        butt.transform.SetParent(answerLine);
        UnityEngine.UI.Navigation nav = butt.navigation;
        nav.mode = UnityEngine.UI.Navigation.Mode.None;
        butt.navigation = nav;

        butt.submitted = true;
    }

    public void RemoveWordButtonValue(WordButton butt)
    {
        if (nbWord == 0)
            return;
        nbWord--;

        sentenceWord.Remove(butt.word);

        Transform toFeed = layouts[0];
        for (int i = 1; i < layouts.Length; i++)
        {
            if (layouts[i].childCount < toFeed.childCount)
                toFeed = layouts[i];
        }
        UnityEngine.UI.Navigation nav = butt.navigation;
        nav.mode = UnityEngine.UI.Navigation.Mode.Automatic;
        butt.navigation = nav;
        butt.transform.SetParent(toFeed);
        butt.submitted = false;
    }

    public void RemoveLastWord()
    {
        if (nbWord == 0)
            return;
        Transform transf = answerLine.GetChild(answerLine.childCount-1);
        WordButton wb = transf.GetComponent<WordButton>();
        RemoveWordButtonValue(wb);
    }

    public void ValidateSentence()
    {
        string answer = "";
        for (int i = 0; i < sentenceWord.Count; i++)
        {
            if (i < sentenceWord.Count - 1)
                answer += sentenceWord[i] + " ";
            else
                answer += sentenceWord[i];
        }

        if (answer == messages.messages[currentMessage].sentence)
        {
            if (OnGoodAnswer != null)
                OnGoodAnswer.Invoke();
        }
        else
        {
            if (OnBadAnswer != null)
                OnBadAnswer.Invoke();
        }

    }

    public void StartMessage(int numMessage)
    {
        int currentLayout = 0;
        MessageInfo info = messages.messages[numMessage];

        List<string> words = new List<string>();
        words.AddRange(info.sentenceWords);
        words.AddRange(info.baitWords);

        for (int i = 0; i < layouts.Length; i++)
        {
            foreach (Transform child in layouts[i].transform)
            {
                Destroy(child.gameObject);
            }
        }

        int nbWordsPerLine = words.Count / layouts.Length + 1;

        int index = words.Count - 1;
        int nbWords = 0;
        bool isFirst = true;
        do
        {
            GameObject go = Instantiate(buttonPrefab);
            RectTransform rect = go.transform as RectTransform;
            rect.rotation = Quaternion.Euler(new Vector3(0.0f,0.0f, UnityEngine.Random.Range(-10, 10)));
            WordButton butt = go.GetComponent<WordButton>();
            butt.SetWord(words[index]);
            if (isFirst)
            {
                butt.Select();
                isFirst = false;
            }
            go.transform.SetParent(layouts[currentLayout]);

            nbWords++;
            if (nbWords == nbWordsPerLine)
            {
                currentLayout++;
                nbWords = 0;
            }

            words.RemoveAt(index);

            index = UnityEngine.Random.Range(0, words.Count);
        } while (words.Count != 0);

        currentMessage = numMessage;
    }

    public void LoadMessages()
    {
        string content;
        using (StreamReader sr = new StreamReader(Application.streamingAssetsPath + "/messages.json"))
        {
            content = sr.ReadToEnd();
        }
        messages = JsonUtility.FromJson<Messages>(content);

        nbMaxWord = messages.messages[0].sentence.Split(' ').Length;
        for (int i = 1; i < messages.messages.Length; i++)
        {
            if (nbMaxWord < messages.messages[i].sentence.Split(' ').Length)
                nbMaxWord = messages.messages[i].sentence.Split(' ').Length;

        }
    }
}

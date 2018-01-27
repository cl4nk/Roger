using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

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

    public UnityEvent OnGoodAnswer;
    public UnityEvent OnBadAnswer;

    void Start()
    {
        sentenceWord = new List<string>();
        LoadMessages();
        StartMessage(0);
    }

    void AddWordButton(string word)
    {
        GameObject go = Instantiate(buttonPrefab);
        WordButton wb = go.GetComponent<WordButton>();
        wb.SetWord(word);
    }

    public void AddWordButtonValue(WordButton butt)
    {
        sentenceWord.Add(butt.word);
        butt.transform.SetParent(answerLine);
    }

    public void RemoveWordButtonValue(WordButton butt)
    {
        sentenceWord.Remove(butt.word);

        Transform toFeed = layouts[0];
        for (int i = 1; i < layouts.Length; i++)
        {
            if (layouts[i].childCount < toFeed.childCount)
                toFeed = layouts[i];
        }

        butt.transform.SetParent(toFeed);
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

        int nbWordsPerLine = words.Count / layouts.Length + 1;

        int index = words.Count - 1;
        int nbWords = 0;
        bool isFirst = true;
        do
        {
            GameObject go = Instantiate(buttonPrefab);
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

    void LoadMessages()
    {
        string content;
        using (StreamReader sr = new StreamReader(Application.streamingAssetsPath + "/messages.json"))
        {
            content = sr.ReadToEnd();
        }
        messages = JsonUtility.FromJson<Messages>(content);
    }
}

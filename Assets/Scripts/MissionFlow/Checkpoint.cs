﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class Checkpoint : MonoBehaviour {

    [SerializeField]
    private int messageToStart;

    [SerializeField]
    private GameObject[] toDisable;

    private Coroutine corout;
    [SerializeField]
    private float timeToPress = 1.0f;


    public void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.gameObject.GetComponent<Player>();

        if (player != null)
        {
            
            corout = StartCoroutine(WaitPressedButtonBeforeMiniGame(player));
            
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        Player player = collider.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.GetComponent<TranslationController>().enabled = true;
            StopCoroutine(corout);
        }
    }

    IEnumerator WaitPressedButtonBeforeMiniGame(Player player)
    {
        float timePressed = 0.0f;

        Debug.Log(timePressed + " " + timeToPress);
        while (timePressed < timeToPress)
        {
            if (Input.GetButton("Action"))
            {
                timePressed += Time.deltaTime;
            }
            else
            {
                timePressed = 0.0f;
            }
            yield return null;
        }
        Debug.Log("Coucou");
        SceneManager.LoadScene("TestMiniGame", LoadSceneMode.Additive);
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => {
            player.GetComponent<TranslationController>().enabled = false;
            MinigameManager.Instance.OnGoodAnswer = new UnityEvent();
            MinigameManager.Instance.OnBadAnswer = new UnityEvent();
            MinigameManager.Instance.OnGoodAnswer.AddListener(() => {
                SceneManager.UnloadScene("TestMiniGame");
                for (int i = 0; i < toDisable.Length; i++)
                {
                    Destroy(toDisable[i]);
                }
                Destroy(gameObject);
            });
            MinigameManager.Instance.OnBadAnswer.AddListener(() => {
                SceneManager.UnloadScene("TestMiniGame");
                for (int i = 0; i < toDisable.Length; i++)
                {
                    Destroy(toDisable[i]);
                }
                Destroy(gameObject);
            });
            MinigameManager.Instance.LoadMessages();
            MinigameManager.Instance.StartMessage(messageToStart);
        };
    }


}
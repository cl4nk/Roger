using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SphereCollider))]
public class Checkpoint : MonoBehaviour {

    [SerializeField]
    private int messageToStart;

    [SerializeField]
    private GameObject[] toDisable;

    private Coroutine corout;
    [SerializeField]
    private float timeToPress = 1.0f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter()
    {
        corout = StartCoroutine(WaitPressedButtonBeforeMiniGame());
    }

    void OnTriggerExit()
    {
        StopCoroutine(corout);
    }

    IEnumerator WaitPressedButtonBeforeMiniGame()
    {
        float timePressed = 0.0f;

        Debug.Log(timePressed + " " + timeToPress);
        while (timePressed < timeToPress)
        {
            Debug.Log("coucou");

            if (Input.GetAxis("Fire3") == 1.0f)
            {
                timePressed += Time.deltaTime;
            }
            else
            {
                timePressed = 0.0f;
            }
            yield return null;
        }

        SceneManager.LoadScene("TestMiniGame", LoadSceneMode.Additive);
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => {

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

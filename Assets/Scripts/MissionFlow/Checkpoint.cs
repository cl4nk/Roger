using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//TODO: Rework all code
[RequireComponent(typeof(BoxCollider2D))]
public class Checkpoint : MonoBehaviour {

    [SerializeField]
    private int messageToStart;

    [SerializeField]
    private GameObject[] toDisable;
    [SerializeField]
    private GameObject[] toEnable;

    private Coroutine corout;

    [SerializeField]
    private float timeToPress = 1.0f;

    public UnityEvent toCall = new UnityEvent();

    public AudioSource ambiant;

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

        player.GetComponent<TranslationController>().enabled = false;

        SceneManager.LoadScene("TestMiniGame", LoadSceneMode.Additive);
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => {
            MinigameManager.Instance.OnGoodAnswer.AddListener(() => {
                for (int i = 0; i < toDisable.Length; i++)
                {
                    if (toDisable[i] != null)
                        Destroy(toDisable[i]);
                }

                for (int i = 0; i < toEnable.Length; i++)
                {
                    if (toEnable[i] != null)
                        toEnable[i].SetActive(true);
                }
                Destroy(gameObject);
                toCall.Invoke();
                player.GetComponent<TranslationController>().enabled = true;
                SceneManager.UnloadSceneAsync("TestMiniGame");
                ambiant.outputAudioMixerGroup.audioMixer.SetFloat("Volume", -0.17f);
            });
            MinigameManager.Instance.OnBadAnswer.AddListener(() => {
                for (int i = 0; i < toDisable.Length; i++)
                {
                    if (toDisable[i] != null)
                        Destroy(toDisable[i]);
                }
                for (int i = 0; i < toEnable.Length; i++)
                {
                    if (toEnable[i] != null)
                        toEnable[i].SetActive(true);
                }
                Destroy(gameObject);
                toCall.Invoke();
                player.GetComponent<TranslationController>().enabled = true;
                SceneManager.UnloadSceneAsync("TestMiniGame");
                ambiant.outputAudioMixerGroup.audioMixer.SetFloat("Volume", -0.17f);
            });
            ambiant.outputAudioMixerGroup.audioMixer.SetFloat("Volume", -80f);

            MinigameManager.Instance.LoadMessages();
            MinigameManager.Instance.StartMessage(messageToStart);
        };
    }


}

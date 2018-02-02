using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(AudioSource))]
public class CallReceiver : MonoBehaviour {

    public UnityEvent responseCallbacks = new UnityEvent();
    public UnityEvent noResponseCallbacks = new UnityEvent();


    public AudioClip ringClip;
    private AudioSource source;

	void Start ()
    {
        responseCallbacks = new UnityEvent();
        noResponseCallbacks = new UnityEvent();
        source = GetComponent<AudioSource>();
        source.clip = ringClip;
	}

    public void StartRinging()
    {
        StartCoroutine(WaitForAnswerToCall());
    }

    IEnumerator WaitForAnswerToCall()
    {
        float ringTime = 5.0f;
        float holdTime = 1.0f;

        source.Play();

        while (ringTime > 0 && holdTime > 0.0f)
        {
            ringTime -= Time.deltaTime;

            if (Input.GetButton("Action"))
            {
                holdTime -= Time.deltaTime;
            }
            else
            {
                holdTime = 1.0f;
            }

            yield return null;
        }

        source.Stop();

        if (holdTime <= 0.0f)
            responseCallbacks.Invoke();
        else
            noResponseCallbacks.Invoke();
    }
}

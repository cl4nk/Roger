using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CallSender : MonoBehaviour {

    public AudioClip[] clipsResponse;
    public AudioClip[] clipsNoResponse;

    AudioSource source;

    bool played = false;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (played)
            return;
        played = true;
        
        CallReceiver receiver = col.GetComponent<CallReceiver>();
        if (receiver != null)
        {
            receiver.responseCallbacks.AddListener(() => PlaySoundsResponse() );
            receiver.noResponseCallbacks.AddListener(() => PlaySoundsNoResponse());
            receiver.StartRinging();
        }
    }

    void PlaySoundsResponse()
    {
        StartCoroutine(PlaySoundsResponseEnum());
    }

    IEnumerator PlaySoundsResponseEnum()
    {
        for (int i = 0; i < clipsResponse.Length; i++)
        {
            source.clip = clipsResponse[i];
            source.Play();

            while (source.isPlaying)
                yield return null;
        }

    }

    void PlaySoundsNoResponse()
    {
        StartCoroutine(PlaySoundsNoResponseEnum());
    }

    IEnumerator PlaySoundsNoResponseEnum()
    {
        for (int i = 0; i < clipsNoResponse.Length; i++)
        {
            source.clip = clipsNoResponse[i];
            source.Play();

            while (source.isPlaying)
                yield return null;
        }
    }



}

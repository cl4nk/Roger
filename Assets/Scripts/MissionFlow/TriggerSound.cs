using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider2D))]
public class TriggerSound : MonoBehaviour {

    private AudioSource source;
    [SerializeField]
    private AudioClip[] clip;

    private bool played = false;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

	void OnTriggerEnter2D(Collider2D col)
    {
        if (played || (col.gameObject.GetComponent<Player>() == null))
            return;

        StartCoroutine(PlayClipArray());
        played = true;
    }

    IEnumerator PlayClipArray()
    {
        for (int i = 0; i < clip.Length; i++)
        {
            source.clip = clip[i];
            source.Play();

            while (source.isPlaying)
                yield return null;
        }
    }
}

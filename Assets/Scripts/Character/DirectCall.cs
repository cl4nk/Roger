using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class DirectCall : MonoBehaviour {

    public AudioClip clip;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip endMusic;
    public AudioSource source;
    public AudioMixer mixer;

    public AudioSource ambiant;

    public Image fadeScreen;
    public Image credits;

    bool played = false;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
    void OnTriggerEnter2D(Collider2D col)
    {
        if (played)
            return;
        played = true;
        if (col.GetComponent<Player>() != null)
        {
            PlayEnd();
        }
    }

    public void PlayEnd()
    {
        StartCoroutine(PlayEndEnum());
    }

    IEnumerator PlayEndEnum()
    {
        Player.Instance.GetComponent<TranslationController>().enabled = false;

        source.clip = clip;

        float t = 0.0f;
        source.Play();
        while(source.isPlaying)
        {
            t += Time.deltaTime;
            fadeScreen.color = Color.Lerp(new Color(0.0f, 0.0f, 0.0f, 0.0f), new Color(0.0f, 0.0f, 0.0f, 1.0f), t / source.clip.length);
            ambiant.outputAudioMixerGroup.audioMixer.SetFloat("Volume", Mathf.Lerp(-0.17f,-80.0f, t / source.clip.length));
            yield return null;
        }

        fadeScreen.color = Color.black;

        ambiant.outputAudioMixerGroup.audioMixer.SetFloat("Volume", -80.0f);

        yield return new WaitForSeconds(2.0f);

        source.clip = clip2;
        source.Play();

        while (source.isPlaying)
        {
            yield return null;
        }

        source.clip = clip3;
        source.Play();

        while (source.isPlaying)
        {
            yield return null;
        }

        source.clip = endMusic;
        source.Play();

        float fadeDuration = 2.0f;
        t = 0.0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            credits.color = Color.Lerp(new Color(1.0f, 1.0f, 1.0f, 0.0f), new Color(1.0f, 1.0f, 1.0f, 1.0f), t / fadeDuration);
            yield return null;
        }

        yield return new WaitForSeconds(30.0f);

        t = 0.0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            credits.color = Color.Lerp(new Color(1.0f, 1.0f, 1.0f, 1.0f), new Color(1.0f, 1.0f, 1.0f, 0.0f), t / fadeDuration);
            yield return null;
        }

        SceneManager.LoadScene("MainMenu");
    }
}

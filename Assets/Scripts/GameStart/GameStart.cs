using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour {

    public Image fadeScreen;
    public Image ggjLogo;
    public AudioClip music;
    public AudioClip intro;

    public AudioSource ambiant;


    // Use this for initialization
    void Start () {
        Player.Instance.GetComponent<TranslationController>().enabled = false;
        ambiant.outputAudioMixerGroup.audioMixer.SetFloat("Volume", -80.0f);
        StartCoroutine(BeginGame());
    }

    IEnumerator BeginGame()
    {
        AudioSource musicSource = gameObject.AddComponent<AudioSource>();
        AudioSource introSource = gameObject.AddComponent<AudioSource>();

        musicSource.clip = music;
        musicSource.loop = true;
        introSource.clip = intro;

        musicSource.Play();
        introSource.Play();

        float t = 0.0f;
        while (t < 2.0f)
        {
            t += Time.deltaTime;
            fadeScreen.color = Color.Lerp(new Color(1.0f, 1.0f, 1.0f, 0.0f), new Color(1.0f, 1.0f, 1.0f, 1.0f), t / 2.0f);
            yield return null;
        }

        while (introSource.isPlaying)
            yield return null;

        t = 0.0f;
        while (t < 2.0f)
        {
            t += Time.deltaTime;
            fadeScreen.color = Color.Lerp(new Color(1.0f, 1.0f, 1.0f, 1.0f), new Color(1.0f, 1.0f, 1.0f, 0.0f), t / 2.0f);
            yield return null;
        }

        float t2 = 0.0f;
        while (t2 < 2.0f)
        {
            t += Time.deltaTime;
            fadeScreen.color = Color.Lerp(new Color(0.0f, 0.0f, 0.0f, 1.0f), new Color(0.0f, 0.0f, 0.0f, 0.0f), t2 / 2.0f);
            ambiant.outputAudioMixerGroup.audioMixer.SetFloat("Volume", Mathf.Lerp(-80.0f, -0.17f, t2 / 2.0f));
            musicSource.volume = Mathf.Lerp(0, 1, t2 / 2.0f);
            yield return null;
        }

        fadeScreen.color = Color.black;
        ambiant.outputAudioMixerGroup.audioMixer.SetFloat("Volume", -0.17f);
        musicSource.Stop();


        Player.Instance.GetComponent<TranslationController>().enabled = true;
    }
}

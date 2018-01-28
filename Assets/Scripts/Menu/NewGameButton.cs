using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour {

    public Image fadeScreen;
    public AudioSource source;

    public void NewGame()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        fadeScreen.gameObject.SetActive(true);
        float t = 0.0f;
        while (t < 2.0f)
        {
            t += Time.deltaTime;
            fadeScreen.color = Color.Lerp(new Color(0.0f, 0.0f, 0.0f, 0.0f), new Color(0.0f, 0.0f, 0.0f, 1.0f), t / 2.0f);
            source.volume = Mathf.Lerp(1.0f, 0.0f, t / 2.0f);
            yield return null;
        }

        SceneManager.LoadScene("MainScene");
    }
}

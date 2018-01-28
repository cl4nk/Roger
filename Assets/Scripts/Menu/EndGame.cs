using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

    [SerializeField]
    private Image fadeScreen;
    [SerializeField]
    private float fadeDuration;
    [SerializeField]
    private Text[] credits;
    [SerializeField]
    private float creditsFadeDuration;

	// Use this for initialization
	void Start () {
		
	}
	

    IEnumerator EndGameCoroutine()
    {
        yield return null;
    }
}

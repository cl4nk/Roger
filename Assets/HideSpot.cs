using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSpot : MonoBehaviour
{
    private bool IsHidden = false;
    private GameObject Player;

    void OnTriggerStay2D(Collider2D other)
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log(IsHidden);
            other.gameObject.GetComponent<TranslationController>().enabled = IsHidden;
            other.gameObject.GetComponent<SpriteRenderer>().enabled = IsHidden;
            IsHidden = !IsHidden;
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {



    }
}

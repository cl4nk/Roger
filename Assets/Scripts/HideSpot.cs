using System;
using UnityEngine;

public class HideSpot : MonoBehaviour
{
    public event Action OnHidden;

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

            if (OnHidden != null)
                OnHidden();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStarter : MonoBehaviour {

    [SerializeField]
    private Selectable firstUI;

	// Use this for initialization
	void Start () {
        firstUI.Select();
	}
}

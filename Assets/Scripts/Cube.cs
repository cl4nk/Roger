using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

    private CharacterControls test = null;


    private void Awake()
    {
        test = GetComponent<CharacterControls>();
    }

    void Start () {
		
	}
	
	void Update () {

        test.Move(gameObject.transform);

	}
}

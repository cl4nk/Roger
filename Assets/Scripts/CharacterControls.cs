using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour {

    #region Fields

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private Vector3 rotation;

    #endregion

    void Start () {
		
	}
	
	void Update () {
        
        

	}

    public void Move(Transform characterTransform)
    {
        float verticalTranslation = (Input.GetAxis("Vertical") * moveSpeed) * Time.deltaTime;
        float horizontalTranslation = (Input.GetAxis("Horizontal") * moveSpeed) * Time.deltaTime;

        characterTransform.Translate(horizontalTranslation, 0, 0);
        characterTransform.Translate(0, verticalTranslation, 0);
    }
}

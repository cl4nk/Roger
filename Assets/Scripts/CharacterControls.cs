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
    private Vector3 direction = Vector3.zero;

    [SerializeField]
    private Vector3 rotation = Vector3.zero;

    [SerializeField]
    private string verticalTranslationAxis = "";

    [SerializeField]
    private string horizontalTranslationAxis = "";

    [SerializeField]
    private string horizontalRotationAxis = "";

    [SerializeField]
    private string verticalRotationAxis = "";

    #endregion

    void Start () {
		
	}
	
	void Update () {
        
        

	}

    public void Move(Transform characterTransform)
    {
        float verticalTranslation = (Input.GetAxis(verticalTranslationAxis) * moveSpeed) * Time.deltaTime;
        float horizontalTranslation = (Input.GetAxis(horizontalTranslationAxis) * moveSpeed) * Time.deltaTime;

        characterTransform.Translate(horizontalTranslation, 0, 0);
        characterTransform.Translate(0, verticalTranslation, 0);
    }

    public void Rotate(Transform characterTransform)
    {
        float verticalRotation = (Input.GetAxis(verticalRotationAxis) * rotationSpeed) * Time.deltaTime;
        float horizontalRotation = (Input.GetAxis(horizontalTranslationAxis) * rotationSpeed) * Time.deltaTime;

       characterTransform.Rotate(horizontalRotation, 0, 0);
        characterTransform.Rotate(0, verticalRotation, 0);
    }

}

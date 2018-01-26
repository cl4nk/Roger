using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationController : MonoBehaviour {

    #region Fields
    [SerializeField]
    private string verticalTranslationAxis = "Vertical";

    [SerializeField]
    private string horizontalTranslationAxis = "Horizontal";

    [SerializeField]
    private float moveSpeed = 5f;

    #endregion

	void Update ()
    {
        Move();
	}

    public void Move()
    {
        float verticalTranslation = (Input.GetAxis(verticalTranslationAxis) * moveSpeed) * Time.deltaTime;
        float horizontalTranslation = (Input.GetAxis(horizontalTranslationAxis) * moveSpeed) * Time.deltaTime;

        transform.Translate(horizontalTranslation, verticalTranslation, 0);
    }
}

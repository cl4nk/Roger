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
        Translation();
	}

    public void Translation()
    {
        float verticalTranslation = (Input.GetAxis(verticalTranslationAxis) * moveSpeed) * Time.deltaTime;
        float horizontalTranslation = (Input.GetAxis(horizontalTranslationAxis) * moveSpeed) * Time.deltaTime;

        transform.Translate(horizontalTranslation, 0, verticalTranslation);
    }
}

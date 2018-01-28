using UnityEngine;

public class TranslationController : MonoBehaviour, IMoving
{

    #region Fields
    [SerializeField]
    private string verticalTranslationAxis = "Vertical";

    [SerializeField]
    private string horizontalTranslationAxis = "Horizontal";

    [SerializeField]
    private float moveSpeed = 5f;

    #endregion

    private bool isMoving = false;

    public bool GoLeft { get; private set; }

	void FixedUpdate ()
    {
        Translation();
	}

    public void Translation()
    {
        float verticalTranslation = (Input.GetAxis(verticalTranslationAxis));
        float horizontalTranslation = (Input.GetAxis(horizontalTranslationAxis));

        Vector3 translation = new Vector3(horizontalTranslation, verticalTranslation);

        GoLeft = Vector3.Angle(translation, Vector3.left) < 90.0f;

        isMoving = translation.sqrMagnitude != 0.0f;

        translation *= moveSpeed * Time.deltaTime;

        transform.Translate(translation);
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}

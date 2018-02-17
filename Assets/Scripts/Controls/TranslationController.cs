using UnityEngine;

public class TranslationController : ADirectionnable, IMoving
{
    #region Fields
    [SerializeField]
    private string verticalTranslationAxis = "Vertical";

    [SerializeField]
    private string horizontalTranslationAxis = "Horizontal";

    [SerializeField]
    private float moveSpeed = 5f;

    private Rigidbody rigidbody;
    public Rigidbody Rigidbody
    {
        get
        {
            if (rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody>();
            }
            return rigidbody;
        }
    }

    #endregion

    private bool isMoving = false;

    public bool GoLeft { get; private set; }

    private Vector3 input;

    public void Update()
    {
        float verticalTranslation = (Input.GetAxis(verticalTranslationAxis));
        float horizontalTranslation = (Input.GetAxis(horizontalTranslationAxis));

        input = new Vector3(horizontalTranslation, 0.0f, verticalTranslation);

        input = Vector3.ClampMagnitude(input, 1.0f);

        GoLeft = Vector3.Angle(input, localDirection) < 90.0f;

        isMoving = input.sqrMagnitude != 0.0f;
    }

    void FixedUpdate ()
    {
        Vector3 translation = transform.TransformDirection(input);
        Rigidbody.MovePosition(Rigidbody.position + translation * moveSpeed * Time.fixedDeltaTime);
	}

    public bool IsMoving()
    {
        return isMoving;
    }
}

using UnityEngine;

[RequireComponent(typeof(Sprite))]
public class DirectionController : MonoBehaviour
{
    [SerializeField]
    private Transform refTransform;
    public Transform RefTransform
    {
        get
        {
            if (refTransform == null)
            {
                refTransform = transform;
            }
            return refTransform;
        }
    }

    private TranslationController controller;

    private SpriteRenderer renderer;
    public SpriteRenderer Renderer
    {
        get
        {
            if (renderer == null)
            {
                renderer = GetComponent<SpriteRenderer>();
            }
            return renderer;
        }
    }

    public bool Inverted = false;

    private void Awake()
    {
        controller = GetComponent<TranslationController>();
    }

    public void Update()
    {
        if (controller)
        {
            Renderer.flipX = Inverted ? controller.GoLeft : !controller.GoLeft;
        }
        else
        {
            float angle = RefTransform.eulerAngles.z;
            Renderer.flipX = Inverted ? Approximately(angle, 180, 90) : !Approximately(angle, 180, 90);
        }
    }

    private bool Approximately(float a, float b, float acceptance)
    {
        return Mathf.Abs(a - b) < acceptance;
    }
}

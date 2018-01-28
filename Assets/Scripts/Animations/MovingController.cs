using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MovingController : MonoBehaviour
{
    private Animator controller;
    public Animator Controller
    {
        get
        {
            if (controller == null)
            {
                controller = GetComponent<Animator>();
            }
            return controller;
        }
    }

    private IMoving moving;
    public IMoving Moving
    {
        get
        {
            if (moving == null)
            {
                moving = GetComponent<IMoving>();
            }
            return moving;
        }
    }

    [SerializeField]
    private string boolKey;

    public void Update()
    {
        Controller.SetBool(boolKey, Moving.IsMoving());
    }

}

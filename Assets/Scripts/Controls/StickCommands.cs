using UnityEngine;

public class StickCommands : MonoBehaviour
{
    [SerializeField]
    private Component horizontalCommand;
    private ICommand iHorizontalCommand;
    [SerializeField]
    private string horizontalRotationAxis = "HorizontalRotation";
    
    [SerializeField]
    private Component verticalCommand;
    private ICommand iVerticalCommand;
    [SerializeField]
    private string verticalRotationAxis = "VerticalRotation";

    private void Awake()
    {
        iHorizontalCommand = horizontalCommand as ICommand;
        iVerticalCommand = verticalCommand as ICommand;
    }

    // Update is called once per frame
    public void Update ()
    {
        if (iHorizontalCommand != null)
        {
            iHorizontalCommand.OnUpdate(Input.GetAxis(horizontalRotationAxis));
        }
        if (iVerticalCommand != null)
        {
            iVerticalCommand.OnUpdate(Input.GetAxis(verticalRotationAxis));
        }
    }
}

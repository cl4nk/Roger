using UnityEngine;
using UnityEngine.Events;

public class CommandController : MonoBehaviour
{
    [SerializeField]
    private Component[] ComponentList;
    private ICommand[] CommandList;
    private int currentCommand = 0;

    public int CurrentCommand
    {
        get { return currentCommand; }
        set
        {
            currentCommand = value;
            if (OnCommandEvent != null)
            {
                OnCommandEvent.Invoke(currentCommand);
            }
        }
    }

    public UnityEvent<int> OnCommandEvent;

    [SerializeField]
    private string leftBump = "LeftBump";
    [SerializeField]
    private string rightBump = "RightBump";

    [SerializeField]
    private string horizontalRotationAxis = "HorizontalRotation";
    [SerializeField]
    private string verticalRotationAxis = "VerticalRotation";

    public void Awake()
    {
        int length = ComponentList.Length;
        CommandList = new ICommand[length];
        for (int i = 0; i < length; ++i)
        {
            CommandList[i] = ComponentList[i] as ICommand;
        }
    }

    // Update is called once per frame
    public void Update ()
    {
        if (Input.GetButtonUp(leftBump))
        {
            if (currentCommand == 0)
            {
                currentCommand = CommandList.Length - 1;
            }
            else
            {
                -- currentCommand;
            }
            
        }
        else if (Input.GetButtonUp(rightBump))
        {
            currentCommand = (currentCommand + 1) % CommandList.Length;
        }
        else
        {
            CommandList[currentCommand]
                .EnterInputVector(new Vector2(Input.GetAxis(horizontalRotationAxis), Input.GetAxis(verticalRotationAxis)));
        }
    }
}

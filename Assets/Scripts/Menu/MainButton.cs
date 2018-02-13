using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MainButton : MonoBehaviour
{

    private Button button;
    public Button Button
    {
        get
        {
            if (button == null)
            {
                button = GetComponent<Button>();
            }
            return button;
        }
    }
	// Use this for initialization
	void Start ()
    {
        Button.Select();
	}
}

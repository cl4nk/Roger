using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CountdownDisplayer : MonoBehaviour
{
    private Text text;
    public Text Text
    {
        get
        {
            if (text == null)
            {
                text = GetComponent<Text>();
            }
            return text;
        }
    }

    [SerializeField]
    private Countdown countdown;
    [SerializeField]
    private string format = "00:00";

	// Update is called once per frame
	void Update ()
    {
		if (countdown.CurrentDuration >= 0)
        {
            Text.enabled = true;
            Text.text = string.Format(format, countdown.CurrentDuration);
        }
        else
        {
            Text.enabled = false;
        }
	}
}

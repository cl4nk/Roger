using UnityEngine;
using UnityEngine.UI;

public class CommandDisplayer : MonoBehaviour
{
    public CommandController controller;

    public Image imageContainer;

    public Sprite[] Sprites;

    public void OnEnable()
    {
        if (controller)
        {
            controller.OnCommandEvent += SetSprite;
        }
    }

    public void OnDisable()
    {
        if (controller)
        {
            controller.OnCommandEvent -= SetSprite;
        }
    }

    public void SetSprite(int index)
    {
        if (index >= 0 && index < Sprites.Length)
        {
            imageContainer.sprite = Sprites[index];
        }
    }

}

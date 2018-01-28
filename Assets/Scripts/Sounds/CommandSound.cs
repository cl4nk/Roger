using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CommandController))]
public class CommandSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips;

    private CommandController controller;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        controller = GetComponent<CommandController>();
    }

    public void OnEnable()
    {
        controller.OnCommandEvent += Play;
    }

    public void OnDisable()
    {
        controller.OnCommandEvent -= Play;
    }

    public void Play(int index)
    {
        source.clip = clips[index];
        source.Play();
    }
}

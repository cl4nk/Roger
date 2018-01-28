using UnityEngine;

[RequireComponent(typeof(RandomAudioSource))]
[RequireComponent(typeof(Character))]
public class DeathSound : MonoBehaviour
{
    public void OnEnable()
    {
        GetComponent<Character>().OnDeathEvent += GetComponent<RandomAudioSource>().Play;
    }

    public void OnDisable()
    {
        GetComponent<Character>().OnDeathEvent -= GetComponent<RandomAudioSource>().Play;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int SceneIndex;

    public bool Async;

	public void LoadScene()
    {
        if (Async)
        {
            SceneManager.LoadSceneAsync(SceneIndex);
        }
        else
        {
            SceneManager.LoadScene(SceneIndex);
        }
    }
}

using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    public UnityEvent OnSceneLoading = new UnityEvent();
    public UnityEvent OnSceneLoaded = new UnityEvent();

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(int sceneBuildIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneBuildIndex));
    }

    private IEnumerator LoadSceneAsync(int index)
    {
        OnSceneLoading.Invoke();
        yield return SceneManager.LoadSceneAsync(index);
        OnSceneLoaded.Invoke();
    }
}

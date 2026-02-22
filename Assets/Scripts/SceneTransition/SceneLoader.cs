using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneTransition _sceneTransition;
    [SerializeField] private ProgressBar _progressBar;

    public static SceneLoader s_Instance;

    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        if (_sceneTransition == null)
        {
            SceneManager.LoadScene(sceneName);
            return;
        }

        LoadSceneAsync(sceneName).Forget();
    }

    private async UniTask LoadSceneAsync(string sceneName)
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        await _sceneTransition.AnimationIn();

        //_progressBar.gameObject.SetActive(true);

        await UniTask.WaitForSeconds(0.5f);

        /*do
        {
            _progressBar.SetProgress((int)(scene.progress * 100) + 10);
        } while (scene.progress < 0.9f);*/

        await UniTask.WaitForSeconds(1f);



        //_progressBar.ShowCompleteButton();

        //_progressBar.gameObject.SetActive(false);

        await _sceneTransition.AnimationOut();
        scene.allowSceneActivation = true;
    }
}

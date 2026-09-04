using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject loadingScreen;

    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private Slider progressBar;

    private void Start()
    {
        mainMenuScreen.SetActive(true);
        loadingScreen.SetActive(false);
    }

    private void OnEnable()
    {
        playButton.onClick.AddListener(Play);
        quitButton.onClick.AddListener(Quit);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(Play);
        quitButton.onClick.RemoveListener(Quit);
    }

    private void Play()
    {
        mainMenuScreen.SetActive(false);
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync("Level1"));
    }

    private void Quit()
    {
        Debug.Log("EXIT!!!");
        Application.Quit();
    }

    private IEnumerator LoadAsync(string newScene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(newScene);

        while (!asyncLoad.isDone)
        {
            progressBar.value = asyncLoad.progress;
            yield return null;
        }
    }


}

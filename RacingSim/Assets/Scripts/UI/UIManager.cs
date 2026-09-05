using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelStatus;
    [SerializeField] private Win winTrigger;
    [SerializeField] private Loss lossTrigger;

    [SerializeField] private GameObject mobileControls;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Button quitButton;    
    [SerializeField] private Slider progressBar;

    private void OnEnable()
    {
        loadingScreen.SetActive(false);
        levelStatus.text = null;
        levelStatus.enabled = false;
        winTrigger.OnLevelFinished += DisplayLevelStatus;
        lossTrigger.OnLevelFinished += DisplayLevelStatus;
        quitButton.onClick.AddListener(BackToMenu);
    }

    private void OnDisable()
    {
        winTrigger.OnLevelFinished -= DisplayLevelStatus;
        lossTrigger.OnLevelFinished -= DisplayLevelStatus;
        quitButton.onClick.RemoveListener(BackToMenu);
    }

    private void DisplayLevelStatus(string message)
    {
        if (message.Contains('w'))
        {
            levelStatus.color = Color.green;
        }
        else
        {
            levelStatus.color = Color.red;
        }
        levelStatus.text = message;
        levelStatus.enabled = true;        
    }

    private void BackToMenu()
    {
        mobileControls.SetActive(false);
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync("MainMenu"));
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

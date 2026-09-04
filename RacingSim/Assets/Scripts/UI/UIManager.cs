using TMPro;
using UnityEngine;

public class UIManager: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelStatus;
    [SerializeField] private Win winTrigger;
    [SerializeField] private Loss lossTrigger;

    private void OnEnable()
    {
        levelStatus.text = null;
        levelStatus.enabled = false;
        winTrigger.OnLevelFinished += DisplayLevelStatus;
        lossTrigger.OnLevelFinished += DisplayLevelStatus;
    }

    private void OnDisable()
    {
        winTrigger.OnLevelFinished -= DisplayLevelStatus;
        lossTrigger.OnLevelFinished -= DisplayLevelStatus;
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
}

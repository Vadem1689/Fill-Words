using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteWindow : MonoBehaviour
{
    public Text completeText; // Используем UI.Text

    public void Start()
    {
        int completedLevel = LevelManager.Instance.currentLevel;
        completeText.text = $"Уровень {completedLevel + 1} пройден!";
    }
}

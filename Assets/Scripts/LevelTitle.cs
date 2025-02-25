using UnityEngine;
using UnityEngine.UI;

public class LevelTitle : MonoBehaviour
{
    public Text levelText; // Используем UI.Text

    private void Start()
    {
        if (levelText == null)
        {
            Debug.LogError("LevelTitle: UI.Text не назначен!");
            return;
        }

        int currentLevel = LevelManager.Instance.currentLevel;
        levelText.text = $"Уровень {currentLevel + 1}";
    }
}

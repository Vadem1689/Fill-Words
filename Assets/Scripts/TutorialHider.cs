using UnityEngine;

public class TutorialHider : MonoBehaviour
{
    public LevelManager levelManager; // Ссылка на LevelManager
    public GameObject tutorialPanel; // Объект туториала
    public bool forceShowTutorial = false; // Флаг для тестов (не скрывать туториал)

    private void Start()
    {
        if (levelManager == null || tutorialPanel == null)
        {
            Debug.LogWarning("TutorialHider: LevelManager или TutorialPanel не назначены.");
            return;
        }

        int latestLevel = levelManager.GetLatestUnlockedLevel();

        // Скрываем туториал, если уровень > 0, но только если forceShowTutorial выключен
        tutorialPanel.SetActive(forceShowTutorial || latestLevel == 0);
    }
}

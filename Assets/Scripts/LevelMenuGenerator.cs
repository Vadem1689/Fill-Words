using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelMenuGenerator : MonoBehaviour
{
    public LevelManager levelManager; // Менеджер уровней
    public GameObject containerPrefab; // Префаб контейнера с кнопками
    public Transform layoutParent; // Родительский объект (Layout Group)
    public ScrollRect scrollRect; // Скролл вью для автоматической прокрутки
    public int levelsPerContainer = 5; // Количество уровней в одном контейнере
    public int totalLevels = 150; // Общее количество уровней

    private List<LevelButtonContainer> containers = new List<LevelButtonContainer>();

    private void Start()
    {
        GenerateLevelContainers();
        UpdateLevelNumbers();
        ScrollToLatestLevel();
    }

    private void GenerateLevelContainers()
    {
        int containerCount = Mathf.CeilToInt((float)totalLevels / levelsPerContainer);

        for (int i = 0; i < containerCount; i++)
        {
            GameObject containerObj = Instantiate(containerPrefab, layoutParent);
            LevelButtonContainer container = containerObj.GetComponent<LevelButtonContainer>();

            int startLevel = totalLevels - ((i + 1) * levelsPerContainer); // Номера уровней идут с конца
            Debug.Log($"StartLevel: {startLevel}");
            container.AssignLevels(levelManager, startLevel);

            containers.Insert(0, container); // Вставляем контейнер в начало списка
        }
    }

    private void UpdateLevelNumbers()
    {
        foreach (var container in containers)
        {
            container.UpdateLevelNumbers();
        }
    }

    private void ScrollToLatestLevel()
    {
        int lastUnlockedLevel = levelManager.GetLatestUnlockedLevel();
        int containerIndex = Mathf.FloorToInt((float)(totalLevels - lastUnlockedLevel) / levelsPerContainer);

        float normalizedPosition = (float)containerIndex / (containers.Count - 1);
        scrollRect.verticalNormalizedPosition = Mathf.Clamp01(1 - normalizedPosition); // Снизу вверх
    }
}

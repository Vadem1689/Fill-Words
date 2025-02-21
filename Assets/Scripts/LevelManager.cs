using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private const string LastLevelKey = "LastLevel";
    private int lastLevel;
    public GameManager gameManager;
    public GameObject LevelMenu;
    
    void Awake()
    {
        lastLevel = PlayerPrefs.GetInt(LastLevelKey, 0);
    }

    public void OpenLevel(int levelNumber)
    {
        if (IsLevelUnlocked(levelNumber))
        {
            gameManager.StartCoroutine(gameManager.RunGameFlow(levelNumber));
            LevelMenu.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"Уровень {levelNumber} не разблокирован.");
        }
    }

    public bool IsLevelUnlocked(int levelNumber)
    {
        return levelNumber <= lastLevel;
    }
    
    public void MarkLevelCompleted(int levelNumber)
    {
        if (levelNumber > lastLevel)
        {
            lastLevel = levelNumber;
            PlayerPrefs.SetInt(LastLevelKey, lastLevel);
            PlayerPrefs.Save();
        }
    }

    public int GetLatestUnlockedLevel()
    {
        return lastLevel;
    }

    public bool IsLevelCompleted(int levelNumber)
    {
        return levelNumber < lastLevel;
    }

    public void OnLevelCompleted()
    {
        lastLevel++;
        PlayerPrefs.SetInt(LastLevelKey, lastLevel);
        PlayerPrefs.Save();
        Debug.Log($"Уровень {lastLevel} завершен. Следующий уровень разблокирован.");
    }
}

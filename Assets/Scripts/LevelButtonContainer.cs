using UnityEngine;
using System.Collections.Generic;

public class LevelButtonContainer : MonoBehaviour
{
    public List<LevelButton> levelButtons = new List<LevelButton>();
    private int startLevel; // Первый уровень в контейнере

    public void AssignLevels(LevelManager levelManager, int startLevel)
    {
        this.startLevel = startLevel;

        for (int i = 0; i < levelButtons.Count; i++)
        {
            int levelNumber = startLevel + i;
            levelButtons[i].levelNumber = levelNumber;
            levelButtons[i].levelManager = levelManager;
            levelButtons[i].gameObject.SetActive(true);
            levelButtons[i].UpdateButtonState();
        }
    }

    public void UpdateLevelNumbers()
    {
        // for (int i = 0; i < levelButtons.Count; i++)
        // {
        //     int levelNumber = startLevel + i;
        //     levelButtons[i].levelNumber = levelNumber;
        //     levelButtons[i].UpdateButtonState();
        // }
    }
}

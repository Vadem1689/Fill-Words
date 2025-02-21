using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{
    public int levelNumber; // Номер уровня
    public LevelManager levelManager; // Ссылка на LevelManager
    public Image buttonImage; // Изображение кнопки
    public Sprite lockedSprite; // Спрайт закрытого уровня
    public Sprite unlockedSprite; // Спрайт открытого уровня
    public Sprite latestUnlockedSprite; // Спрайт для последнего доступного уровня
    public TMP_Text levelText; // Текст с номером уровня
    public GameObject completedIcon; // Объект Image с иконкой "крестика"
    

    public void UpdateButtonState()
    {
        bool isUnlocked = levelManager.IsLevelUnlocked(levelNumber);
        bool isLatestUnlocked = (levelNumber == levelManager.GetLatestUnlockedLevel());
        bool isCompleted = levelManager.IsLevelCompleted(levelNumber);
        completedIcon.SetActive(isCompleted);
        levelText.text = (levelNumber + 1).ToString();
        if (!isUnlocked)
        {
            buttonImage.sprite = lockedSprite;
            ShowLevelNumber(true);
        }
        else if (isCompleted)
        {
            buttonImage.sprite = unlockedSprite;
            ShowLevelNumber(false);
        }
        else if (isLatestUnlocked)
        {
            buttonImage.sprite = latestUnlockedSprite;
            ShowLevelNumber(true);
        }
        else
        {
            buttonImage.sprite = unlockedSprite;
            ShowLevelNumber(true);
        }
        var button = GetComponent<Button>();
        button.interactable = isUnlocked;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => levelManager.OpenLevel(levelNumber));
    }

    private void ShowLevelNumber(bool showNumber)
    {
        levelText.gameObject.SetActive(showNumber);
        completedIcon.SetActive(!showNumber);
    }

    public void OnLevelButtonClick()
    {
        levelManager.OpenLevel(levelNumber);
    }
}

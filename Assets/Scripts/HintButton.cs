using UnityEngine;
using UnityEngine.UI;

public class HintButton : MonoBehaviour
{
    public Button hintButton;
    public int hintCost = 25;
    public GridGenerator gridGenerator;

    private void Start()
    {
        if (hintButton == null)
        {
            Debug.LogError("HintButton: Button не назначен!");
            return;
        }

        hintButton.onClick.AddListener(OnHintButtonClick);
    }

    private void OnHintButtonClick()
    {
        SoundManager.Instance.PlaySound("click");

        if(gridGenerator.wordsToPlace.Count == 0) return;
        
        if (MoneyManager.Instance.SpendMoney(hintCost))
        {
            GridVisualizer.instance.ShowHint();
        }
        else
        {
            Debug.LogWarning("Недостаточно монет для подсказки!");
        }
    }

    private void OnDestroy()
    {
        hintButton.onClick.RemoveListener(OnHintButtonClick);
    }
}
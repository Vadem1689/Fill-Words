using UnityEngine;
using TMPro;

public class MoneyText : MonoBehaviour
{
    public TMP_Text moneyText;

    private void Start()
    {
        if (moneyText == null)
        {
            Debug.LogError("MoneyText: TMP_Text не назначен!");
            return;
        }

        UpdateMoneyText(MoneyManager.Instance.GetMoney());

        MoneyManager.Instance.OnMoneyChanged += UpdateMoneyText; // Подписка на событие
    }

    private void OnDestroy()
    {
        MoneyManager.Instance.OnMoneyChanged -= UpdateMoneyText; // Отписка при уничтожении
    }

    private void UpdateMoneyText(int amount)
    {
        moneyText.text = amount.ToString();
    }
}

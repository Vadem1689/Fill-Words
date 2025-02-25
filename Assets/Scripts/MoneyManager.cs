using UnityEngine;
using System;

public class MoneyManager : MonoBehaviour
{
    private const string MoneyKey = "PlayerMoney";
    public static MoneyManager Instance { get; private set; }
    private int money;

    public event Action<int> OnMoneyChanged; // Событие при изменении монет

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadMoney();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadMoney()
    {
        money = PlayerPrefs.GetInt(MoneyKey, 0);
        OnMoneyChanged?.Invoke(money); // Вызываем событие при загрузке
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetInt(MoneyKey, money);
        PlayerPrefs.Save();
    }

    public int GetMoney()
    {
        return money;
    }

    public void AddMoney(int amount)
    {
        if (amount > 0)
        {
            money += amount;
            SaveMoney();
            OnMoneyChanged?.Invoke(money);
        }
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            SaveMoney();
            OnMoneyChanged?.Invoke(money);
            return true;
        }
        return false;
    }
}

using UnityEngine;
using TMPro;
using System.Collections;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public int coins = 500;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI warningText;   // 👈 Uyarı Text objesi burada

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateCoinUI();
        if (warningText != null)
            warningText.gameObject.SetActive(false); // Başlangıçta kapalı
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            UpdateCoinUI();
            return true;
        }
        else
        {
            ShowWarning("Not enough coins!");
            return false;
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinUI();
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = "" + coins;
    }

    public void ShowWarning(string message)
    {
        if (warningText == null) return;
        
        warningText.text = message;
        warningText.gameObject.SetActive(true);

        // 2 saniye sonra yazıyı gizle (Coroutine ile)
        StartCoroutine(HideWarning());
    }

    private IEnumerator HideWarning()
    {
        yield return new WaitForSeconds(0.5f);
        warningText.gameObject.SetActive(false);
    }
}

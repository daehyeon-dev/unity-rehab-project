using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health playerHealth;
    [SerializeField] private Health enemyHealth;

    [Header("UI")]
    [SerializeField] private TMP_Text playerHpText;
    [SerializeField] private TMP_Text enemyHpText;
    
    public void Update()
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (playerHealth != null && playerHpText != null)
        {
            playerHpText.text = $"Player HP: {playerHealth.CurrentHealth} / {playerHealth.MaxHealth}";
        }
        if (enemyHealth != null && enemyHpText != null)
        {
            enemyHpText.text = $"Enemy HP: {enemyHealth.CurrentHealth} / {enemyHealth.MaxHealth}";
        }
    }
}

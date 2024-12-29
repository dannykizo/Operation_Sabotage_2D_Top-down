using TMPro;
using UnityEngine;
using TopDown.Core;

namespace TopDown.UI
{
    public class MoneyEarnedCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyEarnedCounter;

        private void Awake()
        {
            moneyEarnedCounter.text = GameManager.Instance.LevelReward.ToString();
        }
    }
}
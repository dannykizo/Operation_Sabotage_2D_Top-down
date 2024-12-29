using UniRx;
using UnityEngine;
using TopDown.Health;

namespace TopDown.UI
{
    public class HealthCounter : UICounter
    {
        [Header("Health Component")]
        [SerializeField] private HealthComponent healthComponent;

        private void OnEnable()
        {
            //Subscribe to health component and send health value as percentage
            healthComponent.CurrentHealth.ObserveEveryValueChanged(property => property.Value)
                .Subscribe(value =>
                {
                    UpdateCounter(value.ToString("F0") + "%");
                })
                .AddTo(subscriptions);
        }
    }
}
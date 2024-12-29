using UniRx;
using UnityEngine;
using TopDown.Health;

namespace TopDown.UI
{
    public class ArmorCounter : UICounter
    {
        [Header("Health Component")]
        [SerializeField] private HealthComponent healthComponent;

        private void OnEnable()
        {
            //Subscribe to health component and send armor value as percentage
            healthComponent.CurrentArmor.ObserveEveryValueChanged(property => property.Value)
                .Subscribe(value =>
                {
                    UpdateCounter(value.ToString("F0") + "%");
                })
                .AddTo(subscriptions);
        }
    }
}
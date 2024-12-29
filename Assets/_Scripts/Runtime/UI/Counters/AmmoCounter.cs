using UniRx;
using UnityEngine;
using TopDown.Shooting;

namespace TopDown.UI
{
    public class AmmoCounter : UICounter
    {
        [Header("Health Component")]
        [SerializeField] private GunManager gunManager;

        private int ammoInClip;
        private int totalAmmo;

        private void OnEnable()
        {
            gunManager.CurrentAmmoInClip.ObserveEveryValueChanged(property => property.Value)
                .Subscribe(value =>
                {
                    ammoInClip = value;
                    UpdateCounter($"{ammoInClip}/{totalAmmo}");
                })
                .AddTo(subscriptions);

            gunManager.TotalAmmo.ObserveEveryValueChanged(property => property.Value)
                .Subscribe(value =>
                {
                    totalAmmo = value;
                    UpdateCounter($"{ammoInClip}/{totalAmmo}");
                })
                .AddTo(subscriptions);
        }
    }
}
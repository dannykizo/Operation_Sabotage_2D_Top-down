using UniRx;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace TopDown.UI
{
    //Base class for all counter
    public class UICounter : MonoBehaviour
    {
        protected CompositeDisposable subscriptions = new CompositeDisposable();
        protected TextMeshProUGUI counterText;

        [Header("Pop Scale Animation")]
        [SerializeField] private Vector3 punchPower = new Vector3(0.1f, 0.1f, 0.1f);
        [SerializeField] private float punchDuration = 0.1f;

        protected virtual void Awake()
        {
            counterText = GetComponent<TextMeshProUGUI>();
        }
        protected virtual void OnDisable()
        {
            subscriptions.Clear();
        }

        protected void UpdateCounter(string value)
        {
            counterText.text = value;
            counterText.transform.DOPunchScale(punchPower, punchDuration) //Popup animation
                .OnComplete(() => transform.DORewind()); //Reset scale to initial value when done (should happen automatically but this is for certainty)
        }
    }
}
using UnityEngine;
using DG.Tweening;

namespace TopDown.Animation
{
    public class PopupAnimation : MonoBehaviour
    {
        [SerializeField] private float popupTime = 0.5f;

        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, popupTime).SetUpdate(true);
        }
    }
}
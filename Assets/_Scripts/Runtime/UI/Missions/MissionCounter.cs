using UniRx;
using TMPro;
using UnityEngine;
using TopDown.Core;
using TopDown.Health;

namespace TopDown.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MissionCounter : UICounter
    {
        [Header("Mission Objects")]
        [SerializeField] private HealthComponent[] missionObjectives;

        [Header("Mission Text")]
        [SerializeField] private string missionName;
        private int completedObjectives;

        //Call base method and update mission counter
        protected override void Awake()
        {
            base.Awake();
            UpdateMissionCounter();
        }
        private void UpdateMissionCounter()
        {
            UpdateCounter($"{missionName}:{completedObjectives}/{missionObjectives.Length}");   //Update UI counter

            if (completedObjectives >= missionObjectives.Length)    //Check if all objectives completed
                GameManager.Instance.CompleteMission();
        }

        //Subscribe to all mission objects health component and update counter when one of them is destroyed
        private void OnEnable()
        {
            foreach (var objective in missionObjectives)
            {
                objective.CurrentHealth.ObserveEveryValueChanged(property => property.Value)
                    .Subscribe(value =>
                    {
                        if (value <= 0)
                        {
                            completedObjectives++;
                            UpdateMissionCounter();
                        }
                    })
                    .AddTo(subscriptions);
            }
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            completedObjectives = 0;
        }
    }
}
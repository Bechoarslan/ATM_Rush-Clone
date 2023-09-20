using Managers;
using Runtime.Enums;
using Runtime.Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Handler
{
    public class UIEventSubscriber : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIEventSubscriptionTypes type;
        [SerializeField] private Button button;

        #endregion

        #region Private Variables

        [ShowInInspector] private UIManager _manager;

        #endregion

        #endregion

        private void Awake()
        {
            FindReferences();
        }

        private void FindReferences()
        {
            _manager = FindObjectOfType<UIManager>();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            switch (type)
            {
                case UIEventSubscriptionTypes.OnPlay:
                {
                    button.onClick.AddListener(_manager.OnPlay);
                    break;
                }
                case UIEventSubscriptionTypes.OnNextLevel:
                {
                    button.onClick.AddListener(_manager.OnNextLevel);
                    break;
                }
                case UIEventSubscriptionTypes.OnRestartLevel:
                {
                    button.onClick.AddListener(_manager.OnRestartLevel);
                    break;
                }
                case UIEventSubscriptionTypes.OnIncomeUpdate:
                {
                    button.onClick.AddListener(_manager.OnIncomeUpdate);
                    break;
                }
                case UIEventSubscriptionTypes.OnStackUpdate:
                {
                    button.onClick.AddListener(_manager.OnStackUpdate);
                    break;
                }
            }
        }

        private void UnSubscribeEvents()
        {
            switch (type)
            {
                case UIEventSubscriptionTypes.OnPlay:
                {
                    button.onClick.RemoveListener(_manager.OnPlay);
                    break;
                }
                case UIEventSubscriptionTypes.OnNextLevel:
                {
                    button.onClick.RemoveListener(_manager.OnNextLevel);
                    break;
                }
                case UIEventSubscriptionTypes.OnRestartLevel:
                {
                    button.onClick.RemoveListener(_manager.OnRestartLevel);
                    break;
                }
            }
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}
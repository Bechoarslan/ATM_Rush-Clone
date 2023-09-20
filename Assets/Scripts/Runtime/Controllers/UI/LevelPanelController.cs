using Runtime.Signals;

using TMPro;
using UnityEngine;

namespace Runtime.Controllers.UI
{
    public class LevelPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshProUGUI levelText, moneyText;

        #endregion

        #region Serialized Variables

        private int _moneyValue;

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onSetNewLevelValue += OnSetNewLevelValue;
            UISignals.Instance.onGetMoneyValue += OnGetMoneyValue;
            UISignals.Instance.onSetMoneyValue += OnSetMoneyValue;
        }

        private void OnSetMoneyValue(int moneyValue)
        {
            _moneyValue = moneyValue;
            moneyText.text = moneyValue.ToString();
        }

        private int OnGetMoneyValue()
        {
            return _moneyValue;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onSetNewLevelValue -= OnSetNewLevelValue;
            UISignals.Instance.onGetMoneyValue -= OnGetMoneyValue;
            UISignals.Instance.onSetMoneyValue -= OnSetMoneyValue;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        public void OnSetNewLevelValue(byte levelValue)
        {
            levelText.text = "LEVEL " + ++levelValue;
        }
    }
}
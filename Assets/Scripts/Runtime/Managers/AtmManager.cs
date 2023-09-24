using System;
using DG.Tweening;
using Runtime.Signals;
using TMPro;
using UnityEngine;

namespace Runtime.Managers
{
    public class AtmManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private DOTweenAnimation _doTweenAnimation;
        [SerializeField] private TextMeshPro _atmText;

        #endregion

        #endregion

        private void Awake()
        {
            GetReference();
        }

        private void GetReference()
        {
            _doTweenAnimation = GetComponentInChildren<DOTweenAnimation>();
        }


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            AtmSignals.Instance.onSetAtmScoreText += OnSetAtmScoreText;
            CoreGameSignals.Instance.onAtmTouched += OnAtmTouched;
        }

        private void OnAtmTouched(GameObject touchedAtmObject)
        {
            if (touchedAtmObject.GetInstanceID() == gameObject.GetInstanceID())
            {
                _doTweenAnimation.DOPlay();
            }
        }

        private void OnSetAtmScoreText(int value)
        {
            _atmText.text = value.ToString();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            AtmSignals.Instance.onSetAtmScoreText -= OnSetAtmScoreText;
            CoreGameSignals.Instance.onAtmTouched -= OnAtmTouched;
        }
    }
}
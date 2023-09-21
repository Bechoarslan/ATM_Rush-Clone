using System;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animator;

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onChangePlayerAnimationState += OnChangePlayerAnimationState;
        }

        private void OnChangePlayerAnimationState(PlayerAnimationStates animationStates)
        {
            animator.SetTrigger(animationStates.ToString());
        }

        private void UnSubscribeEvents()
        {
            
            PlayerSignals.Instance.onChangePlayerAnimationState -= OnChangePlayerAnimationState;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        public void OnReset()
        {
            PlayerSignals.Instance.onChangePlayerAnimationState?.Invoke(PlayerAnimationStates.Idle);
        }
    }
}
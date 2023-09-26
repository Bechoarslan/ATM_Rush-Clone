using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Controllers.Player;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerMovementController _movementController;
        [SerializeField] private PlayerAnimationController _animationController;
        [SerializeField] private PlayerPhysicController _playerPhysicController;
        [SerializeField] private PlayerMeshController _playerMeshController;

        #endregion

        #region Private Variables

        private PlayerData _data;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetPlayerData();
            SendPlayerDataToControllers();
        }

        private PlayerData GetPlayerData()
        {
            return Resources.Load<CD_Player>("Data/CD_Player").Data;
        }

        private void SendPlayerDataToControllers()
        {
            _movementController.SetMovementData(_data.MovementData);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(true);
            InputSignals.Instance.onInputReleased += () =>PlayerSignals.Instance.onMoveConditionChanged?.Invoke(false);
            InputSignals.Instance.onInputDragged += OnInputDragged;
            CoreGameSignals.Instance.onLevelFailed  += () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(false);
            CoreGameSignals.Instance.onLevelSuccessful += () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(true);
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onMiniGameAreaEntered += OnMiniGameAreaEntered;
            PlayerSignals.Instance.onSetTotalScore += OnSetTotalScore;

        }

        private void OnSetTotalScore(int value)
        {
            PlayerSignals.Instance.onSetTotalScore?.Invoke(value);
        }

        private void OnPlay()
        {
            PlayerSignals.Instance.onPlayConditionChanged?.Invoke(true);
            PlayerSignals.Instance.onChangePlayerAnimationState?.Invoke(PlayerAnimationStates.Run);
        }
        
        private void OnInputDragged(HorizontalnputParams inputParams) => _movementController.UpdateInputValue(inputParams);
        
       

        private void OnMiniGameAreaEntered()
        {
            PlayerSignals.Instance.onPlayConditionChanged?.Invoke(false);
            StartCoroutine(WaitForFinal());
        }

        
        private void OnReset()
        {
            _movementController.OnReset();
            _animationController.OnReset();
        }

        private void UnSubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(true);
            InputSignals.Instance.onInputReleased -= () =>PlayerSignals.Instance.onMoveConditionChanged?.Invoke(false);
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            CoreGameSignals.Instance.onLevelFailed  -= () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(false);
            CoreGameSignals.Instance.onLevelSuccessful -= () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(true);
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onMiniGameAreaEntered -= OnMiniGameAreaEntered;
            PlayerSignals.Instance.onSetTotalScore -= OnSetTotalScore;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        internal void SetStackPosition()
        {
            var position = transform.position;
            Vector2 pos = new Vector2(position.x, position.z);
            StackSignals.Instance.onStackFollowPlayer?.Invoke(pos);
        }

        private IEnumerator WaitForFinal()
        {
            PlayerSignals.Instance.onChangePlayerAnimationState?.Invoke(PlayerAnimationStates.Idle);
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
            CoreGameSignals.Instance.onMiniGameStart?.Invoke();
        }
    }
}
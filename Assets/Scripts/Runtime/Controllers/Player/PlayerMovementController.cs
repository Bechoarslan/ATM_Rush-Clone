﻿using System;
using Runtime.Data.ValueObject;
using Runtime.Keys;
using Runtime.Managers;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private new Rigidbody rigidbody;

        #endregion

        #region Private Variables

        [Header("Data")] [ShowInInspector] private PlayerMovementData _data;
        [ShowInInspector] private bool _isReadyToMove, _isReadyToPlay;
        [ShowInInspector] private float _inputValue;
        [ShowInInspector] private Vector2 _clampValues;

        #endregion

        #endregion

        public void SetMovementData(PlayerMovementData dataMovementData)
        {
            _data = dataMovementData;
        }
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onMoveConditionChanged += OnMoveConditionChanged;
            PlayerSignals.Instance.onPlayConditionChanged += OnPlayConditionChanged;
        }
       
        private void OnPlayConditionChanged(bool condition) => _isReadyToPlay = condition;
        private void OnMoveConditionChanged(bool condition) => _isReadyToMove = condition;
        

        public void UpdateInputValue(HorizontalnputParams inputParams)
        {
            _inputValue = inputParams.HorizontalInputValue;
            _clampValues = inputParams.HorizontalInputClampSides;
        }

        private void Update()
        {
            if (_isReadyToPlay)
            {
                manager.SetStackPosition();
            }
        }

        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                if (_isReadyToMove)
                {
                    Move();
                }
                else
                {
                    StopSideWays();
                }
            }
            else
                Stop();
        }

        private void Move()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(_inputValue * _data.SidewaySpeed, velocity.y, _data.ForwardSpeed);
            rigidbody.velocity = velocity;
            var position = transform.position;
            position = new Vector3(Mathf.Clamp(rigidbody.position.x, _clampValues.x, _clampValues.y),(position = rigidbody.position).y, position.z);
            rigidbody.position = position;
        }

        private void StopSideWays()
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, _data.ForwardSpeed);
            rigidbody.angularVelocity = Vector3.zero;
        }

        private void Stop()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            
        }
        

        public void OnReset()
        {
            Stop();
            _isReadyToPlay = false;
            _isReadyToMove = false;
        }
    }
}
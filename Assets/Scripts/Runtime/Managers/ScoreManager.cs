﻿using System;
using _Modules.SaveModule.Scripts.Managers;
using Managers;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        [ShowInInspector] private int _money;
        [ShowInInspector] private int _stackValueMultiplier;
        [ShowInInspector] private int _scoreCache = 0;
        [ShowInInspector] private int _atmScoreValue = 0;

        #endregion

        #endregion

        private void Awake()
        {
            _money = GetMoneyValue();
            
        }
        
        
        
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onSendMoney += OnSendMoney;
            ScoreSignals.Instance.onGetMoney += () => _money;
            ScoreSignals.Instance.onSetScore += OnSetScore;
            ScoreSignals.Instance.onSetAtmScore += OnSetAtmScore;
            CoreGameSignals.Instance.onMiniGameStart +=
                () => ScoreSignals.Instance.onSendFinalScore?.Invoke(_scoreCache);
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onLevelSuccessful += RefreshMoney;
            CoreGameSignals.Instance.onLevelFailed += RefreshMoney;
            UISignals.Instance.onClickIncome += OnSetValueMultiplier;
            
        }

        private void OnSetValueMultiplier()
        {
            _stackValueMultiplier = CoreGameSignals.Instance.onGetIncomeLevel();
        }

        private void OnSetAtmScore(int atmValues)
        {
            _atmScoreValue += atmValues * _stackValueMultiplier;
            AtmSignals.Instance.onSetAtmScoreText?.Invoke(_atmScoreValue);
        }

        private void OnSetScore(int setScore)
        {
            _scoreCache = (setScore * _stackValueMultiplier) + _atmScoreValue;
            PlayerSignals.Instance.onSetTotalScore?.Invoke(_scoreCache);
        }

        private void OnSendMoney(int value)
        {
            _money = value;
        }

        private void UnSubscribeEvents()
        {
            ScoreSignals.Instance.onSendMoney -= OnSendMoney;
            ScoreSignals.Instance.onGetMoney -= () => _money;
            ScoreSignals.Instance.onSetScore -= OnSetScore;
            ScoreSignals.Instance.onSetAtmScore += OnSetAtmScore;
            CoreGameSignals.Instance.onMiniGameStart -=
                () => ScoreSignals.Instance.onSendFinalScore?.Invoke(_scoreCache);
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onLevelSuccessful -= RefreshMoney;
            CoreGameSignals.Instance.onLevelFailed -= RefreshMoney;
            UISignals.Instance.onClickIncome -= OnSetValueMultiplier;
            
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }


        private int GetMoneyValue()
        {
            return SaveDistributorManager.GetSaveData().Money;
        }
        private void Start()
        {
            SetValueMultiplier();
            RefreshMoney();
        }
        private void SetValueMultiplier()
        {
            _stackValueMultiplier = CoreGameSignals.Instance.onGetIncomeLevel();
        }
        
        private void RefreshMoney()
        {
            _money += (int)(_scoreCache * ScoreSignals.Instance.onGetMultiplier());
            UISignals.Instance.onSetMoneyValue?.Invoke(_money);
        }
        private void OnReset()
        {
            _scoreCache = 0;
            _atmScoreValue = 0;
        }
    }
}
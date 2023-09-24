using System;
using _Modules.SaveModule.Scripts.Managers;
using Runtime.Commands.Feature;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class FeatureManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public FeatureManager()
        {
            _onClickStackCommand = new OnClickStackCommand(this, ref _newPriceTag, ref _stackLevel);
            _onClickIncomeCommand = new OnClickIncomeCommand(this, ref _newPriceTag, ref _incomeLevel);
            
            
        }

        #endregion

        #region Private Variables

        private byte _incomeLevel = 1;
        private byte _stackLevel = 1;
        private int _newPriceTag;
        private readonly OnClickIncomeCommand _onClickIncomeCommand;
        private readonly OnClickStackCommand _onClickStackCommand;
        
        #endregion
        #endregion


        private void Awake()
        {
            _incomeLevel = LoadIncome();
            _stackLevel = LoadStack();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onClickIncome += _onClickIncomeCommand.Execute;
            UISignals.Instance.onClickStack += _onClickStackCommand.Execute;
            CoreGameSignals.Instance.onGetIncomeLevel += OnGetIncomeLevel;
            CoreGameSignals.Instance.onGetStackLevel += OnGetStackLevel;
        }

        private byte LoadIncome()
        {

            return SaveDistributorManager.GetSaveData().IncomeLevel;

        }

        private byte LoadStack()
        {
            return SaveDistributorManager.GetSaveData().StackLevel;
        }
        

        private int OnGetStackLevel() => _stackLevel;
        

        private int OnGetIncomeLevel() => _incomeLevel;
        


        internal void SaveFeatureData()
        {
            SaveDistributorManager.SaveData();
        }
    }
}
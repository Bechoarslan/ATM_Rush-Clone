
using System;
using Runtime.Extentions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        
        
        public UnityAction<byte> onSetNewLevelValue = delegate { };
        public UnityAction onSetIncomeLvlText = delegate { };
        public UnityAction onSetStackLvlText = delegate { };
        public UnityAction<int> onSetMoneyValue = delegate { };
        public Func<int> onGetMoneyValue = delegate { return 0; };
        
        public UnityAction onClickIncome = delegate {  };
        public UnityAction onClickStack = delegate {  };
    }
}
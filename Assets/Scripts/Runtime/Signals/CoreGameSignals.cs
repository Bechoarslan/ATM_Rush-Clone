using System;
using Runtime.Enums;
using Runtime.Extentions;
using Runtime.Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction<GameStates> onChangeGameStates = delegate { };
        public UnityAction<byte> onLevelInitialize = delegate { };
        public UnityAction onClearActiveLevel = delegate { };
        public UnityAction onLevelSuccessful = delegate { };
        public UnityAction onLevelFailed = delegate { };
        public UnityAction onNextLevel = delegate { };
        public UnityAction onRestartLevel = delegate { };
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };
        public Func<byte> onGetLevelID = delegate { return 0; };
        
        public UnityAction onMiniGameAreaEntered = delegate {  };
        public UnityAction<GameObject> onAtmTouched = delegate {  };
        public UnityAction onMiniGameStart = delegate {  };
        
       
        
        public Func<int> onGetIncomeLevel = delegate { return 0; };
        public Func<int> onGetStackLevel = delegate { return 0; };
    }
}
using System.Collections.Generic;
using Runtime.Data.ValueObject;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class ItemAdderOnStackCommand
    {
        private StackManager _stackManager;
        private List<GameObject> _collectableStack;
        private StackData _data;
        private Transform _levelHolder;
        public ItemAdderOnStackCommand(StackManager stackManager, ref List<GameObject> collectableStack, ref StackData data)
        {
            _stackManager = stackManager;
            _collectableStack = collectableStack;
            _data = data;
            
        }

        public void Execute(GameObject collectableObject)
        {
            if (_collectableStack.Count <= 0)
            {
                _collectableStack.Add(collectableObject);
                collectableObject.transform.SetParent(_stackManager.transform);
                collectableObject.transform.localPosition = Vector3.zero;
                
            }
            else
            {
                collectableObject.transform.SetParent(_stackManager.transform);
                Vector3 newPos = _collectableStack[^1].transform.localPosition;
                newPos.z += _data.CollectableOffsetInStack;
                collectableObject.transform.localPosition = newPos;
                _collectableStack.Add(collectableObject);
            }
            
            
        }
    }
}
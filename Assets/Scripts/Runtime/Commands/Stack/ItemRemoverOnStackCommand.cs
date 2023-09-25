using System.Collections.Generic;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class ItemRemoverOnStackCommand
    {
        private StackManager _stackManager;
        private List<GameObject> _collectableStack;
        private Transform _levelHolder;
        public ItemRemoverOnStackCommand(StackManager stackManager, ref List<GameObject> collectableStack)
        {
            _stackManager = stackManager;
            _collectableStack = collectableStack;
            _levelHolder = GameObject.Find("LevelHolder").transform;
        }

        public void Execute(GameObject collectableObject)
        {
            int index = _collectableStack.IndexOf(collectableObject);
            int last = _collectableStack.Count - 1;
            _collectableStack.Clear();
            _collectableStack.TrimExcess();
            collectableObject.transform.SetParent(_levelHolder);
            collectableObject.SetActive(false);
            _stackManager.StackJumperCommand.Execute(last,index);
            _stackManager.StackTypeUpdaterCommand.Execute();
        }
    }
}
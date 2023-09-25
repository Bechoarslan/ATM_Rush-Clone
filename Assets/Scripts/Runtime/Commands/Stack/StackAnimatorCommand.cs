using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Data.ValueObject;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class StackAnimatorCommand
    {
        private StackManager _stackManager;
        private StackData _data;
        private List<GameObject> _collectableStack;

        public StackAnimatorCommand(StackManager stackManager, StackData data, ref List<GameObject> collectableStack)
        {
            _stackManager = stackManager;
            _data = data;
            _collectableStack = collectableStack;
        }

        public IEnumerator Execute()
        {
            for (int i = 0; i <= _collectableStack.Count - 1; i++)
            {
                int index = (_collectableStack.Count - 1) - i;
                _collectableStack[index].transform
                    .DOScale(
                        new Vector3(_data.StackScaleValue, _data.StackScaleValue, _data.StackScaleValue),
                        _data.StackAnimDuraction).SetEase(Ease.Flash);
                _collectableStack[index].transform.DOScale(Vector3.one, _data.StackAnimDuraction)
                    .SetDelay(_data.StackAnimDuraction).SetEase(Ease.Flash);
                yield return new WaitForSeconds(_data.StackAnimDuraction / 3);
            }
        }
    }
}
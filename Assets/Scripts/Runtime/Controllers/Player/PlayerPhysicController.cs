using System;
using DG.Tweening;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private new Collider collider;
        

        #endregion

        #region Private Variables

        #region Private Variables

        private readonly string _obstacle = "Obstacle";
        private readonly string _atm = "ATM";
        private readonly string _collectable = "Collectable";
        private readonly string _miniGame = "MiniGameArea";

        #endregion

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_obstacle))
            {
                rigidbody.transform.DOMoveZ(rigidbody.transform.position.z - 10f, 1f).SetEase(Ease.OutBack);
                return;
            }

            if (other.CompareTag(_atm))
            {
                CoreGameSignals.Instance.onAtmTouched?.Invoke(other.gameObject);
                return;
            }

            if (other.CompareTag(_collectable))
            {
                other.tag = "Collected";
                StackSignals.Instance.onInteractionCollectable?.Invoke(other.transform.parent.gameObject);
                return;
            }

            if (other.CompareTag(_miniGame))
            {
                CoreGameSignals.Instance.onMiniGameAreaEntered?.Invoke();
                return;
            }
        }

        
    }
}
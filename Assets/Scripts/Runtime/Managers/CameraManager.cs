using Cinemachine;
using Runtime.Enums;
using Runtime.Signals;
using Signals;
using Unity.Mathematics;
using UnityEngine;

namespace Runtime.Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animator;
        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;

        #endregion

        #region Private Variables

        private float3 _initialPosition; 

        #endregion

        #endregion

        #region Event Subscriptions

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _initialPosition = transform.position;
        }
        
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            CameraSignals.Instance.onChangeCameraState += OnChangeCameraState;
            CameraSignals.Instance.onSetCinemachineTarget += OnSetCinemachineTarget;
        }

        private void OnSetCinemachineTarget()
        {
             var target = FindObjectOfType<PlayerManager>().transform;
             stateDrivenCamera.Follow = target;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            CameraSignals.Instance.onChangeCameraState -= OnChangeCameraState;
            CameraSignals.Instance.onSetCinemachineTarget -= OnSetCinemachineTarget;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnChangeCameraState(CameraStates state)
        {
            animator.SetTrigger(state.ToString());
        }

        private void OnReset()
        {
            CameraSignals.Instance.onChangeCameraState?.Invoke(CameraStates.Initial);
            stateDrivenCamera.Follow = null;
            stateDrivenCamera.LookAt = null;
            transform.position = _initialPosition;
        }
    }
}
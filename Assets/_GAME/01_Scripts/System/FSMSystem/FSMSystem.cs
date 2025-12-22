using System;
using UnityEngine;

namespace CONSTANT.FSMSystem
{
    public class FSMSystem : MonoBehaviour
    {
        public FSMState currentState;

        public void ChangeState(FSMState newState)
        {
            if (currentState != null)
            {
                currentState.ExitState();
            }
            currentState = newState;
            currentState.EnterState();
        }

        public void ChangeState(FSMState newState, object data)
        {
            currentState.ExitState();
            currentState = newState;
            currentState.EnterState(data);
        }

        protected virtual void FixedUpdate()
        {
            currentState?.FixedUpdateState();
        }

        protected virtual void Update()
        {
            currentState?.UpdateState();
        }

        protected virtual void LateUpdate()
        {
            currentState?.LateUpdateState();
        }

        public void OnEnterAnim()
        {
            currentState?.OnEnterAnim();
        }

        public void OnMiddleAnim()
        {
            currentState?.OnMiddleAnim();
        }

        public void OnExitAnim()
        {
            currentState?.OnExitAnim();
        }
    }
}
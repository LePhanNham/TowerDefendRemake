using System;
using UnityEngine;

namespace _01_Scripts.Control.Enemy
{
    public class EnemyDataBinding : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private string currentAction = CONSTANT.StateName.Action;
        private string currentState = CONSTANT.StateName.State;
        private void Awake()
        {
            if (_animator==null) _animator = GetComponent<Animator>();
        }

        public void SetAnim(bool action, int state)
        {
            if (_animator == null) return;
            _animator.SetBool(currentAction,action);
            _animator.SetInteger(currentState,state);
        }

        public void SetAnim(int state)
        {
            if (_animator == null) return;
            _animator.SetInteger(currentState, state);
        }
    }
    

}
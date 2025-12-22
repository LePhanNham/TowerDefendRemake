
    using System;
    using UnityEngine;
    using UnityEngine.Serialization;

    public class BallistaBinding : MonoBehaviour
    {
        [FormerlySerializedAs("_animator")] [SerializeField] private Animator animator;
        private readonly string idleState = CONSTANT.StateTurret.Idle;
        private readonly string attackState = CONSTANT.StateTurret.Attack;
        private void Awake()
        {
            if (animator==null) animator = GetComponent<Animator>();
        }

        public void SetAnim(string anim)
        {
            if (animator == null) return;
            animator.SetTrigger(anim);
        }

        public void SetAnimAttack()
        {
            animator.SetTrigger(attackState);
        }
        public void SetAnimIdle()
        {
            animator.SetTrigger(idleState);
        }

        public void ResetAnim()
        {
            animator.SetTrigger(idleState);
        }

    }

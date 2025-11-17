using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _01_Scripts.Control.Enemy.Bats
{
    public class BatEnemyControl : EnemyBase
    {
        private EnemyDataBinding enemyDataBinding;

        protected override void OnMove()
        {
            enemyDataBinding.SetAnim(false,3);
        }

        protected override void OnDie()
        {
            enemyDataBinding.SetAnim(9);
        }

        protected override void OnHit()
        {
        }
    }
    
}
using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _01_Scripts.Control.Enemy.Bats
{
    public class BatEnemyControl : EnemyBase
    {
        private EnemyDataBinding enemyDataBinding;
        private List<SpriteRenderer> allSprites;

        protected override void Awake()
        {
            base.Awake();
            enemyDataBinding = GetComponent<EnemyDataBinding>();
            allSprites = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>(true));
        }

        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnHit();
            }
        }

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
            for (int i = 0; i < allSprites.Count; i++)
            {
                var sr = allSprites[i];
                sr.DOKill(true);

                sr.DOFade(0f, 0.05f)
                    .SetLoops(10, LoopType.Yoyo)   
                    .SetEase(Ease.Linear);
            }
        }
    }
    
}
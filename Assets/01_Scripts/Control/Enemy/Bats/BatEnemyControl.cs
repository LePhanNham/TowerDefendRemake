using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using CONSTANT.FSMSystem;


namespace _01_Scripts.Control.Enemy.Bats
{
    public class BatEnemyControl : EnemyBase
    {
        private EnemyDataBinding enemyDataBinding;
        private BatEnemyMovevement movementState;
        private BatEnemyDead deadState;
        protected override void Awake()
        {
            base.Awake();
            movementState = new BatEnemyMovevement(this,enemyDataBinding);
            deadState = new BatEnemyDead(this, enemyDataBinding);

        }

        protected override void OnMove()
        {
            ChangeState(movementState);
        }

        protected override void Update()
        {
            Movement();
        }
        public void Movement()
        {
            pos = currentWayPoint.GetWaypointPosition(currentWayPointIndex);
            transform.position = Vector3.MoveTowards(transform.position, pos, enemyConfig.Speed * Time.deltaTime);
            if (transform.position.x <= pos.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (Vector3.Distance(transform.position, pos) < 0.1f)
            {
                if (currentWayPointIndex < currentWayPoint.GetLengthPoint() - 1)
                {
                    currentWayPointIndex++;
                    pos = currentWayPoint.GetWaypointPosition(currentWayPointIndex);
                }
                else
                {
                    ReachEndPoint();
                }
            }
        }
        protected override void OnDie()
        {
            ChangeState(deadState);
        }

        protected override void OnHit()
        {
        }
    }
    
}
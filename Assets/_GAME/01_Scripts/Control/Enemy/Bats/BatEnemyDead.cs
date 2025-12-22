
using _01_Scripts.Control.Enemy;
using _01_Scripts.Control.Enemy.Bats;
using CONSTANT.FSMSystem;

public class BatEnemyDead : FSMState
{
    private EnemyDataBinding enemyDataBinding;
    private BatEnemyControl enemyControl;
    public BatEnemyDead(BatEnemyControl enemyControl, EnemyDataBinding enemyDataBinding)
    {
        this.enemyControl = enemyControl;
        this.enemyDataBinding = enemyDataBinding;
    }

    public override void EnterState()
    {
        base.EnterState();
        enemyDataBinding.SetAnim(9);
    }
    public override void UpdateState()
    {
        base.UpdateState();
    }
    public override void ExitState()
    {
        base.ExitState();
        enemyDataBinding.SetAnim(false,0);
    }
}

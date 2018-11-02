using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Attack")]
public class AttackAction : Action {

    public override void InitialAction(Enemy enemy) {
        enemy.targetVelocity.x = 0;

        enemy.animator.SetTrigger("Attack");
    }

}

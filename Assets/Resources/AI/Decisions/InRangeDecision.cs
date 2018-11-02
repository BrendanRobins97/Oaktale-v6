using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/InRangeDecision")]
public class InRangeDecision : Decision
{
    [SerializeField] private float range = 20f;

    public override bool Decide(Enemy enemy)
    {
        return (enemy.transform.position - enemy.target.transform.position).magnitude <= range;
    }

}
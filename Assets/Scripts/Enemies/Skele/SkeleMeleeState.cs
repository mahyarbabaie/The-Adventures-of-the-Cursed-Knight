using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleMeleeState : ISkeleState
{
    private Skele skele;
    private float attackTimer;
    private float attackCoolDown = 2;
    private bool canAttack = true;

    public void Enter(Skele skele)
    {
        this.skele = skele;
    }

    public void Execute()
    {
        Attack();
        if (skele.Target == null) { skele.ChangeState(new SkeleIdleState()); }
    }

    public void Exit()
    {
        return;
    }

    public void OnTriggerEnter(Collider2D collider)
    {
        return;
    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCoolDown)
        {
            canAttack = true;
            attackTimer = 0;
        }
        if (canAttack)
        {
            canAttack = false;
            skele.MyAnimator.SetTrigger("attack");
        }
    }
}

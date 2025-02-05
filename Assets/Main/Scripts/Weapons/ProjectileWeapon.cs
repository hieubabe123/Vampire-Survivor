using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    protected float currentAttackInterval;
    protected int currentAttackCount;

    protected override void Update()
    {
        base.Update();

        if(currentAttackInterval > 0){
            currentAttackInterval -= Time.deltaTime;
            if(currentAttackInterval <= 0){
                Attack(currentAttackCount);
            }
        }
    }

    public override bool CanAttack()
    {
        if(currentAttackCount > 0){
            return true;
        }
        return base.CanAttack();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : WeaponController
{
    // Instantiate the knife at the Player's transform and Get Direction instatiate from DirectionChecker from KnifeBehaviour

    protected override void Start()
    {
        base.Start();
    }
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnKnife = Instantiate(weaponData.WeaponPrefab);
        spawnKnife.transform.position = transform.position;
        spawnKnife.GetComponent<KnifeBehaviour>().DirectionChecker(playerMovement.lastMovedVector);
    }
}

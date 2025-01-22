using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraController : WeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
    protected override void Attack(){
        base.Attack();
        GameObject spawnedAura = Instantiate(weaponData.WeaponPrefab);
        spawnedAura.transform.position = transform.position;
        spawnedAura.transform.parent = transform;
    }
}

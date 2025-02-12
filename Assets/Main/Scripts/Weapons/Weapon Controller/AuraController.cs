using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraController : WeaponController
{
    private GameObject spawnedAura;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();

        if (spawnedAura && spawnedAura.activeInHierarchy)
        {
            return;
        }

        spawnedAura = ObjectPooling.instance.GetObjectFromPool(weaponData);
        if (spawnedAura)
        {
            spawnedAura.transform.position = transform.position;
            spawnedAura.transform.parent = transform;
            spawnedAura.SetActive(true);
        }

    }

}

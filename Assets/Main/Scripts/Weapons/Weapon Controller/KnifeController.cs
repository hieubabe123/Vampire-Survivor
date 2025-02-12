using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : WeaponController
{
    // Instantiate the knife at the Player's transform and Get Direction instatiate from DirectionChecker from KnifeBehaviour
    private float minRandomPosX = -0.1f;
    private float maxRandomPosX = 0.2f;
    private float minRandomPosY = -0.3f;
    private float maxRandomPosY = 0.2f;

    protected override void Start()
    {
        base.Start();
    }
    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(ShootProjectile());
    }

    private IEnumerator ShootProjectile()
    {
        isAttacking = true;
        for (int projectile = 0; projectile < currentProjectileCount; projectile++)
        {
            GameObject spawnKnife = ObjectPooling.instance.GetObjectFromPool(weaponData);
            if (spawnKnife != null)
            {
                Vector3 spawnPositionOffset = new Vector3(Random.Range(minRandomPosX, maxRandomPosX), Random.Range(minRandomPosY, maxRandomPosY), 0);
                spawnKnife.transform.position = transform.position + spawnPositionOffset;
                spawnKnife.SetActive(true);
                spawnKnife.GetComponent<KnifeBehaviour>().DirectionChecker(playerMovement.lastMovedVector);
            }

            yield return new WaitForSeconds(0.3f);
        }
        isAttacking = false;
    }



}

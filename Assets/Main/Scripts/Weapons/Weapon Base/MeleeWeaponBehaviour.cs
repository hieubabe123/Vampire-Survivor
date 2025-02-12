using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//include Time to destroy gameObject (for Aura Weapon)
public class MeleeWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    private float destroyAfterSeconds;
    public float timeToDestroy;

    //current stats
    protected float currentDamage;
    protected float currentCooldownDuration;
    protected float currentSpeed;
    protected float currentPierce;

    private void Awake()
    {
        currentDamage = weaponData.DamageWeapon;
        currentSpeed = weaponData.SpeedWeapon;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }
    protected virtual void Start()
    {
        destroyAfterSeconds = timeToDestroy;
    }

    protected virtual void Update()
    {

        destroyAfterSeconds -= Time.deltaTime;
        if (destroyAfterSeconds <= 0)
        {
            gameObject.SetActive(false);
            destroyAfterSeconds = timeToDestroy;
        }
    }

    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight;
    }


    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage(), transform.position);
        }
        else if (other.CompareTag("Prop"))
        {
            if (other.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());
            }
        }
    }

}

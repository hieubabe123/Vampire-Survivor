using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Include Time to destroy gameObject, rotation of gameObject (for knife,....)
public class ProjectileWeaponBehaviours : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    protected Vector3 direction;
    public float destroyAfterSeconds;

    //current stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    private void Awake()
    {
        currentDamage = weaponData.DamageWeapon;
        currentSpeed = weaponData.SpeedWeapon;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight;
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirX = direction.x;
        float dirY = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirX < 0 && dirY == 0)
        { //left
            scale.x *= -1;
        }
        else if (dirX == 0 && dirY < 0)
        {  //down
            rotation.z = -90f;
        }
        else if (dirX == 0 && dirY > 0)
        {  //up
            rotation.z = 90f;
        }
        else if (dir.x > 0 && dir.y > 0)
        { //right up
            rotation.z = 45f;
        }
        else if (dir.x < 0 && dir.y > 0)
        {  //left up
            rotation.z = 135f;
        }
        else if (dir.x > 0 && dir.y < 0)
        { //right down
            rotation.z = -45f;
        }
        else if (dir.x < 0 && dir.y < 0)
        {  //left down
            rotation.z = -135f;
        }
        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage(), transform.position);
            ReducePierce();
        }
        else if (other.CompareTag("Prop"))
        {
            if (other.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());
                ReducePierce();
            }
        }
    }

    private void ReducePierce()
    {
        currentPierce--;
        if (currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}

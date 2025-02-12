using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// Get Base Script from Melee Weapon Behaviour
public class AuraBehaviour : MeleeWeaponBehaviour
{
    List<GameObject> markedEnemies;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !markedEnemies.Contains(other.gameObject))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage(), transform.position);
            markedEnemies.Add(other.gameObject);  //Mark the Enemy had been taken Damage & can not take other Damage from this Aura (Must other Aura)
        }
        else if (other.CompareTag("Prop"))
        {
            if (other.gameObject.TryGetComponent(out BreakableProps breakable) && !markedEnemies.Contains(other.gameObject))
            {
                breakable.TakeDamage(GetCurrentDamage());
                markedEnemies.Add(other.gameObject); //Mark the Prop had been taken Damage & can not take other Damage from this Aura (Must other Aura)
            }
        }
    }

    private void OnDisable()
    {
        if (!gameObject.scene.isLoaded)
        {
            return;
        }
        if (markedEnemies != null)
        {
            markedEnemies.Clear();
        }
    }
}

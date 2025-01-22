using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Include cooldown duration
public class WeaponController : MonoBehaviour
{

    [Header("Weapon Stat")] public WeaponScriptableObject weaponData;
    protected PlayerMovement playerMovement;
    private float currentCooldown;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentCooldown = weaponData.CooldownDuration;
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0){
            Attack();
        }
    }

    protected virtual void Attack(){
        currentCooldown = weaponData.CooldownDuration; //Set again cooldown time
    }
}

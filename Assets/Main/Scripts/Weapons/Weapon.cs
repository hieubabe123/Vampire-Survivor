using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [System.Serializable]
    public struct Stats
    {
        public string name, description;

        [Header("Visuals")]
        public ParticleSystem hitEffect;
        public Rect spawnVariance;

        [Header("Values")]
        public float lifespan;
        public float damage, damageVariance, area, speed, cooldown, projectileNumber, knockBack;
        public int number, piercing, maxIntances;

        //To combine 2 stats together
        //maybe should think like: Weapon.Stats currentStats = baseStats + levelUpStats;
        public static Stats operator +(Stats s1, Stats s2)
        {
            Stats result = new Stats();
            result.name = s2.name ?? s1.name;
            result.description = s2.description ?? s1.description;
            //result.projectilePrefab = s2.projectilePrefab ?? s1.projectilePrefab;
            //result.auraPrefab = s2.auraPrefab ?? s1.auraPrefab;
            result.hitEffect = s2.hitEffect ?? s1.hitEffect;
            result.spawnVariance = s2.spawnVariance;
            result.lifespan = s1.lifespan + s2.lifespan;
            result.damage = s1.damage + s2.damage;
            result.damageVariance = s1.damageVariance + s2.damageVariance; //final damage return (damage +/- damageVariance)
            result.area = s1.area + s2.area;
            result.speed = s1.speed + s2.speed;
            result.cooldown = s1.cooldown + s2.cooldown;
            result.number = s1.number + s2.number;
            result.piercing = s1.piercing + s2.piercing;
            result.projectileNumber = s1.projectileNumber + s2.projectileNumber;
            result.knockBack = s1.knockBack + s2.knockBack;
            return result;
        }

        public float GetDamage()
        {
            return damage + Random.Range(0,damageVariance);
        }
    }

    public int currentLevel, maxLevel;
    protected PlayerStats player;
    protected Stats currentStats;
    public WeaponData weaponData;
    protected float currentCooldown;
    protected PlayerMovement playerMovement;

    public virtual void InitialiseWeapon(WeaponData data){
        maxLevel = data.maxLevel;
        player = FindObjectOfType<PlayerStats>();

        data = weaponData;
        currentStats = data.baseStats;
        playerMovement = GetComponentInParent<PlayerMovement>();
        currentCooldown = currentStats.cooldown;
    }

    protected virtual void Awake(){
        if(weaponData){
            currentStats = weaponData.baseStats;
        }
    }

    protected virtual void Start(){
        if(weaponData){
            InitialiseWeapon(weaponData);
        }
    }

    protected virtual void Update(){
        currentCooldown -= Time.deltaTime;
        if(currentCooldown  <= 0f){
            Attack(currentStats.number);
        }
    }

    public virtual bool CanLevelUp(){
        return currentLevel <= maxLevel;
    }

    public virtual bool DoLevelUp(){
        if(!CanLevelUp()){
            Debug.LogWarning(string.Format("Cannot level up {0} to Level {1}, max level of {2} already reached.", name, currentLevel, weaponData.maxLevel));
            return false;
        }

        currentStats += weaponData.GetLevelData(++currentLevel);
        return true;
    }

    public virtual bool CanAttack(){
        return currentCooldown <= 0;
    }

    protected virtual bool Attack(int attackCount){
        if(CanAttack()){
            //now current Cooldown = 0 ==> Reset cooldown
            currentCooldown += currentStats.cooldown;
            return true;
        }
        return false;   
    }

    public virtual float GetDamage(){
        return currentStats.damage * player.CurrentMight;
    }

    public virtual Stats GetStats(){
        return currentStats;
    }
}

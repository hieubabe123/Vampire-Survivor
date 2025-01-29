using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private CharacterScriptableObject characterData;

    private float currentHealth;
    private float currentRecovery;
    private float currentMovespeed;
    private float currentMight;
    private float currentProjectilespeed;
    private float currentMagnet;

#region Current Stats Properties
    // To seperate private stat in PlayerStats script and child stat in another script
    public float CurrentHealth{
        get{return currentHealth;}
        set
        {
            //Check if the value has changed
            if(currentHealth!=value){
                currentHealth = value;
                if(GameManager.instance != null ){
                    GameManager.instance.currentHealthDisplay.text = "Health: " + CurrentHealth ;
                    
                }
                //Add any conditoional logic that need to be executed when the value changed
            }
        }
    }

    public float CurrentRecovery{
        get{return currentRecovery;}
        set
        {
            //Check if the value has changed
            if(currentRecovery!=value){
                currentRecovery = value;
                if(GameManager.instance != null ){
                    GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + CurrentRecovery ;
                }
                //Add any conditoional logic that need to be executed when the value changed
            }
        }
    }

    public float CurrentMovespeed{
        get{return currentMovespeed;}
        set
        {
            //Check if the value has changed
            if(currentMovespeed!=value){
                currentMovespeed = value;
                if(GameManager.instance != null ){
                    GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed: " + CurrentMovespeed ;
                }
                //Add any conditoional logic that need to be executed when the value changed
            }
        }
    }

    public float CurrentMight{
        get{return currentMight;}
        set
        {
            //Check if the value has changed
            if(currentMight!=value){
                currentMight = value;
                if(GameManager.instance != null ){
                    GameManager.instance.currentMightDisplay.text = "Might: " + CurrentMight ;
                }
                //Add any conditoional logic that need to be executed when the value changed
            }
        }
    }

    public float CurrentProjectilespeed{
        get{return currentProjectilespeed;}
        set
        {
            //Check if the value has changed
            if(currentProjectilespeed!=value){
                currentProjectilespeed = value;
                if(GameManager.instance != null ){
                    GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + CurrentProjectilespeed ;
                }
                //Add any conditoional logic that need to be executed when the value changed
            }
        }
    }

    public float CurrentMagnet{
        get{return currentMagnet;}
        set
        {
            //Check if the value has changed
            if(currentMagnet!=value){
                currentMagnet = value;
                if(GameManager.instance != null ){
                    GameManager.instance.currentMagnetDisplay.text = "Magnet: " + CurrentMagnet ;
                }
                //Add any conditoional logic that need to be executed when the value changed
            }
        }
    }
#endregion


#region Experience And Level
    [Header("Experience/Level")]
    [SerializeField] private int experience = 0;
    [SerializeField] private int level = 1;
    [SerializeField] private int experienceCap;


    //LevelRange
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }
    public List<LevelRange> levelRanges;
#endregion

    private InventoryManager inventoryManager;
    public int weaponIndex;
    public int passiveItemIndex;


    [Header("UI")]
    public Image healthBar;
    public Image expBar;
    public TMP_Text levelText;

    //I-Frames
    [Header("I-Frames")]
    public float invicibilityDuration;
    public float invicibilityTimer;
    public bool isInvicible;


    private void Awake()
    {
        //Select chareacter
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        //Select weapon and Passive Item

        inventoryManager = GetComponent<InventoryManager>();

        //Player's Stats
        CurrentHealth = characterData.MaxHealth;
        CurrentRecovery = characterData.Recovery;
        CurrentMovespeed = characterData.MoveSpeed;
        CurrentMight = characterData.Might;
        CurrentProjectilespeed = characterData.ProjectileSpeed;
        CurrentMagnet = characterData.Magnet;

        //Spawn starting weapon
        SpawnWeapon(characterData.StartingWeapon);
    }

    private void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;

        //Display of Player's Stat when starting Game
        GameManager.instance.currentHealthDisplay.text = "Health: " + CurrentHealth ;
        GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + CurrentRecovery ;
        GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed: " + CurrentMovespeed ;
        GameManager.instance.currentMightDisplay.text = "Might: " + CurrentMight ;
        GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + CurrentProjectilespeed ;
        GameManager.instance.currentMagnetDisplay.text = "Magnet: " + CurrentMagnet ;

        GameManager.instance.AssignChosenCharacterUI(characterData);
        
        UpdateHealthBar();
        UpdateExpBar();
        UpdateLevelText();
    }

    private void Update()
    {
        ReduceInviciblityTime();
        Recover(); // Update Recover Health after second
    }

    #region Executed Increase Exp and leveling Up, Update experience Bar and Text
    public void IncreaseExperience(int amount)
    {
        experience += amount;
        UpdateExpBar();
        LevelUpChecker();
    }

    private void LevelUpChecker()
    {
        level++;
        experience -= experienceCap;
        int experienceCapIncrease = 0;
        foreach (LevelRange range in levelRanges)
        {
            if (level >= range.startLevel && level <= range.endLevel)
            {
                experienceCapIncrease = range.experienceCapIncrease;
                break;
            }
        }
        experienceCap += experienceCapIncrease;
        UpdateLevelText();

        GameManager.instance.StartLevelUp();
    }

    private void UpdateExpBar(){
        expBar.fillAmount = (float)experience / experienceCap;
    }

    private void UpdateLevelText(){
        levelText.text = "LV " + level.ToString();
    }

    #endregion

    #region Executed Taking Damage and Being Killed
    public void TakeDamage(float dmg)
    {
        //if the player take damage, he will be invicible and can't take damage until invicible Time end
        if (!isInvicible)
        {
            CurrentHealth -= dmg;
            invicibilityTimer = invicibilityDuration;
            isInvicible = true;
            if (CurrentHealth <= 0)
            {
                Kill();
            }
            UpdateHealthBar();
        }
    }

    private void UpdateHealthBar(){
        healthBar.fillAmount = currentHealth / characterData.MaxHealth;
    }

    //Checking Take Damage to accept take damage after per second (Ex: When take damage, after 0.5s to take more damage)
    private void ReduceInviciblityTime(){
        if (invicibilityTimer > 0)
        {
            invicibilityTimer -= Time.deltaTime;
        }
        else if (isInvicible)
        { //If invicible Time reach to 0 and still have invicible --> It wil turn off invicible
            isInvicible = false;
        }
    }

    public void Kill()
    {
        
        Debug.Log("Killed");
        if(!GameManager.instance.isGameOver){
            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.AssignChosenWeaponsAndPassiveItemsUI(inventoryManager.weaponUI_Slots, inventoryManager.passiveItemUI_Slots);
            GameManager.instance.GameOver();
        }
    }
    #endregion

    #region Executed Recovery Health (per second and when taking heath potion)
    //Restore Health when take health potion
    public void RestoreHealth(float amount)
    {
        if (CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += amount;
            if (CurrentHealth > characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }
        }
    }

    //Recovery health (a little) per second
    public void Recover(){
        if(CurrentHealth < characterData.MaxHealth){
            CurrentHealth += CurrentRecovery * Time.deltaTime;

            if(CurrentHealth > characterData.MaxHealth){
                CurrentHealth = characterData.MaxHealth;
            }
        }
    }
    #endregion

    #region Executed Spawning Weapon and Passive Item
    public void SpawnWeapon(GameObject weapon){
        //Checking if the inventory slots are full --> return, not add more weapon to inventory
        if(weaponIndex >= inventoryManager.weaponSlots.Count - 1){
            Debug.LogError("Full Inventory");
            return;
        }
        GameObject spawnedWeapon = Instantiate(weapon,transform.position,Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);

        //Add weapon to Inventory Slot
        inventoryManager.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());

        weaponIndex++;
    }

    public void SpawnPassiveItem(GameObject passiveItem){
        //Checking if the inventory slots are full --> return, not add more passive item to inventory
        if(passiveItemIndex >= inventoryManager.weaponSlots.Count - 1){
            Debug.LogError("Full Inventory");
            return;
        }
        GameObject spawnedPassiveItem = Instantiate(passiveItem,transform.position,Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);

        //Add passive item to Inventory Slot
        inventoryManager.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());

        passiveItemIndex++;
    }
    #endregion
}

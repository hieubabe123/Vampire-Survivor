using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    private InventoryManager inventory;

    private void Start()
    {
        inventory = FindObjectOfType<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OpenTreasureChest();
            Destroy(gameObject);
        }
    }

    public void OpenTreasureChest()
    {
        //if can't get Evolved Weapon ==> Make weapon/passive item upgraded
        if (inventory.GetPossibleEvolution().Count <= 0)
        {
            GameManager.instance.StartLevelUp();
            Debug.LogWarning("No Availble Evolution");
            return;
        }
        WeaponEvolutionBlueprint toEvolve = inventory.GetPossibleEvolution()[Random.Range(0, inventory.GetPossibleEvolution().Count - 1)];
        inventory.EvolveWeapon(toEvolve);
    }

}



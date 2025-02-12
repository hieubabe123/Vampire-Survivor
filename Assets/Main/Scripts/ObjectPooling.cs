using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling instance;
    [SerializeField] private GameObject player;
    public int maxPoolSize; // Số lượng object mỗi pool

    private Dictionary<WeaponScriptableObject, List<GameObject>> weaponPools = new Dictionary<WeaponScriptableObject, List<GameObject>>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Hàm tạo pool cho một loại vũ khí
    private void CreatePool(WeaponScriptableObject weaponData)
    {
        if (weaponData == null || weaponPools.ContainsKey(weaponData))
        {
            return;
        }

        int poolSize = (weaponData.IsAuraWeapon) ? 1 : maxPoolSize;

        List<GameObject> newPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(weaponData.WeaponPrefab);
            obj.SetActive(false);
            newPool.Add(obj);
            obj.transform.SetParent(player.transform);
        }

        weaponPools.Add(weaponData, newPool);
    }

    // Hàm lấy vũ khí từ pool
    public GameObject GetObjectFromPool(WeaponScriptableObject weaponData)
    {
        if (weaponData == null)
            return null;

        // Kiểm tra nếu pool chưa tồn tại thì tạo mới
        if (!weaponPools.ContainsKey(weaponData))
        {
            CreatePool(weaponData);
        }

        foreach (var obj in weaponPools[weaponData])
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }

    public void ResetPool(WeaponScriptableObject oldWeapon)
    {
        if (oldWeapon == null || !weaponPools.ContainsKey(oldWeapon))
            return;
        foreach (var obj in weaponPools[oldWeapon])
        {
            Destroy(obj);
        }

        // Xóa khỏi dictionary
        weaponPools.Remove(oldWeapon);
    }
}

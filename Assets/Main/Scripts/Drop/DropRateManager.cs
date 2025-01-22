using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }

    public List <Drops> drops;

    void OnDestroy() {
        if(!gameObject.scene.isLoaded){
            return;
        }
        float randomNumber = Random.Range(0, 100f);
        List<Drops> possibleDrops = new List <Drops>();
        foreach (Drops rate in drops) {
            if(randomNumber <=  rate.dropRate){
                possibleDrops.Add(rate);
            }
        }
        if(possibleDrops.Count > 0){
            Drops dropItem = possibleDrops[Random.Range(0, possibleDrops.Count)];
            Instantiate(dropItem.itemPrefab, transform.position, Quaternion.identity);
        }
    }
}

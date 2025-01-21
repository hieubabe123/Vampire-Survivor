using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomizer : MonoBehaviour
{
    [SerializeField] private List<GameObject> propSpawnPoints;
    [SerializeField] private List<GameObject> propPrefabs;

    private void Start(){
        SpawnProps();
    }

    private void SpawnProps(){
        foreach(GameObject pos in propSpawnPoints){
            int randomProp = Random.Range(0, propPrefabs.Count);
            GameObject prop = Instantiate(propPrefabs[randomProp],pos.transform.position, Quaternion.identity);
            prop.transform.parent = pos.transform;
        }
    }
}

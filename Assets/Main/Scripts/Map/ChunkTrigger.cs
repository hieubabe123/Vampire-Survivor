using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    private MapController mapController;
    public GameObject targerMap;

    private void Start() {
        mapController = FindObjectOfType<MapController>();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            mapController.currentChunk = targerMap;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            mapController.currentChunk = null;
        }
    }
}

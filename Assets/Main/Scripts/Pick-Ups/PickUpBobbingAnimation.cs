using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBobbingAnimation : MonoBehaviour
{
    public float frequency;
    public float magtinude;
    public Vector3 direction;
    private Vector3 initialPosition;
    private PickUp pickUp;

    private void Start() {
        pickUp = GetComponent<PickUp>();
        initialPosition = this.transform.position;
    }

    private void Update() {
        if(pickUp != null && !pickUp.hasBeenCollected){
            transform.position = initialPosition + direction * Mathf.Sin(frequency * Time.time) * magtinude;

        }
    }
}

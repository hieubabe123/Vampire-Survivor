using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBobbingAnimation : MonoBehaviour
{
    public float frequency;
    public float magtinude;
    public Vector3 direction;
    private Vector3 initialPosition;

    private void Start() {
        initialPosition = this.transform.position;
    }

    private void Update() {
        transform.position = initialPosition + direction * Mathf.Sin(frequency * Time.time) * magtinude;
    }
}

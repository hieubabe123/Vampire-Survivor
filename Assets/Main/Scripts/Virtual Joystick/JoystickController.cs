using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    private void Update() {
        if(GameManager.instance != null && (GameManager.instance.isGameOver || GameManager.instance.isPausedGame || GameManager.instance.chosingUpgrade)){
            this.gameObject.SetActive(false);
        }else{
            this.gameObject.SetActive(true);
        }
    }
}

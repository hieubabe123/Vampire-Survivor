using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem inputSystem;
    private bool isMoving;
    [HideInInspector] public Vector3 moveDir;
    [HideInInspector] public Vector2 lastMovedVector;
    [HideInInspector] private float lastMovedVectorX;
    [HideInInspector] private float lastMovedVectorY;


    //References
    private Rigidbody2D rb2d;
    private PlayerStats playerStats;

    void Awake(){
        rb2d = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
    }
    void Start()
    {
        inputSystem = FindObjectOfType<InputSystem>();
        lastMovedVector = new Vector2(1,0f);                            //Start game --> Default direction weapon util player move
    }


    private void FixedUpdate() {
        Move();
        FlipSprite();
    }



    private void Move(){
        if(GameManager.instance.isGameOver){
            return;
        }
        moveDir = new Vector2 (inputSystem.GetMovementVectorNormalized().x,inputSystem.GetMovementVectorNormalized().y);
        float moveDistance = playerStats.CurrentMovespeed * Time.deltaTime;
        rb2d.velocity = moveDir * moveDistance;

        if(moveDir.x != 0){                  //Check last move Direction
            lastMovedVectorX = moveDir.x;
            lastMovedVector = new Vector2(lastMovedVectorX,0);
        }

        if(moveDir.y != 0){
            lastMovedVectorY = moveDir.y;
            lastMovedVector = new Vector2(0, lastMovedVectorY);
        }
        if(moveDir.x != 0 && moveDir.y != 0){
            lastMovedVector = new Vector2(lastMovedVectorX,lastMovedVectorY);
        }
    }

    private void FlipSprite(){
        if(moveDir.x != 0){
            lastMovedVectorX = moveDir.x;
        }
        Vector2 playerScale = new Vector2(Mathf.Sign(lastMovedVectorX),1f);
        this.transform.localScale = playerScale;
    }

    public bool IsMoving(){
        if(moveDir.x != 0 || moveDir.y != 0){
            isMoving = true;
        }else{
            isMoving = false;
        }
        return isMoving;
    }

}

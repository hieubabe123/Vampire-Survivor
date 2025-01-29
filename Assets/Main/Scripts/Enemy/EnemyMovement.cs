using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyStats enemyStats;
    [SerializeField] private Transform player;

    private Vector2 knockBackVelocity;
    private float knockBackDuration;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        enemyStats = GetComponent<EnemyStats>();
    }

    void Update()
    {
        //Check enemy be knocked back, if they're knocking back, they can't move
        if(knockBackDuration > 0){
            ProcessKnockBack();
        }else{
            MoveTowardPlayer();
        }
    }

    private void MoveTowardPlayer(){
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyStats.currentMoveSpeed * Time.deltaTime);
    }

    public void ReadyKnockBack(Vector2 velocity, float duration){
        if(knockBackDuration > 0){
            return;
        }

        knockBackVelocity = velocity;
        knockBackDuration = duration;
    }

    private void ProcessKnockBack(){
        transform.position += (Vector3)knockBackVelocity * Time.deltaTime;
        knockBackDuration -= Time.deltaTime;
    }
}

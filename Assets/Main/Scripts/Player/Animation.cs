using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    const string IS_MOVING = "isMoving";
    [SerializeField] private Animator animator;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveAnimation();
    }

    private void MoveAnimation(){
        animator.SetBool(IS_MOVING,playerMovement.IsMoving());
    }
}

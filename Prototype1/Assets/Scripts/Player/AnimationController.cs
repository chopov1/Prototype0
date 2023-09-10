using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    float MaxSpeed;

    int SpeedHash, AttackHash;

    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        SpeedHash = Animator.StringToHash("Speed");
        AttackHash = Animator.StringToHash("isAttacking");
    }

    public void SetPlayerController(PlayerController pc)
    {
        playerController = pc;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSpeed(float speed)
    {
        animator.SetFloat(SpeedHash, speed);
    }

    public void Attack()
    {
        animator.SetBool(AttackHash, true);
    }

    public void AttackBegin()
    {
        playerController.AttackBegin();
    }

    public void AttackComplete()
    {
        playerController.AttackComplete();
        animator.SetBool(AttackHash, false);
    }

}

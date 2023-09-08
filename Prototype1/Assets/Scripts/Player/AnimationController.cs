using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    float MaxSpeed;

    int speedHash, attackHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        speedHash = Animator.StringToHash("Speed");
        attackHash = Animator.StringToHash("Attack");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public float GetCurrentAnimTime()
    {
        return animator.GetCurrentAnimatorStateInfo(1).length;
    }

    public void SetSpeed(float speed)
    {
        animator.SetFloat(speedHash, speed);
    }

    public void Attack()
    {
        //animator.SetLayerWeight(1, 1);
        //animator.Play("Slash",1);
        animator.SetTrigger(attackHash);
    }

}

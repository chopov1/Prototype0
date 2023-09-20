using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Ianimatable
{
    public void AttackBegin();
    public void AttackComplete();
}

public class AnimationController : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    float MaxSpeed;

    int SpeedHash, AttackHash, DeadHash;

    Ianimatable animatableScript;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        SpeedHash = Animator.StringToHash("Speed");
        AttackHash = Animator.StringToHash("isAttacking");
        DeadHash = Animator.StringToHash("isDead");
    }

    public void SetAnimatableScript(Ianimatable script)
    {
        animatableScript = script;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSpeed(float speed)
    {
        if(animator == null)
        {
            return;
        }
        animator.SetFloat(SpeedHash, speed);
    }

    public void Attack()
    {
        animator.SetBool(AttackHash, true);
    }

    public void AttackBegin()
    {
        animatableScript.AttackBegin();
    }

    public void AttackComplete()
    {
        animatableScript.AttackComplete();
        animator.SetBool(AttackHash, false);
    }

    public void Die()
    {
        animator.SetBool(DeadHash, true);
    }

}

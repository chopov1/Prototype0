using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : DamagablePawn, Ianimatable
{


    [SerializeField]
    float rotationSpeed = 720, attackDistance, MaxSpeed, attackRadius, acceleration, decceleration;
    float currentSpeed;

    [SerializeField]
    GameObject weaponSprite,enemySprite;

    private GameObject player;

    Weapon weaponScript;

   
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animController = enemySprite.GetComponent<AnimationController>();
        animController.SetAnimatableScript(this);
        weaponScript = weaponSprite.GetComponent<Weapon>();
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case PawnState.alive:
                animController.SetSpeed(MaxSpeed);
                FacePlayer();
                Move();
                if (IsNearPlayer())
                {
                    Attack();
                }
                switch (weaponScript.currentState)
                {
                    case Weapon.WeaponState.Idle:
                        Move();
                        break;
                    default: 
                        break;
                }
                break;
            default:
                break;
        }
        
    }

    private void CalculateSpeed()
    {
        if (weaponScript.currentState == Weapon.WeaponState.Idle)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed -= decceleration * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, MaxSpeed);
    }

    void Attack()
    {
        animController.Attack();
    }

    void FacePlayer()
    {
        Vector3 dir = player.transform.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }

    public void AttackBegin()
    {
        weaponScript.SetWeaponState(Weapon.WeaponState.Attacking);
    }

    public void AttackComplete()
    {
        weaponScript.SetWeaponState(Weapon.WeaponState.Idle);
    }

    protected bool IsNearPlayer()
    {
        if(Vector3.Distance(player.transform.position, transform.position )<attackDistance)
        {
            return true;
        }
        return false;
    }

    protected override void Die()
    {
        base.Die();
        animController.Die();
        StartCoroutine(waitForDeathAnim());
    }

    public void ResetEnemy()
    {
        currentHealth = MaxHealth;
        state = PawnState.alive;
        UpdateHealthUI();
    }

    IEnumerator waitForDeathAnim()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }

    protected void Move()
    {
        CalculateSpeed();
        if (Vector3.Distance(player.transform.position, transform.position) > attackRadius) {
            transform.position += (player.transform.position - this.transform.position).normalized * currentSpeed * Time.deltaTime;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : DamagablePawn, Ianimatable
{
    Vector3 MoveVector;

    [SerializeField]
    float maxSpeed, rotationSpeed, acceleration, decceleration;

    [SerializeField]
    GameObject playerSprite, weaponSprite;
    Weapon weaponScript;

    [SerializeField]
    GameObject deathUI;

    float currentSpeed;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animController = playerSprite.GetComponent<AnimationController>();
        animController.SetAnimatableScript(this);
        weaponScript = weaponSprite.GetComponent<Weapon>();
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case PawnState.alive:
                MovePlayer();
                RotatePlayer();
                break;
            case PawnState.dead:
                
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case PawnState.alive:
                SetAnimationValues();
                CheckForAttack();
                break;
            case PawnState.dead:
                if (!deathUI.activeSelf)
                {
                    deathUI.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
                }
                break;
        }
    }

    private void SetAnimationValues()
    {
        animController.SetSpeed(currentSpeed);
    }
    
    private void CheckForAttack()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            animController.Attack();
        }
    }

    public void AttackBegin()
    {
        weaponScript.SetWeaponState(Weapon.WeaponState.Attacking);
    }

    public void AttackComplete()
    {
        weaponScript.SetWeaponState(Weapon.WeaponState.Idle);
    }

    #region Movement
    private void RotatePlayer()
    {
        if(isMoving())
        {
            Quaternion toRotation = Quaternion.LookRotation(MoveVector, Vector3.up);
            playerSprite.transform.rotation = Quaternion.RotateTowards(playerSprite.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void MovePlayer()
    {
        SetMoveVector();
        CalculateSpeed();
        transform.position += MoveVector * currentSpeed * Time.deltaTime;
        KeepInBounds();
    }

    private void SetMoveVector()
    {
        MoveVector.x = Input.GetAxisRaw("Horizontal");
        MoveVector.z = Input.GetAxisRaw("Vertical");
        MoveVector.Normalize();
    }

    private void CalculateSpeed()
    {
        if(isMoving())
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed -= decceleration * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
    }

    private bool isMoving()
    {
        if (MoveVector != Vector3.zero)
        {
            return true;
        }
        return false;
    }
    #endregion

    protected override void Die()
    {
        base.Die();
        animController.Die();
    }

    void KeepInBounds()
    {
        if(transform.position.x > 28)
        {
            transform.position = new Vector3(28, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -28)
        {
            transform.position = new Vector3(-28, transform.position.y, transform.position.z);
        }
        if (transform.position.z > 35)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 35);
        }
        if (transform.position.x < -35)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -35);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 MoveVector;

    [SerializeField]
    float maxSpeed, rotationSpeed, acceleration, decceleration;

    AnimationController animController;
    [SerializeField]
    GameObject playerSprite, weaponSprite;
    Weapon weaponScript;

    float currentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        animController = playerSprite.GetComponent<AnimationController>();
        weaponScript = weaponSprite.GetComponent<Weapon>();
    }

    private void FixedUpdate()
    {
        MovePlayer();   
        RotatePlayer();
        CheckForAttack();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimationValues();
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
            weaponScript.SetWeaponState(Weapon.WeaponState.Attacking, animController.GetCurrentAnimTime());
        }
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
}

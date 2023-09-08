using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponState { Idle, Attacking}
    public WeaponState currentState;
    CapsuleCollider hitCollider;
    // Start is called before the first frame update
    void Start()
    {
        hitCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ResetState(float animTime, WeaponState stateToResetTo)
    {
        yield return new WaitForSeconds(animTime);
        currentState = stateToResetTo;
    }

    public void SetWeaponState(WeaponState state, float animTime)
    {
        currentState = state;
        StartCoroutine(ResetState(animTime, WeaponState.Idle));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (currentState)
        {
            case WeaponState.Idle:
                Debug.Log("Idle Weapon Collided");
                break;
            case WeaponState.Attacking:
                Debug.Log("Attacking Weapon Collided");
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (currentState)
        {
            case WeaponState.Idle:
                Debug.Log("Idle Weapon Triggered");
                break;
            case WeaponState.Attacking:
                Debug.Log("Attacking Weapon Triggered");
                break;
        }
    }
}

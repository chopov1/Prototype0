using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponState { Idle, Attacking}
    public WeaponState currentState;
    CapsuleCollider hitCollider;

    [SerializeField]
    GameObject SwingTrail;
    // Start is called before the first frame update
    void Start()
    {
        hitCollider = GetComponent<CapsuleCollider>();
        //swingTrail = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        SetParticleVisibility();
    }

    void SetParticleVisibility()
    {
        switch (currentState)
        {
            case WeaponState.Idle:
                if (SwingTrail.activeSelf)
                {
                    SwingTrail.SetActive(false);
                }
                break;
            case WeaponState.Attacking:
                if (!SwingTrail.activeSelf)
                {
                    SwingTrail.SetActive(true);
                }
                break;
        }
    }

    
    public void SetWeaponState(WeaponState state)
    {
        currentState = state;
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

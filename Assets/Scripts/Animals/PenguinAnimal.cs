using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinAnimal : AnimalRoot
{
    [SerializeField] private float ExplosionAltitude;
    private void Update() 
    {
        HandleAttack();
        HandleUltimate();
    }

    private void HandleAttack()
    {
        if(currentAttackSpeed > 0)
        {
            currentAttackSpeed -= Time.deltaTime;
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                OnAttackButtonPressed();
                currentAttackSpeed = AttackSpeed;
            }    
        }
    }

    private void Eggxplosion()
    {
        rb.AddForce(Vector3.up * ExplosionAltitude,ForceMode.Impulse);
    }

    private void HandleUltimate()
    {
        if(currentUltimateCoolDown > 0)
        {
            currentUltimateCoolDown -= Time.deltaTime;
        }
        else
        {
            if(Input.GetKeyDown(ultimateKeyCode))
            {
                OnAnimalUltimate();
                currentUltimateCoolDown =UltimateCoolDown;
            }    
        }
    }

    public override void OnAttackButtonPressed()
    {
        base.OnAttackButtonPressed();
    }

    public override void OnAnimalUltimate()
    {
        Eggxplosion();
    }
}

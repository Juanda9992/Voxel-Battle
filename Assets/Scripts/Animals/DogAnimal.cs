using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAnimal : AnimalRoot
{
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

    private void HandleUltimate()
    {
        if(currentUltimateCoolDown > 0)
        {
            currentUltimateCoolDown -= Time.deltaTime;
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Space))
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
        base.OnAnimalUltimate();
        Debug.Log("Ultra Woof");
    }
}

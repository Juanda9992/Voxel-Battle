using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAnimal : AnimalRoot
{
    [SerializeField] private int timeBetweenUltimateAttackInMilliseconds;
    public override void OnAttackButtonPressed()
    {
        base.OnAttackButtonPressed();
        BasicAttack();
    }

    public override void OnAnimalUltimate()
    {
        base.OnAnimalUltimate();
        SpamAttacks();
    }

    private async void SpamAttacks()
    {
        controller.canMove = false;
        for(int i = 0; i < 5; i++)
        {
            BasicAttack();
            await System.Threading.Tasks.Task.Delay(timeBetweenUltimateAttackInMilliseconds);
        }
        controller.canMove = true;
    }

    public void BasicAttack()
    {
        Debug.Log("Damage Dealed");
    }
}

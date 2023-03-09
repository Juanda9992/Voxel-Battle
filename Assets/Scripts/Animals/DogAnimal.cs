using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
            base.OnAttackButtonPressed();
            BasicAttack();
            await System.Threading.Tasks.Task.Delay(timeBetweenUltimateAttackInMilliseconds);
        }
        controller.canMove = true;
    }

    public void BasicAttack()
    {
        Vector3 originalPos = transform.position;
        controller.canMove = false;
        transform.DOMove(transform.position + (transform.forward * 0.6f),0.1f).SetEase(attackEase);
        transform.DOMove(originalPos,0.2f).SetEase(attackEase).SetDelay(0.2f).OnComplete(()=>controller.canMove = true);
        
    }
}

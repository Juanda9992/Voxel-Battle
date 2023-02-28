using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinAnimal : AnimalRoot
{
    [SerializeField] private float ExplosionAltitude;
    [SerializeField] private int TimeToResetMovementInMilseconds;
    [SerializeField] private GameObject eggPrefab;
    [SerializeField] private Transform eggSpawnPosition;
    private async void Eggxplosion()
    {
        controller.canMove = false;
        Instantiate(eggPrefab,eggSpawnPosition.position,Quaternion.identity);
        rb.AddForce(Vector3.up * ExplosionAltitude,ForceMode.Impulse);
        await System.Threading.Tasks.Task.Delay(TimeToResetMovementInMilseconds);
        controller.canMove = true;
    }
    public override void OnAttackButtonPressed()
    {
        base.OnAttackButtonPressed();
    }

    public override void OnAnimalUltimate()
    {
        base.OnAnimalUltimate();
        Eggxplosion();
    }
}

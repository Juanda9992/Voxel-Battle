using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PenguinAnimal : AnimalRoot
{
    [Header("Penguin Setup")]
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
    public async override void OnAttackButtonPressed()
    {
        transform.DORotate(new Vector3(transform.rotation.eulerAngles.x + 15,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z),0.5f).SetEase(attackEase);
        await System.Threading.Tasks.Task.Delay(250);
        base.OnAttackButtonPressed();
    }

    public override void OnAnimalUltimate()
    {
        base.OnAnimalUltimate();
        Eggxplosion();
    }
}

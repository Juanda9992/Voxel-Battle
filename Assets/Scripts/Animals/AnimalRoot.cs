using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalRoot : MonoBehaviour
{
    [Header("Stats")]
    public string animalName;
    public float Health;
    public float Damage;
    public float AttackSpeed;
    public float UltimateCoolDown;

    [Header("Configurations")]
    [SerializeField] protected KeyCode ultimateKeyCode;
    protected float currentAttackSpeed;
    protected float currentUltimateCoolDown;
    protected Rigidbody rb;

    private void Start() 
    {
        currentAttackSpeed = AttackSpeed;
        currentUltimateCoolDown = UltimateCoolDown;
        rb = GetComponent<Rigidbody>();
    }
    public virtual void OnAttackButtonPressed()
    {
        Debug.Log($"{animalName} has attack");
    }

    public virtual void OnAnimalUltimate()
    {
        Debug.Log($"{animalName} has used Ultimate!");
    }
}

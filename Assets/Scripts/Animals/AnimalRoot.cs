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
    protected PlayerController controller;

    private void Start() 
    {
        currentAttackSpeed = AttackSpeed;
        currentUltimateCoolDown = UltimateCoolDown;
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<PlayerController>();
    }

    private void Update() 
    {
        if(this.controller.isSelected)
        {
            HandleAttack();
            HandleUltimate();
        }
    }
    public virtual void OnAttackButtonPressed()
    {
        Debug.Log($"{animalName} has attack");
    }

    public virtual void OnAnimalUltimate()
    {
        Debug.Log($"{animalName} has used Ultimate!");
    }
    public virtual void HandleAttack()
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

    public virtual void HandleUltimate()
    {
        if(this.currentUltimateCoolDown > 0)
        {
            this.currentUltimateCoolDown -= Time.deltaTime;
        }
        else
        {
            if(Input.GetKeyDown(ultimateKeyCode))
            {
                this.OnAnimalUltimate();
                this.currentUltimateCoolDown =this.UltimateCoolDown;
            }    
        }
    }
}

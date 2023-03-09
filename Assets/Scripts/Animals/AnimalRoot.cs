using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    [Header("Animations")]
    [SerializeField] protected Ease attackEase;
    private MeshRenderer[] mRenderer;

    [Header("Attack Settings")]
    [SerializeField] protected Transform attackPosition;
    [SerializeField] protected float attackRadius;
    private void Start() 
    {
        currentAttackSpeed = AttackSpeed;
        currentUltimateCoolDown = UltimateCoolDown;
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<PlayerController>();
        mRenderer = GetComponentsInChildren<MeshRenderer>();
    }

    private void Update() 
    {
        if(this.controller.isSelected)
        {
            HandleAttack();
            HandleUltimate();
        }
    }

    public void TakeDamage(float nextDamage)
    {
        Health-= nextDamage;
        foreach(var renderer in mRenderer)
        {
            renderer.material.DOColor(Color.red,0.25f).OnComplete(()=>
            {
                if(Health > 0)
                {
                    renderer.material.DOColor(Color.white,0.15f);
                }
                
                });
        }
        if(Health <= 0)
        {
            controller.canMove = false;
            transform.DOScale(0,1).SetDelay(0.8f).OnComplete(OnAnimalDeath);
        }
    }

    public virtual void OnAnimalDeath()
    {
        Destroy(this.gameObject);
    }
    public virtual void OnAttackButtonPressed()
    {
        Debug.Log("Enter here");
        Collider[] animalsDamaged = Physics.OverlapSphere(attackPosition.position,attackRadius);
        foreach(var animal in animalsDamaged)
        {
            if(animal.TryGetComponent<AnimalRoot>(out AnimalRoot extractedAnimal))
            {
                if(extractedAnimal != this)
                {
                    DamageText_Controller.damageText_Controller_Instance.InstantiateText(extractedAnimal.transform).SetDamageText(Damage);
                    extractedAnimal.TakeDamage(Damage);
                }
            }
        }
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

    public Rigidbody GetRigidBody()
    {
        return rb;
    }
}

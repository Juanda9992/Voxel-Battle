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
    public float SprintSpeed;
    protected bool isSprinting = false;

    [Header("Configurations")]
    [SerializeField] protected KeyCode ultimateKeyCode;
    [SerializeField] private  KeyCode sprintKeyCode = KeyCode.LeftShift;
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
            HandleSprint();
        }
    }

    public void TakeDamage(float nextDamage)
    {
        Health-= nextDamage;
        DamageText_Controller.damageText_Controller_Instance.InstantiateText(transform).SetDamageText(nextDamage);
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

    private void HandleSprint()
    {
        if(Input.GetKeyDown(sprintKeyCode))
        {
            OnAnimalSprintStart();
        }
        if(Input.GetKeyUp(sprintKeyCode	))
        {
            OnAnimalSprintEnd();
        }
        if(isSprinting)
        {
            OnAnimalSprint();
        }
    }

    public virtual void OnAnimalSprintStart()
    {
        controller.canMove = false;
        rb.freezeRotation = true;
        isSprinting = true;
    }

    public virtual void OnAnimalSprint()
    {
        controller.canMove = false;
        isSprinting = true;
    }

    public virtual void OnAnimalSprintEnd()
    {
        controller.canMove = true;
        isSprinting = false;
        rb.freezeRotation = false;
        rb.angularVelocity = Vector3.zero;
        DOTween.To(()=> (Vector3)rb.velocity,x=>rb.velocity = x, new Vector3(0,rb.velocity.y,0),0.4f).SetDelay(0.3f);
    }

    public Rigidbody GetRigidBody()
    {
        return rb;
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.TryGetComponent<AnimalRoot>(out AnimalRoot extractedAnimal))
        {
            if(extractedAnimal.isSprinting)
            {
                TakeDamage(2);
                rb.AddExplosionForce(10,other.transform.position,20,10,ForceMode.VelocityChange);
            }
        }    
    }
}

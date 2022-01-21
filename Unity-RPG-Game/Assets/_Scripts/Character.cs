using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{    
    [SerializeField] protected float speed;
    protected Vector2 direction;
    
    private Rigidbody2D myRigidbody;

    protected Animator myAnimator;

    [SerializeField] protected Transform hitBox;

   [SerializeField] protected Stat health;

   public Stat MyHealth
   {
       get { return health;}
   }
   
   [SerializeField] private float InitHealth;



    protected virtual void Start()
    {
        health.Initialize(InitHealth,InitHealth);

        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    protected virtual  void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Karakterin hareket etmesini sağlayan fonksiyondur. Yön değişkenini Player.cs scriptinden alır
    protected void Move()
    {
        myRigidbody.velocity = direction.normalized  * speed;
        if (myRigidbody.velocity != Vector2.zero)
        {
            myAnimator.SetBool("isWalking", true);
        }
        else
        {
            myAnimator.SetBool("isWalking", false);
        }

    }

    public virtual void TakeDamage(float damage)
    {
        print("Max " + health.MyMaxValue + "Current " + health.MyCurrentValue + "Damage: " + damage);
        
        health.MyCurrentValue -= damage;

        if (health.MyCurrentValue <= 0)
        {
            myAnimator.SetTrigger("Die");
        }
    }
}

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


    protected virtual void Start()
    {
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
}

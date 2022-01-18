using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    private Rigidbody2D myRigidbody;

    [SerializeField] private float speed;
    private Transform target;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Target").transform;
    }


    private void FixedUpdate()
    {
        Vector2 direction = target.position - transform.position;

        myRigidbody.velocity = direction.normalized * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Update()
    {
        
    }
}

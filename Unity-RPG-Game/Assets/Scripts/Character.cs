using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{    
    [SerializeField] protected float speed;
    protected Vector2 direction;
  

    protected virtual  void Update()
    {
        Move();
    }
    
    // Karakterin hareket etmesini sağlayan fonksiyondur. Yön değişkenini Player.cs scriptinden alır
    protected void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        
    }
}

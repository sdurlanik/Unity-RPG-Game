using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    private Rigidbody2D myRigidbody;

    [SerializeField] private GameObject poof;

    [SerializeField] private float speed;
    public Transform MyTarget { get; set; }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

    }


    private void FixedUpdate()
    {
        if (MyTarget != null)
        {
            // Büyünün yönünü hesaplar
            Vector2 direction = MyTarget.position - transform.position;

            // Rigidbody kullanarak büyüyü hareket ettirir
            myRigidbody.velocity = direction.normalized * speed;

            // Büyünün açısını hesaplar ( Görselin uç kısmı karaktere bakacak şekilde )
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            // Açı hesesabı yapıldıktan sonra döndürme işlemi yapılır
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
       
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Çarpılan nesne MyTarget ve tagı HitBox ise işlem yapar
        if (col.CompareTag("HitBox") && col.transform == MyTarget)
        {
            // poof efektini oluşturur
            Instantiate(poof, col.transform.position, Quaternion.identity);
            
            // Çarptıktan sonra büyünün buglı hareket etmesini engeller
            myRigidbody.velocity = Vector2.zero;
            
            // Hedefi sıfırlar
            MyTarget = null;
            
            // Büyü prefabını siler
            Destroy(gameObject);
        }
    }
}

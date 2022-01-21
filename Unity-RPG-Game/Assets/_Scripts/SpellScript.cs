using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{

    private Rigidbody2D myRigidbody;

    [SerializeField] private GameObject poof;

    [SerializeField] private float speed;
    public Transform MyTarget { get; private set; }

    private int damage;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

    }

    public void Initialize(Transform target, int damage)
    {
        this.MyTarget = target;
        this.damage = damage;
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
            speed = 0;
            
            // Çarpan nesnemiz hitbox olduğu için parenti olan enemy nesnesindeki enemy scriptine
            // ulaşıp TakeDamage() fonksiyonunu çağırıyoruz
            
            // poof efektini oluşturur
            Instantiate(poof, col.transform.position, Quaternion.identity);
            
            // Çarptıktan sonra büyünün buglı hareket etmesini engeller
            myRigidbody.velocity = Vector2.zero;
            
            col.GetComponentInParent<Enemy>().TakeDamage(damage);

            // Hedefi sıfırlar
            MyTarget = null;
            
            // Büyü prefabını siler
            Destroy(gameObject);
        }
    }
}

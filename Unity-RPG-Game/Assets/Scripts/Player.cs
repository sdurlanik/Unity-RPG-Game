using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    protected override void Update()
    {
        GetInput();
        
        // Character.cs scripti içerisindeki Update() fonksiyonunda bulunan komutları çalıştırır
        base.Update();
    }

    

    // Klavyeden girilen WASD değerlerini tespit eder ve Vector2 türündeki
    // direction değişkenine bu değeri aktarır. Klavyeden parmağımızı çektiğimizde
    // değer sıfırlanır (Vector2.zero)
    private void GetInput()
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
    }
}

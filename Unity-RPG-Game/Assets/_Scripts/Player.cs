using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]private Stat health;
    [SerializeField]private Stat mana;
    [SerializeField] private float InitHealth = 100;
    [SerializeField] private float InitMana = 50;
    
    private SpriteRenderer playerSpriteRenderer;
    private bool isFlipped = false;
    private bool coroutineRunning;

    [SerializeField] private GameObject leftSpear;
    [SerializeField] private GameObject rightSpear;

    [SerializeField] private GameObject[] spellPrefab;
    [SerializeField] private Transform[] spellExitPoints;
    private int spellExitIndex;
    
    [SerializeField] private Block[] blocks;
    private int blockIndex = 0;

    private GameObject target;







    protected override void Start()
    {
        health.Initialize(InitHealth,InitHealth);
        mana.Initialize(InitMana,InitMana);

        playerSpriteRenderer = GetComponent<SpriteRenderer>();

        target = GameObject.Find("Target");
        
        
        
        base.Start();
    }

    protected override void Update()
    {
        GetInput();
        SetSpearPosition();
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
            blockIndex = 2;
            direction += Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;

            if (isFlipped && !myAnimator.GetBool("rightAttack"))
            {
                spellExitIndex = 0;
                blockIndex = 0;
                playerSpriteRenderer.flipX = false;
                isFlipped = false;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            blockIndex = 3;
            direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
            if (!isFlipped && !myAnimator.GetBool("leftAttack"))
            {
                spellExitIndex = 1;
                blockIndex = 1;
                playerSpriteRenderer.flipX = true;
                isFlipped = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Block();
            myAnimator.SetBool("isAttacking", true);

            if (!coroutineRunning && InlineOfSight())
            {
                StartCoroutine(StartAttack());

            }
        }
    }

    // Karakter saldırı fonksiyonu
    private IEnumerator StartAttack()
    {
        coroutineRunning = true;
        
        myAnimator.SetBool("isAttacking",true);
        
        if (isFlipped)
        {
            myAnimator.SetBool("rightAttack",true);
            myAnimator.SetBool("leftAttack", false);
        }
        else
        {
            myAnimator.SetBool("leftAttack", true);
            myAnimator.SetBool("rightAttack",false);

        }
        
        
        yield return new WaitForSeconds(1); // Debug için hardcode (Daha sonra değiştirilecek)
        
        myAnimator.SetBool("leftAttack", false);
        myAnimator.SetBool("rightAttack", false);
        myAnimator.SetBool("isAttacking", false);
        
        Castspell();
        coroutineRunning = false;
    }

    public void Castspell()
    {
        Instantiate(spellPrefab[0], spellExitPoints[spellExitIndex].position, Quaternion.identity);
    }

    private void SetSpearPosition()
    {
        if (isFlipped)
        {
            leftSpear.SetActive(false);
            rightSpear.SetActive(true);
        }
        else
        {
            leftSpear.SetActive(true);
            rightSpear.SetActive(false);
        }
    }

    // Targetin playerın görüş açısında olup olmadığını kontrol eder
    private bool InlineOfSight()
    {
        Vector3 targetDirection = (target.transform.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection,
            Vector2.Distance(transform.position, target.transform.position),256);

        if (hit.collider == null)
        {
            return true;
        }

        return false;
    }

    private void Block()
    {
        foreach (Block b in blocks)
        {
            b.Deactivate();
        }
        
        blocks[blockIndex].Activate();
    }
}

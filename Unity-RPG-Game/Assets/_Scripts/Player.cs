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
    [SerializeField] private Transform[] spellExitPoints;
    private int spellExitIndex;

    private SpellBook spellBook;
    
    [SerializeField] private Block[] blocks;
    private int blockIndex = 0;

    public Transform MyTarget { get; set; }







    protected override void Start()
    {
        spellBook = GetComponent<SpellBook>();
        health.Initialize(InitHealth,InitHealth);
        mana.Initialize(InitMana,InitMana);

        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        
        
        
        
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
    }

    // Karakter saldırı fonksiyonu
    private IEnumerator StartAttack(int spellIndex)
    {
        coroutineRunning = true;
        Spell newSpell = spellBook.CastSpell(spellIndex);
        
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
        
        
        yield return new WaitForSeconds(newSpell.MyCastTime);
        
        myAnimator.SetBool("leftAttack", false);
        myAnimator.SetBool("rightAttack", false);
        myAnimator.SetBool("isAttacking", false);
        
        SpellScript s = Instantiate(newSpell.MySpellPrefab, spellExitPoints[spellExitIndex].position, Quaternion.identity).GetComponent<SpellScript>();
        s.MyTarget = MyTarget;
        coroutineRunning = false;
    }

    public void Castspell(int spellIndex)
    {
        Block();
        myAnimator.SetBool("isAttacking", true);

        if (MyTarget !=null && !coroutineRunning && InlineOfSight())
        {
            StartCoroutine(StartAttack(spellIndex));

        }
        
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
        Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection,
            Vector2.Distance(transform.position, MyTarget.transform.position),256);

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

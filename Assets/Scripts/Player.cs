using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public Image[] hearts;
    public int maxHealth;
    public GameObject sword;
    public float thrustPower;
    public bool canMove;
    public bool canAttack;

    private Animator _anim;
    private int _currentHealth;

    // Start is called before the first frame update
    public void Start()
    {
        _anim = GetComponent<Animator>();
        _currentHealth = maxHealth;
    }

    // Update is called once per frame
    public void Update()
    {
        GetHealth();
        Movement();
        CheatHealth();
    }

    private void Attack()
    {
        if (!canAttack)
        {
            return;
        }
        canMove = false;
        canAttack = false;
        var newSword = Instantiate(sword, transform.position, sword.transform.rotation);

        if (_currentHealth == maxHealth)
        {
            newSword.GetComponent<Sword>().special = true;
            canMove = true;
            thrustPower = 500;
        }
        
        #region SwordRotation
        var swordDirection = _anim.GetInteger("dir");
        _anim.SetInteger("attackDir", swordDirection);
        switch (swordDirection)
        {
            case 0:
                newSword.transform.Rotate(0, 0, 0);
                newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.up * thrustPower);
                break;
            case 1:
                newSword.transform.Rotate(0, 0, 180);
                newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.down * thrustPower);
                break;
            case 2:
                newSword.transform.Rotate(0, 0, 90);
                newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.left * thrustPower);
                break;
            case 3:
                newSword.transform.Rotate(0, 0, -90);
                newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrustPower);
                break;
        }
        #endregion
    }

    private void CheatHealth()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            IncreaseCurrentHealth();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            DecreaseCurrentHealth();
        }
    }

    private void DecreaseCurrentHealth()
    {
        if (_currentHealth > 0)
        {
            _currentHealth--;
        }
    }

    private void IncreaseCurrentHealth()
    {
        if (_currentHealth < maxHealth)
        {
            _currentHealth++;
        }
    }

    private void GetHealth()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < _currentHealth; i++)
        {
            hearts[i].gameObject.SetActive(true);
        }
    }

    private void Movement()
    {
        if (!canMove)
        {
            return;
        }
        if (Input.GetKey(KeyCode.W))
        {
            MoveDown();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MoveUp();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        else
        {
            _anim.speed = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void MoveRight()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        _anim.SetInteger("dir", 3);
        _anim.speed = 1;
    }

    private void MoveLeft()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
        _anim.SetInteger("dir", 2);
        _anim.speed = 1;
    }

    private void MoveUp()
    {
        transform.Translate(0, -speed * Time.deltaTime, 0);
        _anim.SetInteger("dir", 1);
        _anim.speed = 1;
    }

    private void MoveDown()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
        _anim.SetInteger("dir", 0);
        _anim.speed = 1;
    }
}
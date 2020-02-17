using UnityEngine;

public class Sword : MonoBehaviour
{

    public bool special;
    public GameObject swordParticle;
    
    private float _timer = .15f;
    private float _specialTimer = 1f;
    
    // Start is called before the first frame update
    public void Start()
    {
    }

    // Update is called once per frame
    public void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("attackDir", 5);
        }
        if (!special && _timer <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMove = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack = true;
            Destroy(gameObject);
        }
        _specialTimer -= Time.deltaTime;
        if (_specialTimer <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack = true;
            Instantiate(swordParticle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
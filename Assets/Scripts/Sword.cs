using UnityEngine;

public class Sword : MonoBehaviour
{

    private float _timer = .15f;
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMove = true;
            Destroy(gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoor : MonoBehaviour
{
    //configuration parameters
    [SerializeField] Enemy[] _enemyThrowers;
    [SerializeField] float _fallSpeed;

    //state
    int _deadCount;

    //cached component references
    Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _deadCount = 0;
        foreach(Enemy enemy in _enemyThrowers)
        {
            if(!enemy.GetComponent<BoxCollider2D>())
            _deadCount++;
        }
       
    }

    private void FixedUpdate()
    {
        if (_deadCount >= _enemyThrowers.Length)
        {
            Fall();
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(0f, 0f);
        }
    }

    private void Fall()
    {
        _rigidbody2D.position -= new Vector2(0f, _fallSpeed);
    }
}

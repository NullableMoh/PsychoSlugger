using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //configuration parameters
    [SerializeField] private AudioClip _enemyDamage;
    [SerializeField] private EnemyJumper _enemyToThrow;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private Vector2 _playerAttackRange;
    [SerializeField] private bool _hasHealth;
    [SerializeField] private int _health;

    //state
    

    //cached component references
    Animator _animator;
    AudioSource _audioSource;
    BoxCollider2D _boxCollider2D;

    //properties
    public EnemyJumper EnemyToThrow
    {
        set
        {
            _enemyToThrow = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _boxCollider2D = GetComponent<BoxCollider2D>();

        if(!_hasHealth)
        {
            _health = 1;
        }

        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerFist playerFist = collision.gameObject.GetComponent<PlayerFist>();
        if(playerFist && FindObjectOfType<Player>().HasClickedPunch)
        {
            _health--;

            if(_health <= 0)
            {
                _animator.SetBool("isDying", true);
                Die();
            }
            else
            {
                _animator.SetBool("isTakingDamage", true);
            }

            if (_boxCollider2D)
            {
                _audioSource.PlayOneShot(_enemyDamage);
            }
        }
    }

    public void StopTakeDamage()
    {
        _animator.SetBool("isTakingDamage", false);
    }

    public void Die()
    {
        Destroy(_boxCollider2D);
    }

    public void ThrowEnemy()
    {
        EnemyJumper spawnedEnemy = Instantiate(_enemyToThrow, GetComponentInChildren<EnemyThrowerSlot>().transform.position, Quaternion.identity);
    }

    public void StopThrowEnemy()
    {
        _animator.SetBool("isThrowing", false);
    }

    private IEnumerator SpawnEnemy()
    {
        while( _boxCollider2D)
        {
            if(Mathf.Abs(transform.position.x - FindObjectOfType<Player>().transform.position.x) < _playerAttackRange.x && Mathf.Abs(transform.position.y - FindObjectOfType<Player>().transform.position.y) < _playerAttackRange.y)
            {
                _animator.SetBool("isThrowing", true);
            }
            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}

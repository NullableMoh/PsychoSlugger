using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{


    //config params
    [SerializeField] float _moveSpeed;
    [SerializeField] float _punchSpeed;
    [SerializeField] int _health;
    [SerializeField] AudioClip _takeDamageSound;
    [SerializeField] AudioClip _punchSound;
    [SerializeField] AudioClip _walkingSound;
    [SerializeField] float _flyUpMoveSpeed;

    //state
    private float _direction;

    private bool _playerControlsEnabled;
    private bool _hasClickedPunch;
    private bool _flyVelocity;

    //cached component references
    Rigidbody2D _rigidbody;
    Animator _animator;
    AudioSource _audioSource;
    BoxCollider2D _boxCollider2D;
    CircleCollider2D _circleCollider2D;

    //properties
    public bool HasClickedPunch { get { return _hasClickedPunch; } }

    //called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _circleCollider2D = GetComponent<CircleCollider2D>();

        _animator = GetComponent<Animator>();

        _audioSource = GetComponent<AudioSource>();

        _direction = 1f;

        _playerControlsEnabled = true;
        _hasClickedPunch = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(_rigidbody.velocity.x) >= _moveSpeed / 100)
        {
            _animator.SetBool("isWalking", true);
        }

        else if (Mathf.Abs(_rigidbody.velocity.x) < _moveSpeed / 100)
        {
            _animator.SetBool("isWalking", false);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void FixedUpdate()
    {
        if (_boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Foreground")) || _boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Elevator")))
        {
            _playerControlsEnabled = true;
            GetComponentInChildren<PolygonCollider2D>().isTrigger = true;
            _animator.SetBool("isFalling", false);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, -10f);
        }

        else if (!_boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Foreground")) && !_boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Elevator")))
        {

            _animator.SetBool("isFalling", true);
            GetComponentInChildren<PolygonCollider2D>().isTrigger = true;
            _playerControlsEnabled = false;
            _rigidbody.velocity = new Vector2(0f, -10f);
        }
        else
        {
            GetComponentInChildren<PolygonCollider2D>().isTrigger = true;
           // _animator.SetBool("isFalling", false);
            _playerControlsEnabled = true;

        }

        if (_health <= 0)
        {
            _rigidbody.velocity = new Vector2(0f, _rigidbody.velocity.y);
            _playerControlsEnabled = false;
            _animator.SetBool("isDying", true);
        }
        if (_health <= 0)
        {
            _playerControlsEnabled = false;
            _animator.SetBool("isDying", true);
        }


        if (_playerControlsEnabled)
        {
            Move();
        }

        Punch();
        FlipSprite();

    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(
                                            Mathf.Sign(Input.GetAxis("Horizontal"))
                                            * _moveSpeed
                                            * Time.deltaTime
                                            * Convert.ToInt32(Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Epsilon)
                                            , _rigidbody.velocity.y);

        if (!_audioSource.isPlaying && Mathf.Abs(_rigidbody.velocity.x) > Mathf.Epsilon)
            _audioSource.PlayOneShot(_walkingSound);
    }

    private void FlipSprite()
    {
            if (_rigidbody.velocity.x != 0)
            {
                _direction = Mathf.Sign(_rigidbody.velocity.x);
            }
            transform.localScale = new Vector3(_direction, 1f, 1f);
            
    }

    private void Punch()
    {
        //if player clicks mouse, play punch animation, and give player x velocity in direction they are facing, and disable ability to move manually with a bool, as well as disabling the ability to punch again, with a bool
        //when they hit a wall (add a collider at the player's hand, to detect collision) stop animation, and return ability to move with the bool

        if(!_hasClickedPunch && Input.GetButton("Fire1") && Mathf.Abs(_rigidbody.velocity.x) > 0)
        {
            _hasClickedPunch = true;

            _animator.SetBool("isPunching", true);

            _playerControlsEnabled = false;

            GetComponentInChildren<PolygonCollider2D>().isTrigger = false;
        }

        if(_flyVelocity)
        {
            _rigidbody.velocity = new Vector2(_punchSpeed * Time.deltaTime * _direction, _flyUpMoveSpeed * Time.deltaTime);
        }

    }

    public void EnableFlyVelocity()
    {
        _flyVelocity = true;
        _audioSource.PlayOneShot(_punchSound);
    }

    //can also be used to stop after time has run out
    public void OnFistWallCollision()
    {
        _flyVelocity = false;
        _rigidbody.velocity = new Vector2(0f, -10f);

        _audioSource.Stop();

        _animator.SetBool("isPunching", false);
        _animator.SetBool("isWalking", true);
        _playerControlsEnabled = true;
        _hasClickedPunch = false;
    }

    public void OnFistEnemyCollision()
    {
        _flyVelocity = false;
        _rigidbody.velocity = new Vector2(0f, 0f);

        _audioSource.Stop();

        _playerControlsEnabled = true;
        _hasClickedPunch = false;
        _animator.SetBool("isPunching", false);
        GetComponentInChildren<PolygonCollider2D>().isTrigger = true;
       // _animator.SetBool("isFalling", false);
    }

    public void StopFlying()
    {
        OnFistWallCollision();
    }

    public void TakeKnockback()
    {
        OnFistWallCollision();
        _animator.SetBool("isTakingDamage", true);
        _audioSource.PlayOneShot(_takeDamageSound);
        
        if(!_flyVelocity)
            _health--;
    }

    public void StopTakeKnockback()
    {
        _animator.SetBool("isTakingDamage", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopFlying();
    }
}

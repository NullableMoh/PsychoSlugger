using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumper : MonoBehaviour
{
    //configuration parameters
    [SerializeField] Vector2 _jumpSpeed;
    [SerializeField] bool _fallDownFast;
    [SerializeField] float _fallDownFastSpeed;

    //state
    int i;

    //cached component references
    Rigidbody2D _rigidbody2D;
    BoxCollider2D _boxCollider2D;
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();

        _animator.SetBool("isFlying", true);

        i = 0;

    }



    void FixedUpdate()
    {
        if(!_fallDownFast)
        {
            for (; i < 1; i++)
            {
                _rigidbody2D.velocity = new Vector2(_jumpSpeed.x * Time.deltaTime, _jumpSpeed.y * Time.deltaTime);
            } 
        }
        
        else
        {
            for (; i < 1; i++)
            {
                _rigidbody2D.velocity = new Vector2(_jumpSpeed.x * Time.deltaTime, _fallDownFastSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            _animator.SetBool("isFlying", false);
            _animator.SetBool("isDying", true);

            //player.GetComponent<Animator>().SetBool("isFlying", false);
            //player.GetComponent<Animator>().SetBool("isWalking", false);
            //player.GetComponent<Animator>().SetBool("isTakingDamage", true);
            //player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, player.GetComponent<Rigidbody2D>().velocity.y);
            player.TakeKnockback();
        }
    }
}

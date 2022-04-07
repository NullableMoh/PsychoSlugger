using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    //configuration parameters
    [SerializeField] float _moveSpeed;
    [SerializeField] float _playerTeleportOffset;
    [SerializeField] float _centerTeleportationRandomOffest;

    [SerializeField] bool _centerTeleportation;


    //state


    //cached component references
    Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(0f, _moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if(player)
        {
            if(_centerTeleportation)
            {
                player.transform.position = new Vector2(Random.Range(transform.position.x - _centerTeleportationRandomOffest, transform.position.x + _centerTeleportationRandomOffest), transform.position.y + _playerTeleportOffset);
            }
            else
            {
                player.transform.position = new Vector2(player.transform.position.magnitude, transform.position.y + _playerTeleportOffset);
            }
        }

        ChangePos changePos = collision.gameObject.GetComponent<ChangePos>();
        if (changePos)
        {
            _moveSpeed *= -1f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LookAtEnemy : MonoBehaviour
{
    //Configuration Parameters
    [SerializeField] private CinemachineVirtualCamera _vCamPlayer;
    [SerializeField] private CinemachineVirtualCamera _vCamLookAtEnemy;

    [SerializeField] private Enemy _enemyToLookAt;

    //state 
    private bool _firstLookDone;

    //cached component refernces
    BoxCollider2D _boxCollider2D;

    //properties
    public bool FirstLookDone { get { return _firstLookDone; } }

    // Start is called before the first frame update
    void Start()
    {

        _boxCollider2D = GetComponent<BoxCollider2D>();

        _firstLookDone = false;
        _vCamLookAtEnemy.Follow = _enemyToLookAt.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<Player>().transform.localScale.x == -1)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void LookAtPlayerCamera()
    {
        _vCamLookAtEnemy.Priority = 0;
        _vCamPlayer.Priority = 1;
    } 
    private void LookAtEnemyCamera()
    {
        _vCamPlayer.Priority = 0;
        _vCamLookAtEnemy.Priority = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _vCamLookAtEnemy.Follow = _enemyToLookAt.transform;
        Player player = collision.gameObject.GetComponent<Player>();
        if(player)
        {
            LookAtEnemyCamera();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Destroy(_boxCollider2D);
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
        }
        LookAtPlayerCamera();
        _firstLookDone = true;
    }
}

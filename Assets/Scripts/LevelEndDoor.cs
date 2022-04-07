using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndDoor : MonoBehaviour
{
    //configuration parameters
    [SerializeField] AudioClip _doorUnlockSound;

    //cached component references
    Animator _animator;
    BoxCollider2D _boxCollider2D;

    AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();

        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player>())
        {
            _animator.SetBool("isOpening", true);
        }
    }

    public void AllowEntrance()
    {
        _audioSource.PlayOneShot(_doorUnlockSound);
        Destroy(_boxCollider2D);
    }
}

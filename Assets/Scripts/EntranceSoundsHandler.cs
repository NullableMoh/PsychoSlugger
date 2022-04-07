using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceSoundsHandler : MonoBehaviour
{
    //configuration parameters
    [SerializeField] AudioClip _soundtrack;
    [SerializeField] AudioClip _playerCrash;

    //state triggerSounds
    int _soundTriggerCounter;


    //cached component references
    AudioSource _audioSource;
    BoxCollider2D _boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _boxCollider2D = GetComponent<BoxCollider2D>();

        _soundTriggerCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player>())
        {

            if(_soundTriggerCounter < 1)
            {
                _audioSource.PlayOneShot(_playerCrash);
                
                FindObjectOfType<CinemachineShake>().ShakeCamera(3f, 0.25f);

                yield return new WaitForSeconds(_playerCrash.length);
                _audioSource.loop = true;
                _audioSource.clip = _soundtrack;
                _audioSource.Play();


                _soundTriggerCounter++;
            }

        }

    }
}

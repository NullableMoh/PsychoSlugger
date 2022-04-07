using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSessionHandler : MonoBehaviour
{
    //configuration parameters
    [SerializeField] AudioClip _selectSound;

    //cached component refernces
    AudioSource _audioSource;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        StopAllCoroutines();
        StartCoroutine(StartGameEnumerator());
    }

    private IEnumerator StartGameEnumerator()
    {
        _audioSource.clip = _selectSound;
        _audioSource.Play();

        yield return new WaitForSeconds(_audioSource.clip.length);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void EndGame()
    {
        StopAllCoroutines();
        StartCoroutine(EndGameEnumerator());
    }

    private IEnumerator EndGameEnumerator()
    {
        _audioSource.clip = _selectSound;
        _audioSource.Play();

        yield return new WaitForSeconds(_audioSource.clip.length);

        Application.Quit();

    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollfaceActivator : MonoBehaviour
{
    [SerializeField] GameObject _trollface;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            _trollface.SetActive(true);
        }
    }
}

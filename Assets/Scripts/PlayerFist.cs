using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFist : MonoBehaviour
{

    //cached component references
    PolygonCollider2D _polygonCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInChildren<PolygonCollider2D>().IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            FindObjectOfType<Player>().OnFistWallCollision();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Foreground foreground = collision.gameObject.GetComponent<Foreground>();
        if(foreground)
        {
            GetComponentInChildren<PolygonCollider2D>().isTrigger = true;
        }
    }
}

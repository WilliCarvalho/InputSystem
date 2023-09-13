using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent (typeof(BoxCollider2D))]
public class Collectable : MonoBehaviour
{
    [SerializeField] private Sprite collectableSprite;

    private SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = collectableSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerBehaviour>().acornsCollected++;
            Destroy(this.gameObject);
        }
    }
}

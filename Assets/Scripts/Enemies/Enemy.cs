using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite sprite;
    [SerializeField] private Vector2 rightSidePos;
    [SerializeField] private Vector2 leftSidePos;
    [SerializeField] private float velocity = 10;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }

    private void Start()
    {
        StartCoroutine(MoveEnemy());
    }

    private IEnumerator MoveEnemy()
    {
        while (this.gameObject.active)
        {
            if (transform.position.x < rightSidePos.x)
            {
                for (float i = transform.position.x; transform.position.x < rightSidePos.x; i++)
                {
                    yield return new WaitForSeconds(0.1f);
                    transform.Translate(Vector2.right * velocity * Time.deltaTime);
                }
            }
            else if (transform.position.x > leftSidePos.x)
            {
                for (float i = transform.position.x; transform.position.x < leftSidePos.x; i++)
                {
                    yield return new WaitForSeconds(0.1f);
                    transform.Translate(Vector2.left);
                }
            }

            yield return new WaitForSeconds(3);
        }        
    }
}

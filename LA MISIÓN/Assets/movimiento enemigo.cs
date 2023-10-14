using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 7.0f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Iniciar el movimiento del enemigo
        StartCoroutine(ChangeDirection());
    }

    void Update()
    {
        // Mover al enemigo en la dirección actual
        rb.velocity = moveDirection * moveSpeed;
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            // Elegir una nueva dirección aleatoria
            moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

            // Esperar un tiempo antes de cambiar de dirección nuevamente
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaDesacelerar : MonoBehaviour
{
    public float desaceleracion = 0.5f; // Factor de desaceleración
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            // Aplicar resistencia proporcional a la velocidad
            Vector2 desaceleracionFuerza = -rb.velocity * desaceleracion;
            rb.AddForce(desaceleracionFuerza);

            // Verificar si la velocidad es lo suficientemente baja
            if (rb.velocity.magnitude < 0.1f)
            {
                Debug.Log(rb.gameObject.name);
                rb.velocity = Vector2.zero; // Detener la bomba
                rb.gravityScale = 0f;
            }
        }
    }
}
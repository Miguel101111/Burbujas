using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{ 
    public float explosionForce = 500f;     // Fuerza de la explosión
    public float explosionRadius = 5f;     // Radio de la explosión
    public LayerMask affectedLayers;       // Capas afectadas por la explosión
    public float countdown = 3f;           // Tiempo antes de explotar
    //public GameObject explosionEffect;     // Efecto visual opcional (partículas)

    void Start()
    {
        // Inicia la cuenta regresiva
        //Invoke(nameof(Explode), countdown);
    }

    void Explode()
    {
        // Genera un efecto visual si está configurado
        /**if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        *
        */
        // Detectar objetos dentro del radio de la explosión
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, explosionRadius, affectedLayers);

        foreach (Collider2D obj in objects)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Calcular la dirección de la explosión
                Vector2 direction = rb.transform.position - transform.position;
                direction.Normalize();

                // Aplicar fuerza de explosión
                if (rb.transform.position.y < transform.position.y +0.2)
                {
                    rb.AddForce(direction * explosionForce * 10f);
                }
                else
                {
                    rb.AddForce(direction * explosionForce);
                }
            }
        }

        // Destruye el objeto bomba después de la explosión
       Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja el radio de explosión en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Personaje"))
        {
            Explode();

            Debug.Log("Pop");
        }
        
    }

}


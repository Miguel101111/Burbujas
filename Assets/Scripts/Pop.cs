using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{ 
    public float explosionForce = 500f;     // Fuerza de la explosi�n
    public float explosionForceLateral = 500f;
    public float explosionRadius = 5f;     // Radio de la explosi�n
    public LayerMask affectedLayers;       // Capas afectadas por la explosi�n
    public float countdown = 3f;           // Tiempo antes de explotar
    //public GameObject explosionEffect;     // Efecto visual opcional (part�culas)

    void Start()
    {
        // Inicia la cuenta regresiva
        //Invoke(nameof(Explode), countdown);
    }

    public void Explode()
    {
        // Genera un efecto visual si est� configurado
        /**if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        *
        */
        // Detectar objetos dentro del radio de la explosi�n
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, explosionRadius, affectedLayers);

        foreach (Collider2D obj in objects)
        {
            GetComponent<AudioSource>().Play();
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Calcular la direcci�n de la explosi�n
                Vector2 direction = rb.transform.position - transform.position;
                direction.Normalize();

                // Aplicar fuerza de explosi�n
                if (rb.transform.position.y < transform.position.y +0.5 && rb.transform.position.y > transform.position.y -0.5)
                {
                    rb.AddForce(direction * explosionForceLateral);
                    Debug.Log("Lateral");
                }
                else
                {
   
                    rb.AddForce(direction * explosionForce);
                }
            }
        }

        // Destruye el objeto bomba despu�s de la explosi�n
       Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja el radio de explosi�n en el editor
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


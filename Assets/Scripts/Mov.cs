using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mov : MonoBehaviour
{
    Rigidbody2D rb;
    public bool enSuelo = true;
    public Vector2 Salto;
    public LayerMask Suelo;
    public float LongRayCas = 0.6f;
    public float anchoBoxCast = 1.0f;
    public GameObject burbujaPrefab;  // El prefab de la bomba
    public Transform puntoDisparo; // El punto desde donde se lanzan las bombas
    public float fuerzaLanzamiento = 5f; // Fuerza con la que se lanzará la bomba
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 boxCenter = (Vector2)transform.position + Vector2.down * LongRayCas / 2;

        // Realizar un BoxCast
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCenter,                         // Centro del BoxCast
            new Vector2(anchoBoxCast, 0.1f),   // Tamaño del área (ancho y altura)
            0f,                                // Ángulo de rotación (0 para horizontal)
            Vector2.down,                      // Dirección del BoxCast
            LongRayCas,                        // Longitud del BoxCast
            Suelo                              // Capas detectadas
        );
        if (Input.GetKeyDown(KeyCode.Space)) // Cambia "Space" por la tecla que desees
        {
            LanzarBomba();
        }
        enSuelo = hit.collider != null;
        if (Input.GetKeyDown(KeyCode.W) && enSuelo)
        {
            rb.AddForce(Salto, ForceMode2D.Impulse);
        }
        // Velocidad constante hacia la derecha mientras se mantiene presionada la tecla 'D'
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(5, rb.velocity.y); // Velocidad constante en X
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-5, rb.velocity.y); // Velocidad constante en X
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // Detiene el movimiento en X cuando no se presiona 'D'
        }
    }
    void LanzarBomba()
    {
        // Instanciar la bomba en el punto de disparo
        GameObject bomba = Instantiate(burbujaPrefab, puntoDisparo.position, Quaternion.identity);

        // Agregar fuerza a la bomba
        Rigidbody2D rb = bomba.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Aplica una fuerza hacia adelante (en la dirección del personaje)
            rb.AddForce(transform.right * fuerzaLanzamiento, ForceMode2D.Impulse);
        }

    }
}

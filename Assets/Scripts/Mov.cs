using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mov : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    public bool bStart =false;
    public bool enSuelo = true;
    public Vector2 Salto;
    public LayerMask Suelo;
    public float LongRayCas = 0.51f;
    public float anchoBoxCast = 1.0f;  
    public Transform puntoDisparo; // El punto desde donde se lanzan las bombas
    public float fuerzaLanzamiento = 5f; // Fuerza con la que se lanzará la bomba
    public bool bPuedeContro =true;
    public GameObject bombaPrefab;        // Prefab de la bomba
    public float tiempoIgnorarJugador = 0.5f; // Tiempo para ignorar colisiones con el jugador
    public float desaceleracion = 0.5f;   // Factor para desacelerar la bomba
    public LayerMask capaJugador;         // Capa del jugador para ignorar colisiones
    bool powerup = false;
    GameObject bomba = null;
    public MainMenu retryMenuManager;
    public Victoria victoria;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        if (bStart)
        {
            if (Input.GetMouseButtonDown(0) && powerup && bomba == null) // Botón izquierdo del mouse
            {
                LanzarBomba();
            }

            enSuelo = hit.collider != null;
            if (Input.GetKeyDown(KeyCode.W) && enSuelo && bPuedeContro)
            {
                rb.AddForce(Salto, ForceMode2D.Impulse);
            }
            if (Input.GetKey(KeyCode.D) && !enSuelo && bPuedeContro)
            {
                rb.velocity = new Vector2(3, rb.velocity.y); // Velocidad constante en X
            }
            else if (Input.GetKey(KeyCode.A) && !enSuelo && bPuedeContro)
            {
                rb.velocity = new Vector2(-3, rb.velocity.y); // Velocidad constante en X
            }

            // Velocidad constante hacia la derecha mientras se mantiene presionada la tecla 'D'
            else if (Input.GetKey(KeyCode.D) && enSuelo && bPuedeContro)
            {
                animator.SetBool("Walk", true);
                spriteRenderer.flipX = false;
                rb.velocity = new Vector2(5, rb.velocity.y); // Velocidad constante en X
            }
            else if (Input.GetKey(KeyCode.A) && enSuelo && bPuedeContro)
            {
                animator.SetBool("Walk", true);
                spriteRenderer.flipX = true;
                rb.velocity = new Vector2(-5, rb.velocity.y); // Velocidad constante en X
            }
            else
            {
                animator.SetBool("Walk", false);
                rb.velocity = new Vector2(0, rb.velocity.y); // Detiene el movimiento en X cuando no se presiona 'D'
            }
        }
    }
    void LanzarBomba()
    {
        // Obtener la posición del mouse en el mundo
        Vector3 posicionMouse = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        posicionMouse.z = 0; // Asegurarse de que la coordenada Z sea 0 (2D)

        // Calcular la dirección hacia el mouse
        Vector2 direccion = (posicionMouse - puntoDisparo.position).normalized;

        // Instanciar la bomba
        bomba = Instantiate(bombaPrefab, puntoDisparo.position, Quaternion.identity);

        // Aplicar fuerza inicial
        Rigidbody2D rb = bomba.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(direccion * fuerzaLanzamiento, ForceMode2D.Impulse);

            // Ignorar colisiones con el jugador temporalmente
            Physics2D.IgnoreLayerCollision(bomba.layer, capaJugador, true);
            StartCoroutine(HabilitarColisiones(rb, bomba));
        }

        // Configurar desaceleración
        BombaDesacelerar desaceleracionScript = bomba.AddComponent<BombaDesacelerar>();
        desaceleracionScript.desaceleracion = desaceleracion;
    }

    private IEnumerator HabilitarColisiones(Rigidbody2D rb, GameObject bomba)
    {
        yield return new WaitForSeconds(tiempoIgnorarJugador);

        // Dejar de ignorar colisiones con el jugador
        Physics2D.IgnoreLayerCollision(rb.gameObject.layer, capaJugador, false);
        bomba.GetComponent<Collider2D>().excludeLayers = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Burbuja")
        {
            bPuedeContro = false;
            StartCoroutine(wait());
            Debug.Log("Pierda de control");
        }
        if (collision.gameObject.tag == "powerup")
        {
            Debug.Log("aaaaaaaaaa");
            powerup = true;
        }
        if (collision.gameObject.tag=="Victoria")
        {
            bStart=false;
            victoria.ShowRetryMenu();
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        bPuedeContro = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Muerte" )
        {
            Debug.Log("muerto");
            DestroyPlayer();


        }
   
    }
    void DestroyPlayer()
    {
        Destroy(this.gameObject);
        retryMenuManager.ShowRetryMenu();
    }
}

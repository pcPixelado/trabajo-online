using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Armas armaEquipada;

    public GameObject cadaver;

    private GameObject player;
    public Transform firePoint;
    public float Firetime = 0;
    public float RangoDeVision;

    private bool JugadorEnElCampoDeVisión, recargando, cagao = false;

    private Vector3 posicionesEstrategicas;

    public Vector3 posicionJugadorMemoria;

    private Vector3 destination;
    private NavMeshAgent agent;

    public float vida = 10;
    private float vidaRestante;
    public void Awake()
    {
        vidaRestante = vida;

        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        destination = transform.position;
    }

    void Update()
    {
        if (vidaRestante < vida / 2 && !cagao)
        {
            BuscaCobertura();
            if (!JugadorEnElCampoDeVisión)
            {
                cagao = true;
            }
        }
        else
        {
            if (vidaRestante < vida / 3)
            {
                posicionJugadorMemoria = player.transform.position;
            }

            if (posicionJugadorMemoria != Vector3.zero)
            {
                if (recargando)
                {
                    BuscaCobertura();
                }
                else
                {
                    OfensivaAlJugador();
                }
            }
        }

        if (JugadorEnElCampoDeVisión)
        {
            posicionJugadorMemoria = player.transform.position;
        }

        RangoDeVision = armaEquipada.mirillaDeApuntado * 35f;

        Vector3 vectorDestination = destination - transform.position + new Vector3(0.5f, 0.5f);
        float angle = Mathf.Atan2(vectorDestination.y, vectorDestination.x) * Mathf.Rad2Deg;

        if (JugadorEnElCampoDeVisión)
        {
            if (Firetime > armaEquipada.CadenciaDeTiro)
            {
                Shoot();
            }
            else Firetime += Time.deltaTime;

            //print(angle);
            agent.SetDestination(new Vector3(destination.x, destination.y, transform.position.z));
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else 
        {
            agent.SetDestination(new Vector3(destination.x, destination.y, transform.position.z));
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        Vector3 playerDirection = player.transform.position - transform.position;
        float rayCastAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

        LanzarRaycast(rayCastAngle, 0.9f);
    }

    private int municionEnElCartucho = 10;

    void Shoot()
    {
        if (municionEnElCartucho > 0)
        {
            if (Random.Range(0,10) > 1)
            {
                recargando = false;
                municionEnElCartucho--;
                Firetime = 0;
                for (int i = 0; i < armaEquipada.NumeroDeBalasPorDisparo; i++)
                {
                    GameObject bullet = Instantiate(armaEquipada.TipoDeMunicíon, firePoint.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + Random.Range(-armaEquipada.Dispersión * 2.2f, armaEquipada.Dispersión * 2.2f)));
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.velocity = bullet.transform.right * armaEquipada.VelocidadDeLasBalas * 0.8f;

                    // Destruye la bala después de x segundos.
                    Destroy(bullet, armaEquipada.AlcanceSegundos);
                }
            }
            else Firetime = armaEquipada.CadenciaDeTiro / 2;
        }
        else
        {
            Firetime = -armaEquipada.TiempoDeRecarga;
            if (vidaRestante < vida / 3) municionEnElCartucho = 10;
            else municionEnElCartucho = 15;
            recargando = true;
            posicionesEstrategicas = new Vector3(Random.Range(-85, 85), Random.Range(-85, 85));
        }

    }

    public void LanzarRaycast(float angulo, float distancia)
    {
        Vector3 direction = new Vector2(Mathf.Cos(angulo * Mathf.Deg2Rad), Mathf.Sin(angulo * Mathf.Deg2Rad));

        RaycastHit2D hit = Physics2D.Raycast(transform.position + direction * 5, direction, RangoDeVision * distancia);

        if (hit)
        {
            if (hit.transform.CompareTag("Player")/*|| hit == player*/)
            {
                Debug.DrawRay(transform.position, distancia * RangoDeVision * direction, Color.green);
                JugadorEnElCampoDeVisión = true;
            }
            else if (hit.transform.CompareTag("Bala"))
            {
                Debug.DrawRay(transform.position, distancia * RangoDeVision * direction, Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, distancia * RangoDeVision * direction, Color.red);
                JugadorEnElCampoDeVisión = false;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, distancia * RangoDeVision * direction, Color.red);
            JugadorEnElCampoDeVisión = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            vidaRestante -= collision.transform.localScale.z * collision.relativeVelocity.magnitude / 290;

            Destroy(collision.gameObject);
        }

        if (vidaRestante < 0)
        {
            Instantiate(cadaver, transform.position, Quaternion.Euler(0,0,Random.Range(0,0)));
            Destroy(gameObject);
        }
    }

    public void BuscaCobertura()
    {
        if (JugadorEnElCampoDeVisión)
        {
            destination = (transform.position - posicionJugadorMemoria) / 3 + transform.position + posicionesEstrategicas;
        }
        else
        {
            destination = transform.position;
        }

        if(vidaRestante < vida/3)agent.speed = 11;
        else agent.speed = 20;
    }
    public void OfensivaAlJugador()
    {
        if (!JugadorEnElCampoDeVisión)
        {
            if (vidaRestante < vida / 3) agent.speed = 8;
            else agent.speed = 10;
            destination = posicionJugadorMemoria;
            print("ofensiva");
        }
        else
        {
            destination = posicionJugadorMemoria;
            agent.speed = 2;
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Armas armaEquipada;

    private GameObject player;
    public Transform firePoint;
    public float Firetime = 0;
    public float RangoDeVision;

    private bool JugadorEnElCampoDeVisión;

    public Vector3 posicionesEstrategicas;

    public bool SeguirAlJugador;

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


        if (JugadorEnElCampoDeVisión)
        {
            destination = player.transform.position;
            
        }



        Vector3 vectorDestination = destination - transform.position;
        float angle = Mathf.Atan2(vectorDestination.y, vectorDestination.x) * Mathf.Rad2Deg;

        if (JugadorEnElCampoDeVisión)
        {
            if (Firetime > armaEquipada.CadenciaDeTiro)
            {
                Shoot();
                Firetime = 0;
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

        LanzarRaycast(rayCastAngle);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(armaEquipada.TipoDeMunicíon, firePoint.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * armaEquipada.VelocidadDeLasBalas;

        // Destruye la bala después de 2 segundos.
        Destroy(bullet, armaEquipada.AlcanceSegundos);
    }

    void LanzarRaycast(float angulo)
    {
        Vector3 direction = new Vector2(Mathf.Cos(angulo * Mathf.Deg2Rad), Mathf.Sin(angulo * Mathf.Deg2Rad));

        RaycastHit2D hit = Physics2D.Raycast(transform.position + direction * 5, direction, RangoDeVision);

        if (hit)
        {
            if (hit.transform.tag == "Player" /*|| hit == player*/)
            {
                Debug.DrawRay(transform.position, direction * RangoDeVision, Color.green);
                JugadorEnElCampoDeVisión = true;
            }
            else if (hit.transform.tag == "Bala")
            {
                Debug.DrawRay(transform.position, direction * RangoDeVision, Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, direction * RangoDeVision, Color.red);
                JugadorEnElCampoDeVisión = false;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, direction * RangoDeVision, Color.red);
            JugadorEnElCampoDeVisión = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bala")
        {
            vidaRestante -= collision.transform.localScale.z;

            Destroy(collision.gameObject);
        }

        if (vidaRestante < 0)
        {
            Destroy(gameObject);
        }
    }
}
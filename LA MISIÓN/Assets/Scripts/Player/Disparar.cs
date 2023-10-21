using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform firePoint;
    public Armas armaEquipada;
    public float timer;
    private GameObject lastHitEnemy;

    void Update()
    {
<<<<<<< HEAD
        if (Input.GetMouseButtonDown(0) && timer > armaEquipada.CadenciaDeTiro)
=======
        if (armaEquipada.Automatica)
        {
            if (Input.GetKey(KeyCode.Mouse0) && timer > armaEquipada.CadenciaDeTiro)
            {
                Shoot();
            }
            else if (timer <= armaEquipada.CadenciaDeTiro)
            {
                timer = timer + Time.deltaTime;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && timer > armaEquipada.CadenciaDeTiro)
>>>>>>> f2ae74881a63f8f91bd04d17ee807fa14ed2b898
        {
            Shoot();
        }
        else if (timer <= armaEquipada.CadenciaDeTiro)
        {
            timer += Time.deltaTime;
        }


        Vector3 vectormouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(vectormouse.y, vectormouse.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {
        timer = 0;

        for (int i = 0; i < armaEquipada.NumeroDeBalasPorDisparo; i++)
        {
<<<<<<< HEAD
            GameObject bullet = Instantiate(armaEquipada.TipoDeMunicíon, firePoint.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + Random.Range(-armaEquipada.Dispersión, armaEquipada.Dispersión)));
=======
            GameObject bullet = Instantiate(armaEquipada.TipoDeMunicíon, firePoint.position, Quaternion.Euler(0,0,transform.rotation.z + Random.Range(-armaEquipada.Dispersión * 2, armaEquipada.Dispersión * 2)));
>>>>>>> f2ae74881a63f8f91bd04d17ee807fa14ed2b898
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = firePoint.up * armaEquipada.VelocidadDeLasBalas;

            // Destruye la bala después de 1 segundo.
            Destroy(bullet, armaEquipada.AlcanceSegundos);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            // Comprobar si es el mismo enemigo que fue golpeado anteriormente
            if (collision.gameObject == lastHitEnemy)
            {
                Destroy(collision.gameObject); // Destruir al enemigo si ha sido golpeado dos veces
            }
            else
            {
                lastHitEnemy = collision.gameObject;
            }
        }
    }
}

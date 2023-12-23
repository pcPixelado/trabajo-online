using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Arma", fileName = "Arma")]
public class Armas : ScriptableObject
{
    public float CadenciaDeTiro = 1f, VelocidadDeLasBalas = 200f, NumeroDeBalasPorDisparo = 1, Dispersión = 0f, AlcanceSegundos = 1f, mirillaDeApuntado = 2f, TiempoDeRecarga = 1f;

    public GameObject TipoDeMunicíon;

    public bool Automatica;
}

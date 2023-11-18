using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSeleccionado : MonoBehaviour
{
    public GameObject objetoSeleccionado;
    public GameObject objetoEnElSuelo;
    void Update()
    {
        
    }

    public void SoltarItemSeleccionado()
    {
        ItemInfo info = objetoSeleccionado.GetComponent<ItemInventory>().info;
        Destroy(objetoSeleccionado);
        GameObject newitem = Instantiate(objetoEnElSuelo, transform.parent.parent.parent.GetChild(1).position, Quaternion.identity);
        newitem.GetComponent<ItemGround>().info = info;
    }
    public void UsarEquiparItemSeleccionado()
    {

    }
}

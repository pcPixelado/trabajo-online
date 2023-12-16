using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour
{
    public RectTransform puntero, rectTransform, parent; 

    public ItemSeleccionado IS;

    public bool Soltar, UsarEquipar;
    void Update()
    {
        Rect prueba = new(parent.anchoredPosition.x - parent.rect.width / 2, parent.anchoredPosition.y - parent.rect.height, parent.rect.width, parent.rect.height);


        Vector2 mousePos2 = (puntero.position / Camera.main.scaledPixelWidth * 1600) - new Vector3(prueba.x,prueba.y);
        Rect itemDelInventarioLocalRect = new(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y, rectTransform.rect.width, rectTransform.rect.height);
       

        if (itemDelInventarioLocalRect.Contains(mousePos2))
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Soltar)
                {
                    IS.SoltarItemSeleccionado();
                }
                else if (UsarEquipar)
                {
                    IS.UsarEquiparItemSeleccionado();
                }
            }
        }
    }
}

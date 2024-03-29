using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmasInventory : MonoBehaviour
{
    public RectTransform Puntero;

    public Image image;
    public ItemInfo info;
    private RectTransform rectTransform, spriteRectTransform;
    public GameObject clickDerecho;
    public InventoryManager inventoryManager;

    [HideInInspector]public GameObject[] slotsDeArmas;

    public int Mejoras;

    public bool CartuchoEquipado, ArmaEquipada = false;
    public int MunicionEnElCartucho;

    public ItemInfo cartucho;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        clickDerecho = GameObject.FindGameObjectWithTag("ClickDerecho");

        spriteRectTransform = transform.GetChild(0).GetComponent<RectTransform>();
    }

    public void CambioDeCartucho()
    {
        SacarCartucho();
        MeterCartucho();
    }
    public void CambioDeCartucho(GameObject cartuchoAcambiar)
    {
        SacarCartucho();
        MeterCartucho(cartuchoAcambiar);
    }

    public void SacarCartucho()
    {
        if (CartuchoEquipado)
        {
            CartuchoEquipado = false;
            inventoryManager.NewItemOnInventory(cartucho, MunicionEnElCartucho);
            MunicionEnElCartucho = 0;
        }
    }

    public void MeterCartucho()
    {
        for (int i = 0; i < inventoryManager.itemsDentroDelinventario.Length; i++)
        {
            if (inventoryManager.itemsDentroDelinventario[i].GetComponent<ItemInventory>() != null)
            {
                if (inventoryManager.itemsDentroDelinventario[i].GetComponent<ItemInventory>().ItemID == info.CalibreDelArma)
                {
                    MeterCartucho(inventoryManager.itemsDentroDelinventario[i]);
                    break;
                }
            }
        }
    }
    public void MeterCartucho(GameObject cartuchoParaAdentro)
    {
        inventoryManager.playerController.Audio[2].Play();

        MunicionEnElCartucho = cartuchoParaAdentro.GetComponent<ItemInventory>().Munici�n;
        cartucho = cartuchoParaAdentro.GetComponent<ItemInventory>().info;
        Destroy(cartuchoParaAdentro);
        CartuchoEquipado = true;
    }
    public bool AgarrandoItem;
    private Vector2 PosicionInicial, distanciaAlCentro, NuevaPosiblePosicion;
    void Update()
    {
        if (ArmaEquipada)
        {
            if ((Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("uBtn")) && !CartuchoEquipado)
            {
                MeterCartucho();
            }
            else if ((Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("uBtn")) && CartuchoEquipado)
            {
                CambioDeCartucho();
            }
        }

        int estado;

        if (CartuchoEquipado) estado = Mejoras + info.sprite.Length / 2;
        else estado = Mejoras;


        image.sprite = info.sprite[estado];

        rectTransform.sizeDelta = new Vector2(info.SlotsX * 120, info.SlotsY * 120);

        spriteRectTransform.sizeDelta = new Vector2(info.sprite[estado].rect.width, info.sprite[estado].rect.height);
        spriteRectTransform.localScale = new Vector3(info.inventoryScale, info.inventoryScale);



        Rect uiObjectLocalRect = new(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - rectTransform.rect.height + 780, rectTransform.rect.width, rectTransform.rect.height);

        Vector2 mousePos = (Puntero.position / Camera.main.scaledPixelWidth * 1600) - new Vector3(460, 60);

        if (uiObjectLocalRect.Contains(mousePos) && image.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetButtonDown("oBtn"))
            {
                clickDerecho.transform.position = Puntero.position;

                clickDerecho.GetComponent<ItemSeleccionado>().objetoSeleccionado = gameObject;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetButtonDown("xBtn"))
            {
                AgarrandoItem = true;
                PosicionInicial = rectTransform.anchoredPosition;
                distanciaAlCentro = rectTransform.anchoredPosition - mousePos;
            }
        }

        if (AgarrandoItem && (Input.GetKey(KeyCode.Mouse0) || Input.GetButton("xBtn")))
        {
            inventoryManager.RecargarSlotsOcupados();
            rectTransform.anchoredPosition = mousePos + distanciaAlCentro;

            float DistanciaMasCercana = 1000;
            Vector2 coordsMasCercanas = Vector2.zero;
            for (int i = 0; i < inventoryManager.slots.GetLength(0); i++)
            {
                for (int j = 0; j < inventoryManager.slots.GetLength(1); j++)
                {
                    if(Vector3.Distance(inventoryManager.slots[i,j].transform.position,transform.position) < DistanciaMasCercana)
                    {
                        DistanciaMasCercana = Vector3.Distance(inventoryManager.slots[i, j].transform.position, transform.position);
                        coordsMasCercanas = new Vector2(i, j);
                    } 
                }
            }
            if (inventoryManager.ConfirmarUnaPosicion(Mathf.RoundToInt(coordsMasCercanas.x), Mathf.RoundToInt(coordsMasCercanas.y), info.SlotsX, info.SlotsY))
            {
                NuevaPosiblePosicion = inventoryManager.slots[Mathf.RoundToInt(coordsMasCercanas.x), Mathf.RoundToInt(coordsMasCercanas.y)].GetComponent<RectTransform>().anchoredPosition + new Vector2(-1,1);
            }
            else NuevaPosiblePosicion = PosicionInicial;
        }
        else if (AgarrandoItem && (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetButtonUp("xBtn")))
        {
            if (slotsDeArmas.Length < 1)
            {
                slotsDeArmas = inventoryManager.SlotsDeArmas;
            }
            //Soltar un item dentro de otro
            for (int i = 0; i < slotsDeArmas.Length; i++)
            {
                Vector2 mousePos2 = (Puntero.position / Camera.main.scaledPixelWidth * 1600) - new Vector3(31, 60);

                RectTransform SlotDeArmasRT = slotsDeArmas[i].GetComponent<RectTransform>();
                Rect SlotDeArmasLocalRect = new(SlotDeArmasRT.anchoredPosition.x, SlotDeArmasRT.anchoredPosition.y, SlotDeArmasRT.rect.width, SlotDeArmasRT.rect.height);

                //print(SlotDeArmasLocalRect + " " + mousePos2);

                if (SlotDeArmasLocalRect.Contains(mousePos2))
                {
                    if (SlotDeArmasRT.GetComponent<SlotDeArma>() != null)
                    {
                        inventoryManager.EquiparArma(gameObject, SlotDeArmasRT.GetComponent<SlotDeArma>().SlotNumber);
                    }
                }
            }
            //sacar la respuesta

            AgarrandoItem = false;

            rectTransform.anchoredPosition = NuevaPosiblePosicion;
        }
    }
}
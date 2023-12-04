using UnityEngine;
using UnityEngine.UI;

public class MenuControllerDesplegable : MonoBehaviour
{
    public GameObject charla1Panel;
    public GameObject charla2Panel;
    public float offset = 200f; // Ajusta el valor según sea necesario
    private RectTransform[] rectTransforms;
    private Vector2[] posicionesOriginales;

    void Start()
    {
        // Al inicio, almacenar las posiciones originales y actuales de los botones
        rectTransforms = new RectTransform[transform.childCount];
        posicionesOriginales = new Vector2[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            rectTransforms[i] = transform.GetChild(i).GetComponent<RectTransform>();
            posicionesOriginales[i] = rectTransforms[i].anchoredPosition;
        }
    }

    void Update()
    {
        // Ejemplo: Desplazar los botones cuando se presiona la tecla "D"
        if (Input.GetKeyDown(KeyCode.D))
        {
            DesplazarBotones(charla1Panel); // Puedes pasar el panel correspondiente como argumento
        }

        // Ejemplo: Restaurar posiciones originales cuando se presiona la tecla "R"
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestaurarPosicionesOriginales();
        }
    }

    void DesplazarBotones(GameObject panel)
    {
        for (int i = 0; i < rectTransforms.Length; i++)
        {
            // Desplazar cada botón hacia abajo según el offset
            rectTransforms[i].anchoredPosition += new Vector2(0f, -offset);

            // Desactivar el panel correspondiente
            if (panel != null)
            {
                panel.SetActive(false);
            }
        }
    }

    void RestaurarPosicionesOriginales()
    {
        for (int i = 0; i < rectTransforms.Length; i++)
        {
            rectTransforms[i].anchoredPosition = posicionesOriginales[i];
        }
    }
}

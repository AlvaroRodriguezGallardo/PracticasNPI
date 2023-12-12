using UnityEngine;

public class DropdownGroup : MonoBehaviour
{
    [SerializeField] private RectTransform contenidoPanel;

    private bool desplazado;

    public bool EstaDesplazado => desplazado;

    public RectTransform ContenidoPanel => contenidoPanel;

    public void Desplazar(bool estado)
    {
        desplazado = estado;
        contenidoPanel.anchoredPosition = new Vector2(contenidoPanel.anchoredPosition.x, estado ? -100f : 0f);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class TableManager : MonoBehaviour
{
    public GameObject cellPrefab;  // Prefab de la celda (deberías crear un prefab de una celda en Unity).
    public string[,] cellTexts;    // Matriz para almacenar los textos en cada celda.

    void Start()
    {
        CreateTable();
        FillTable();
    }

    void CreateTable()
    {
        // Inicializar la matriz de textos
        cellTexts = new string[4, 4];

        // Crear las celdas y organizarlas en forma de tabla
        for (int row = 0; row < 4; row++)
        {
            int numCols = (row == 0) ? 1 : 4;

            for (int col = 0; col < numCols; col++)
            {
                // Instanciar la celda desde el prefab
                GameObject cell = Instantiate(cellPrefab, transform);

                // Posicionar la celda en la escena
                float xPos = col * 150;  // Ajusta según tus necesidades
                float yPos = -row * 30;  // Ajusta según tus necesidades
                cell.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
            }
        }
    }

    void FillTable()
    {
        // Asignar texto a cada celda (puedes modificar esto según tus necesidades)
        cellTexts[0, 0] = "Martes";
        cellTexts[1, 0] = "Primero";
        cellTexts[1, 1] = "Segundo";
        cellTexts[1, 2] = "Postre";
        cellTexts[2, 0] = "Texto Celda 2,0";
        cellTexts[2, 1] = "Texto Celda 2,1";
        cellTexts[2, 2] = "Texto Celda 2,2";
        cellTexts[2, 3] = "Texto Celda 2,3";
        cellTexts[3, 0] = "Texto Celda 3,0";
        cellTexts[3, 1] = "Texto Celda 3,1";
        cellTexts[3, 2] = "Texto Celda 3,2";
        cellTexts[3, 3] = "Texto Celda 3,3";

        // Obtener todas las celdas en el objeto Canvas
        Text[] allCells = GetComponentsInChildren<Text>();

        // Asignar los textos de la matriz a las celdas
        for (int i = 0; i < allCells.Length; i++)
        {
            int row = i / 4;
            int col = i % 4;
            allCells[i].text = cellTexts[row, col];
        }
    }
}

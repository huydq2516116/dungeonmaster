using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class FloorManager : MonoBehaviour
{
    /* Tạo các cell đầu tiên ngay khi floor được khởi tạo
     */
    public GameObject cellPrefabs;
    public GridLayoutGroup glg;
    public Sprite unlockableLockSprite;
    public List<GameObject> cellObjects;


    private void Start()
    {
        cellObjects = new();
        for (int i = 1; i <= glg.constraintCount; i++)
        {
            for (int j = 1; j <= glg.constraintCount; j++)
            {
                string cellName = $"{i}{j}";
                GameObject cell = Instantiate(cellPrefabs,transform);
                cell.name = cellName;
                if (i == 1 && j == 1)
                {
                    CellController cellController = cell.GetComponent<CellController>();
                    cellController.cellState = CellController.CellState.Unlockable;
                    Image buttonImage = cell.transform.GetChild(0).GetComponent<Image>();
                    buttonImage.sprite = unlockableLockSprite;
                }
                cellObjects.Add(cell);
            }
        }
    }
    public void ExpandThisFloor()
    {
        glg.constraintCount += 1;
        int i = glg.constraintCount;
        for (int j = 1; j < glg.constraintCount; j++)
        {
            string cellName = $"{i}{j}";
            GameObject cell = Instantiate(cellPrefabs, transform);
            cell.name = cellName;
            cellObjects.Add(cell);
        }
        for (int j = 1; j <= glg.constraintCount; j++)
        {
            string cellName = $"{j}{i}";
            GameObject cell = Instantiate(cellPrefabs, transform);
            cell.name = cellName;
            cellObjects.Add(cell);
        }
        SortChild();
    }
    public void SortChild()
    {
        var sorted = cellObjects.OrderBy(x => x.name).ToList();
        for (int i = 0; i < sorted.Count; i++)
        {
            sorted[i].transform.SetSiblingIndex(i);
        }
    }
    public void CheckUnlockableAllCell()
    {
        int rowNum = glg.constraintCount;
        for (int i = 0; i < rowNum*rowNum; i++)
        {
            int currentRow = Mathf.FloorToInt(i/rowNum) + 1;
            int currentMinRow = rowNum * (currentRow - 1);
            int currentMaxRow = rowNum * (currentRow + 1) - 1;
            int c1 = Mathf.CeilToInt(Mathf.Clamp(i - 1, currentMinRow, currentMaxRow));
            int c2 = Mathf.FloorToInt(Mathf.Clamp(i + 1, currentMinRow, currentMaxRow));

            int currentColumn = (i % rowNum) + 1;
            int currentMinColumn =currentColumn - 1;
            int currentMaxColumn = rowNum * (rowNum - 1) + (currentColumn - 1);

            GameObject cell = cellObjects[i];
            CellController cc = cell.GetComponent<CellController>();
            if (cc.cellState == CellController.CellState.Locked)
            {
                
            }
        }
    }
}

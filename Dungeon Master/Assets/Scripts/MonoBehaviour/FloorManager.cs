using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class FloorManager : MonoBehaviour
{
    /* Tạo các cell đầu tiên ngay khi floor được khởi tạo (Nếu cell(1,1) chuyển thành Unlockable, không thì giữ cell)
     * Chứa hàm Expand This Floor, mỗi lần expand thì sortchild -> Giữ nguyên vị trí cell
     * Chứa hàm kiểm tra cell Unlockable 
     */
    public GameObject cellPrefabs;
    public Sprite unlockableLockSprite;
    public List<GameObject> cellObjects;

    private GridLayoutGroup glg;
    private void Awake()
    {
        glg = GetComponent<GridLayoutGroup>();
    }
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
                    cellController.CellBecomeUnlockable();
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
            int currentMaxRow = rowNum * currentRow - 1;
            int c1 = Mathf.Clamp(i - 1, currentMinRow, currentMaxRow);
            int c2 = Mathf.Clamp(i + 1, currentMinRow, currentMaxRow);

            int currentColumn = (i % rowNum) + 1;
            int currentMinColumn =currentColumn - 1;
            int currentMaxColumn = rowNum * (rowNum - 1) + (currentColumn - 1);
            int c3 = Mathf.Clamp(i - rowNum, currentMinColumn, currentMaxColumn);
            int c4 = Mathf.Clamp(i+rowNum, currentMinColumn, currentMaxColumn);

            GameObject cell = cellObjects[i];
            CellController cell1 = cellObjects[c1].GetComponent<CellController>();
            CellController cell2 = cellObjects[c2].GetComponent<CellController>();
            CellController cell3 = cellObjects[c3].GetComponent<CellController>();
            CellController cell4 = cellObjects[c4].GetComponent<CellController>();
            
            CellController cc = cell.GetComponent<CellController>();
            if (cc.cellState == CellController.CellState.Locked)
            {
                if (!cell1.IsLocked() || !cell2.IsLocked() || !cell3.IsLocked() || !cell4.IsLocked())
                {
                    cc.cellState = CellController.CellState.Unlockable;
                    cc.CellBecomeUnlockable();
                }
            }
        }
    }
}

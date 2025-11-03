using System;
using UnityEngine;
using UnityEngine.UI;


public class CellController : MonoBehaviour
{
    /* Chứa trạng thái của cell
     */
    public CellState cellState;
    public void Unlock()
    {
        if (cellState == CellState.Unlockable)
        {
            cellState = CellState.NotUsed;
        }
    }
    public void CellBecomeUnlockable()
    {
        cellState = CellState.Unlockable;
    }


    public enum CellState {Locked, Unlockable, NotUsed, Wall, Normal, Boss}
    public bool IsLocked()
    {
        return cellState == CellState.Locked || cellState == CellState.Unlockable;
    }

}

using System;
using UnityEngine;
using UnityEngine.UI;


public class CellController : MonoBehaviour
{
    /* Chứa state của cell
     * Chứa hàm mở khóa cell (Unlockable -> NotUsed)
     */
    public CellState cellState;
    public Sprite unlockableLockSprite;
    public Image buttonImage;
    [SerializeField] FloorManager floorManager;

    private void Start()
    {
        floorManager = transform.parent.gameObject.GetComponent<FloorManager>();
    }
    public void Unlock()
    {
        if (cellState == CellState.Unlockable)
        {
            cellState = CellState.NotUsed;
            transform.GetChild(0).gameObject.SetActive(false);
        }
        floorManager.CheckUnlockableAllCell();
    }
    public void CellBecomeUnlockable()
    {
        cellState = CellState.Unlockable;
        buttonImage.sprite = unlockableLockSprite;
    }


    public enum CellState {Locked, Unlockable, NotUsed, Wall, Normal, Boss}
    public bool IsLocked()
    {
        return cellState == CellState.Locked || cellState == CellState.Unlockable;
    }

}

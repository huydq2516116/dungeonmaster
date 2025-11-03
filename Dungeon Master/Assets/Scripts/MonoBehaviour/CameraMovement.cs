using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    /* Chứa hàm di chuyển camera tới int floor
     * Chứa hàm di chuyển camera tới floor tiếp theo/floor trước đó
     */
    public Camera cam;
    public int floor;
    public GameObject cellPrefabs;
    public TextMeshProUGUI floorText;
    public void MoveCameraToFloor()
    {
        StartCoroutine(MoveCameraToFloorDelayed());
    }
    public IEnumerator MoveCameraToFloorDelayed()
    {
        yield return null;
        int index = floor - 1;
        RectTransform floorRect = transform.GetChild(index).GetComponent<RectTransform>();
        cam.transform.position = new Vector3(floorRect.position.x, floorRect.position.y, cam.transform.position.z);

        GridLayoutGroup floorGridLayoutGroup = transform.GetChild(index).GetComponent<GridLayoutGroup>();
        int constraintCount = floorGridLayoutGroup.constraintCount;
        float cellScale = cellPrefabs.transform.localScale.x;
        cam.orthographicSize = cellScale * constraintCount / 1.5f;

        floorText.text = $"Floor {floor}";
    }
    public void MoveToNextFloor()
    {
        floor += 1;
        MoveCameraToFloor();
    }
    public void MoveToLastFloor()
    {
        floor -= 1;
        MoveCameraToFloor();
    }
}

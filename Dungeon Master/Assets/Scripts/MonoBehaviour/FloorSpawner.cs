using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;


public class FloorSpawner : MonoBehaviour
{
    /* Chứa dòng code tạo floor tiếp theo và giữ chỉ số maxFloor
     * Chứa code expand floor dựa trên expandThisFloor từ FloorManager.cs
     * Có khả năng thay đổi int floor từ CameraMovement.cs
     */
    public int maxFloor;
    public GameObject floorPrefabs;
    
    [SerializeField] List<GameObject> floorObjects;

    private CameraMovement cameraMovement;

    private void Awake()
    {
        cameraMovement = GetComponent<CameraMovement>();
        maxFloor = 0; 
    }
    private void Start()
    {
        floorObjects = new();
        CreateNextFloor();
    }
    public void CreateNextFloor()
    {
        maxFloor += 1;
        string floorName = $"Floor {maxFloor}";
        GameObject floorGameObject = Instantiate(floorPrefabs, transform);
        floorGameObject.name = floorName;
        floorObjects.Add(floorGameObject);
        cameraMovement.floor = maxFloor;
        cameraMovement.MoveCameraToFloor();
    }
    public void ExpandFLoor()
    {
        int currentFloor = cameraMovement.floor;
        int index = currentFloor - 1;
        FloorManager floorManager = floorObjects[index].GetComponent<FloorManager>();
        floorManager.ExpandThisFloor();
        cameraMovement.MoveCameraToFloor();
    }
    

}

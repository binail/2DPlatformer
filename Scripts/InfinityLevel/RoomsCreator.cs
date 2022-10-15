using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomsCreator : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private RoomLocation[] roomPrefabs;
    [SerializeField] private RoomLocation startRoom;
    [SerializeField] private RoomLocation checkpointRoom;

    [Header("Door Settings")]
    [SerializeField] private CameraController cam;

    private List<RoomLocation> createdRooms = new List<RoomLocation>();
    private int counterOfRooms = 1;
    private int numberOfCheckpointRoom = 5;

    void Start()
    {
        createdRooms.Add(startRoom);
        CreateRoom();
    }

    void Update()
    {
        if (counterOfRooms == numberOfCheckpointRoom-1)
        {
            CreateCheckpointRoom();
        }
        else
        {
            if (player.position.x > createdRooms[createdRooms.Count - 2].end.position.x)
            {
                CreateRoom();
            }
        }
        
    }

    private void CreateRoom()
    {
        RoomLocation newRoom = Instantiate(GetRandomRoom());
        newRoom.transform.position = createdRooms[createdRooms.Count - 1].end.position - newRoom.begin.localPosition;
        createdRooms.Add(newRoom);

        GameObject Child = createdRooms[createdRooms.Count - 2].transform.Find("Door").gameObject;
        Child.GetComponent<DoorForInfinity>().cam = cam;
        Child.GetComponent<DoorForInfinity>().previousRoom = createdRooms[createdRooms.Count - 2].transform;
        Child.GetComponent<DoorForInfinity>().nextRoom = createdRooms[createdRooms.Count - 1].transform;

        counterOfRooms++;
    }

    private void CreateCheckpointRoom()
    {
        numberOfCheckpointRoom *= 2;

        RoomLocation newRoom = Instantiate(checkpointRoom);
        newRoom.transform.position = createdRooms[createdRooms.Count - 1].end.position - newRoom.begin.localPosition;
        createdRooms.Add(newRoom);

        GameObject Child = createdRooms[createdRooms.Count - 2].transform.Find("Door").gameObject;
        Child.GetComponent<DoorForInfinity>().cam = cam;
        Child.GetComponent<DoorForInfinity>().previousRoom = createdRooms[createdRooms.Count - 2].transform;
        Child.GetComponent<DoorForInfinity>().nextRoom = createdRooms[createdRooms.Count - 1].transform;

        counterOfRooms++;
    }

    private RoomLocation GetRandomRoom()
    {
        List<float> chances = new List<float>();
        for (int i = 0; i < roomPrefabs.Length; i++)
        {
            chances.Add(roomPrefabs[i].chanceFromRooms.Evaluate(counterOfRooms));
        }

        float value = Random.Range(0, chances.Sum());
        float sumOfChance = 0;

        for (int i = 0; i < chances.Count; i++)
        {
            sumOfChance += chances[i];

            if (value < sumOfChance)
            {
                return roomPrefabs[i];
            }
        }

        return roomPrefabs[roomPrefabs.Length - 1];
    }
}

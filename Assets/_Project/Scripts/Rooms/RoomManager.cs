using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private QuestionManager questionManager;
    [SerializeField] private Transform player;

    [Header("Room Prefabs")]
    [SerializeField] private RoomController firstRoomPrefab;
    [SerializeField] private RoomController normalRoomPrefab;

    [Header("Generation")]
    [SerializeField] private float roomLength = 20f;
    [SerializeField] private int initialNormalRooms = 3;

    private readonly List<RoomController> activeRooms =
        new List<RoomController>();

    private float nextRoomZ;

    private void Start()
    {
        SpawnFirstRoom();

        for (int i = 0; i < initialNormalRooms; i++)
        {
            SpawnNormalRoom();
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        if (activeRooms.Count == 0)
            return;

        float nextSpawnThreshold =
            nextRoomZ - roomLength * 1.5f;

        if (player.position.z >= nextSpawnThreshold)
        {
            SpawnNormalRoom();

            RemoveOldestRoom();
        }
    }

    private void SpawnFirstRoom()
    {
        RoomController room =
            Instantiate(
                firstRoomPrefab,
                Vector3.zero,
                Quaternion.identity
            );

        SetupRoom(room);

        activeRooms.Add(room);

        nextRoomZ = roomLength;
    }

    private void SpawnNormalRoom()
    {
        RoomController room =
            Instantiate(
                normalRoomPrefab,
                new Vector3(
                    0f,
                    0f,
                    nextRoomZ
                ),
                Quaternion.identity
            );

        SetupRoom(room);

        activeRooms.Add(room);

        nextRoomZ += roomLength;
    }

    private void SetupRoom(RoomController room)
    {
        if (room == null)
            return;

        room.SetupQuestion(questionManager);
    }

    private void RemoveOldestRoom()
    {
        if (activeRooms.Count <= 4)
            return;

        RoomController oldRoom =
            activeRooms[0];

        activeRooms.RemoveAt(0);

        if (oldRoom != null)
        {
            Destroy(oldRoom.gameObject);
        }
    }
}
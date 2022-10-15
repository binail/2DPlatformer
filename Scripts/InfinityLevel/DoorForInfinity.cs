using UnityEngine;

public class DoorForInfinity : MonoBehaviour
{
    public Transform previousRoom;
    public Transform nextRoom;
    public CameraController cam;
    private bool playerEnter = false;

    private void Start()
    {
        previousRoom.GetComponent<Room>().ActivateRoom(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                cam.MoveToNewRoom(nextRoom);
                nextRoom.GetComponent<Room>().ActivateRoom(true);
                previousRoom.GetComponent<Room>().ActivateRoom(false);
                if (!playerEnter)
                {
                    playerEnter = true;
                    collision.GetComponent<PlayerScore>().AddScore();
                }
            }
            else
            {
                cam.MoveToNewRoom(previousRoom);
                nextRoom.GetComponent<Room>().ActivateRoom(false);
                previousRoom.GetComponent<Room>().ActivateRoom(true);
            }
        }
    }


}

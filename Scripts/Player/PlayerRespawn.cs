using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;


    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn()
    {
        //Check if check point avalable
        if(currentCheckpoint == null)
        {
            //Show game over screen
            uiManager.GameOver();

            return;
        }

        transform.position = currentCheckpoint.position;
        playerHealth.Respawn();//Restore health and reset animation

        //Move camera to checkpoint room
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }

    //Activate checkpoints

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.inctance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;//Deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("Appear");
        }
    }
}

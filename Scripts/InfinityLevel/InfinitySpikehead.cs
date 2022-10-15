using UnityEngine;

public class InfinitySpikehead : EnemyDamage
{
    [Header("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;

    [Header("Trap Attributes")]
    [SerializeField] private GameObject trapper;
    [SerializeField] private float conclisionTime;
    private float timer;
    private bool isPlayerEnter;

    [Header("SFX")]
    [SerializeField] private AudioClip impactSound;

    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private bool attacking;
    private float checkTimer;

    private void OnEnable()
    {
        Stop();
        timer = 0;
        isPlayerEnter = false;
    }
    private void Update()
    {
        //Start the timer if player enters the room
        if (isPlayerEnter && timer < conclisionTime) 
        {
            trapper.SetActive(true);
            timer += Time.deltaTime;

            if(timer >= conclisionTime)
            {
                trapper.SetActive(false);
            }
        }

        //Move spikehead to destination only if attacking
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }
    private void CheckForPlayer()
    {
        CalculateDirections();

        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                isPlayerEnter = true;
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range;//Right direction
        directions[1] = -transform.right * range;//Left direction
        directions[2] = transform.up * range;//Up direction
        directions[3] = -transform.up * range;//Up direction
    }
    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.inctance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);
        Stop();//Stop Spikehead if it hit something
    }
}

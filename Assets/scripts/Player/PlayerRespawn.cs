using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Heal playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Heal>();
        uiManager = FindObjectOfType<UIManager>();
    }

    private void CheckRespawn()
    {
       //check if check point available
       if(currentCheckpoint == null)
        {
            //Show game over screen
            uiManager.GameOver();

            return;//Don't execute the rest of this function
        }

        playerHealth.Respawn();
        transform.position = currentCheckpoint.position;
        GetComponent<PlayerAttacks>().enabled = true;



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "checkpoint" && playerHealth.currentHealth != 0)
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
          
        }
    }
}

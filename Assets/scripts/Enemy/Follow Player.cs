
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField] private float followPlayer;

    public Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer > 1)
        this.Follow();
    }

    void Follow()
    {
        Vector3 distance = this.Player.position - transform.position;
        if ( distance.magnitude >= 3)
        {
            Vector3 targetPoint = this.Player.position - distance.normalized * 3;
            gameObject.transform.position = 
                Vector3.MoveTowards(gameObject.transform.position, targetPoint, 15 * Time.deltaTime);
        }
    }
}

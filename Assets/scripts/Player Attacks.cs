using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound; 

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }


    //câu lệnh giúp tạo khoảng cách giữa 2 tia lửa bắn ra liên tục
    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown ) 
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        
        //

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x * transform.localScale.y));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;

    }

}

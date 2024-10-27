using UnityEngine;
using UnityEngine.UI;

public class Heal : MonoBehaviour
{
	[Header("Health")]
	[SerializeField] private float startingHealth;
	public float currentHealth { get; private set; }
	private Animator anim;
	private bool dead;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

	[Header("Deadth Sound")]
	[SerializeField] private AudioClip deathSound;

	private void Awake()
	{
		currentHealth = startingHealth;
		anim = GetComponent<Animator>();
	}

	public void TakeDamage(float _damage)
	{
		currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

		if (currentHealth <= 0)
		{
           
			anim.SetTrigger("Die");

            /*
            //Player
            if (GetComponent<PlayerMovement>() != null )
			{ 
			    GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerAttacks>().enabled = false;
            }

            //Enemy

			if(GetComponentInParent<EnemyPatrolling>() != null)
			{
				GetComponentInParent<EnemyPatrolling>().enabled = false;
            }
            if (GetComponentInParent<MeleeEnemy>() != null)
            {
                GetComponentInParent<MeleeEnemy>().enabled = false;
            }*/
              
            //Deactivate all attached component classes
            foreach (Behaviour component in components)
                component.enabled = false;

            //sửa lỗi không thể respaw khi đang jump
            anim.SetBool("grounded", true);
			anim.SetTrigger("die");

			
			SoundManager.instance.PlaySound(deathSound);
		}
        else
        {
            if (currentHealth  < startingHealth)
            anim.SetTrigger("Hurt");
           
        }
	}
	 

	public void AddHealth(float _value)
	{
		currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
	}

	public void Respawn()
	{
		
		AddHealth(startingHealth);
		anim.ResetTrigger("die");
		anim.Play("Idle");
		GetComponent<PlayerMovement>().enabled = true;
		if (GetComponentInParent<MeleeEnemy>() != null)
		{
			GetComponentInParent<MeleeEnemy>().enabled = true;
			gameObject.SetActive(true);
		}
	}

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

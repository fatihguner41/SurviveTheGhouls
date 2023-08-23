using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ghoul : MonoBehaviour
{
    public Animator animator;
    GameObject player;
    public float speed=5;
    public int damage=25;
    public int health=100;
    public float attackSpeed=2;
    float attackSpeed2;
    NavMeshAgent nav;

    bool isDead=false;

    public AudioSource audioSource;
    public AudioClip[] idleSounds;
    public AudioClip[] attackSounds;
    public AudioClip[] deathSounds;

    private float stopThreshold = 0.1f; // Eþik deðeri belirleyin (sýfýra yakýn bir deðer)


    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.speed = speed;
        attackSpeed2 = attackSpeed;
        player = GameObject.Find("Player");
        audioSource = GetComponent<AudioSource>();

        InvokeRepeating("GhoulScreamSound", Random.Range(0f, 5f), Random.Range(0f, 12f));
        InvokeRepeating("SetDestination", 0f, 2f);

    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            attackSpeed2 -= Time.deltaTime;

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer < 50f)
            {
                nav.SetDestination(player.transform.position);
            }


            if (nav.velocity.magnitude<stopThreshold && animator.GetBool("Run"))
            {
                animator.SetBool("Run", false);
            }

            if(nav.velocity.magnitude>=stopThreshold && !animator.GetBool("Run"))
            {
                animator.SetBool("Run", true);
            }
            

            if (Vector3.Distance(player.transform.position, transform.position) < 5 && attackSpeed2 < 0)
            {
                player.GetComponent<PlayerMovement>().health -= 25;
                animator.Play("Attack2");
                attackSpeed2 = attackSpeed;

                audioSource.Stop();

                AudioClip randomAttackSound = attackSounds[Random.Range(0, attackSounds.Length)];

                // Seçilen idle sesini çal
                audioSource.PlayOneShot(randomAttackSound);
            }
        }
        
        
       

       

        if (health <= 0 && !isDead)
        {
            isDead = true;
            animator.Play("Death");
            GetComponentsInChildren<Collider>()[2].enabled = false;
            GetComponentsInChildren<Collider>()[1].enabled = false;
            GetComponentsInChildren<Collider>()[2].enabled = false;
            nav.enabled = false;

            
            //audioSource.Stop();
            
            AudioClip randomDeathSound = attackSounds[Random.Range(0, deathSounds.Length)];

            // Seçilen idle sesini çal
            audioSource.PlayOneShot(randomDeathSound);
            


            
            Invoke("OnDeath", 3);
        }

        if(health<=0)
        {
            transform.position -= new Vector3(0, Time.deltaTime * 2, 0);
        }

        
    }

    public void GhoulScreamSound()
    {
        if (!audioSource.isPlaying && isDead==false)
        {
            // Rasgele bir idle sesini seçin
            AudioClip randomIdleSound = idleSounds[Random.Range(0, idleSounds.Length)];

            // Seçilen idle sesini çal
            audioSource.PlayOneShot(randomIdleSound);
        }
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }

    public void SetDestination()
    {
        if (health > 0)
        {
            nav.SetDestination(player.transform.position);
        }
    }
}

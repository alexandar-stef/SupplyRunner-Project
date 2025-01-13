using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{

    public AudioClip[] zombieSounds;
    private AudioSource soundSource;

    public float minDelay = 10;
    public float maxDelay = 30;
    MeshCollider meshCollider;
    public Transform playerPos;
    private NavMeshAgent Zombie;
    public float navigationDistanceThreshold = 10.0f;
    public float zombieClose = 2.0f;
    public float zombieDamage = 5.0f;
    Rigidbody rb;

    float distanceToTarget; 

    ZombieDrop zombieDrop;
    PlayerHealth playerHealth;

    public float zombieHP;
    
    public float attackdelay = 2.0f;

    bool Attacking = false;

    public Animator zombieAnimator;

    NavMeshPath pathToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        pathToPlayer = new NavMeshPath();
        zombieDrop = GetComponent<ZombieDrop>();
        soundSource = GetComponent<AudioSource>();
        meshCollider = GetComponent<MeshCollider>();
        rb = GetComponent<Rigidbody>();
        Zombie = GetComponent<NavMeshAgent>();
        zombieAnimator = GetComponent<Animator>();
        zombieAnimator.SetBool("Idle", true);
        zombieHP = GlobalVariables.zombieHealth;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        Zombie.enabled = true;
        playerHealth = playerPos.gameObject.GetComponent<PlayerHealth>();

    }


    // Update is called once per frame
    void Update()
    {
        if (Zombie.isActiveAndEnabled && Zombie.isOnNavMesh)
        {        

            if(zombieHP <= 0)
            {
                KillZombie();
                zombieDrop.Drop();
            }
            else
            {
                //float distanceToTarget = Vector3.Distance(transform.position, playerPos.position);
                float distanceToTarget = CalculateDistance();

                        // Check if the distance is below the threshold
                if (distanceToTarget <= navigationDistanceThreshold && distanceToTarget >= zombieClose)
                {
                    //plays the zombie sound
                    //if player is close enough to zombie, play the walking animation

                    zombieAnimator.SetBool("Idle", false);
                    zombieAnimator.SetBool("Walking", true);
                    zombieAnimator.SetBool("Attacking", false);
                    Attacking = false;

                    // Set the destination for the NavMeshAgent
                    Zombie.SetDestination(playerPos.position);

                    // Get the direction from the current object position to the target position
                    Vector3 directionToTarget = playerPos.position - transform.position;

                    // Project the direction onto the x-axis (ignoring the y and z components)
                    Vector3 xDirection = Vector3.ProjectOnPlane(directionToTarget, Vector3.up);

                    // Calculate the rotation to look along the x-axis
                    Quaternion xRotation = Quaternion.LookRotation(xDirection, Vector3.up);

                    // Apply the rotation only to the x-axis
                    transform.rotation = xRotation;
                    StartCoroutine(zombieGroanDelay());
                }
                if(distanceToTarget <= zombieClose)
                {
                    zombieAnimator.SetBool("Idle", false);
                    zombieAnimator.SetBool("Walking", false);
                    zombieAnimator.SetBool("Attacking", true);
                    Attacking = true;

                }
                //if player is far enough away, just play the idle animation
                if(distanceToTarget >= navigationDistanceThreshold)
                {
                    zombieAnimator.SetBool("Idle", true);
                    zombieAnimator.SetBool("Walking", false);
                    zombieAnimator.SetBool("Attacking", false);
                }
            }
        }

    }

    void FixedUpdate(){
        if(Attacking == true && attackdelay <= 0)
        {
            playerHealth.TakeDamage(zombieDamage);
            attackdelay = 2.0f;
        }
        else if(Attacking == true)
        {
            attackdelay -= Time.deltaTime;
        }
        
    }

    // private void OnControllerColliderHit(ControllerColliderHit hit) {
    //     Debug.Log("Zombie hit player in ControllerColliderHit");
    //     if(hit.gameObject.tag == "Player")
    //     {   
    //         Debug.Log("Zombie hit player");
    //         PlayerHealth playerHealth = hit.gameObject.GetComponent<PlayerHealth>();
    //         playerHealth.TakeDamage(zombieDamage);

    //     }
    // }

    // public void OnCollisionEnter(Collision collision)
    // {
    //     Debug.Log("Zombie hit player in OnCollisionEnter");
    //     if(collision.gameObject.CompareTag("Player"))
    //     {   
    //         Debug.Log("Zombie hit player");
    //         PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
    //         playerHealth.TakeDamage(zombieDamage);
    //     }
    // }

    public void KillZombie()
    {
        Zombie.enabled = false;
        Destroy(meshCollider);
        zombieAnimator.SetBool("Idle", false);
        zombieAnimator.SetBool("Walking", false);
        zombieAnimator.SetBool("Attacking", false);
        zombieAnimator.SetBool("Death", true);
        Destroy(gameObject, 2);

        PlayerLevel playerLevel = playerPos.gameObject.GetComponent<PlayerLevel>();
        playerLevel.ZombieKill();
        Attacking = false;
    }

    public void TakeDamage(float damage)
    {
        zombieHP -= damage;
    }


    IEnumerator zombieGroanDelay()
    {

        // Play a random sound
        PlayRandomSound();

        // Wait for the sound to finish playing before starting the next delay
        yield return new WaitForSeconds(soundSource.clip.length);
        

        // Wait for the next delay
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        
    }


    public void PlayRandomSound()
    {
        if (zombieSounds.Length > 0)
        {
            // Randomly select a sound effect from the array
            AudioClip randomSound = zombieSounds[Random.Range(0, zombieSounds.Length)];

            // Play the selected sound effect
            soundSource.clip = randomSound;
            soundSource.Play();
        }
    }

    float CalculateDistance()
    {
        if (Vector3.Distance(transform.position, playerPos.position) > navigationDistanceThreshold) return 1000;
        if (!NavMesh.CalculatePath(transform.position, playerPos.position, NavMesh.AllAreas, pathToPlayer)) return 1000;

        float fullDistance = 0;

        for (int i = 1; i < pathToPlayer.corners.Length; i++)
        {
            fullDistance += Vector3.Distance(pathToPlayer.corners[i-1], pathToPlayer.corners[i]);
        }

        return fullDistance;
    }
}
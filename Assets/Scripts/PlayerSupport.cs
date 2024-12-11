using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSupport : MonoBehaviour
{
    Animator animator;
    public int health = 1;

    public GameObject bullet;

    public float time = 0;

    public GameObject gunPos;

    public bool inPlace = false;

    public float distance;

    bool attack = true;

    public GameObject playerDie;

    public GameObject[] gos;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        // Set initial health based on PlayerPrefs
        health = Mathf.Clamp(PlayerPrefs.GetInt("Health", 1), 1, 5);
    }

    void Update()
    {
        GameObject closeEnemy = FindClosestEnemy();

        if (closeEnemy != null)
        {
            distance = Vector3.Distance(gameObject.transform.position, closeEnemy.transform.position);
        }
        else
        {
            distance = 20;
        }

        if (gos.Length != 0)
        {
            if (distance < 12)
            {
                transform.LookAt(closeEnemy.transform.position);
                HandleAnimationAndShooting(true);
            }
            else
            {
                HandleAnimationAndShooting(false);
            }
        }
        else
        {
            HandleIdleOrRun();
        }
    }

    void HandleAnimationAndShooting(bool withinRange)
    {
        if (withinRange)
        {
            if (!inPlace)
            {
                animator.SetBool("run", true);
                animator.SetBool("idle", false);
                animator.SetBool("shoot", false);
                animator.SetBool("hit", false);

                ShootBullet(1f);
            }
            else
            {
                animator.SetBool("run", false);
                animator.SetBool("idle", false);
                animator.SetBool("shoot", true);
                animator.SetBool("hit", false);

                ShootBullet(3f);
            }
        }
        else
        {
            HandleIdleOrRun();
        }
    }

    void HandleIdleOrRun()
    {
        if (!inPlace)
        {
            animator.SetBool("run", true);
            animator.SetBool("idle", false);
            animator.SetBool("shoot", false);
            animator.SetBool("hit", false);
        }
        else
        {
            animator.SetBool("run", false);
            animator.SetBool("idle", true);
            animator.SetBool("shoot", false);
            animator.SetBool("hit", false);
        }
    }

    void ShootBullet(float cooldown)
    {
        if (time == 0)
        {
            Instantiate(bullet, gunPos.transform.position, gameObject.transform.rotation);
        }

        time += Time.deltaTime;

        if (time > cooldown)
        {
            time = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (GameObject.FindGameObjectWithTag("Game Manager").GetComponent<Gamemanager>().guard == false)
        {
            if (other.gameObject.tag == "Bullet Enemy")
            {
                HandleDamage(other);
            }

            if (other.gameObject.tag == "Enemy Hand")
            {
                HandleMeleeAttack();
            }
        }

        if (other.gameObject.tag == "Coin")
        {
            GameObject.FindGameObjectWithTag("Game Manager").GetComponent<Gamemanager>().coins += 1;
            Destroy(other.gameObject);
        }
    }

    void HandleDamage(Collider other)
    {
        if (health > 0)
        {
            health -= 1;
            GameObject.Find("Player").GetComponent<MainPlayer>().health -= 1;

            if (health == 0)
            {
                Destroy(gameObject); // Destroy the player
            }
            Destroy(other.gameObject);
        }
        else
        {
            Destroy(gameObject); // Destroy the player
            Destroy(other.gameObject);
        }
    }

    void HandleMeleeAttack()
    {
        if (health > 0 && attack)
        {
            health -= 1;
            GameObject.Find("Player").GetComponent<MainPlayer>().health -= 1;

            if (health == 0)
            {
                Destroy(gameObject); // Destroy the player
            }
            attack = false;
            StartCoroutine(ResetAttack());
        }
        else if (attack)
        {
            Destroy(gameObject); // Destroy the player
            attack = false;
            StartCoroutine(ResetAttack());
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1f);
        attack = true;
    }

    public GameObject FindClosestEnemy()
    {
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}

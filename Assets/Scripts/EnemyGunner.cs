using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGunner : MonoBehaviour
{
    public float speed = 5f;
    public float distanceBetween;

    public int health = 1;

    public Animator animator;
    public GameObject bullet;
    public GameObject gunPos;
    float time = 0;
    public GameObject[] gos;
    public int index;
    public bool toBeAdded = false;

    public GameObject blood;

    public GameObject coin;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Main Player").Length > 0)
        {
            GameObject closestObject = FindClosestEnemy();

            float step = speed * Time.deltaTime;
            distanceBetween = Vector3.Distance(transform.position, closestObject.transform.position);

            if (distanceBetween < 15)
            {
                if (!toBeAdded)
                {
                    index = closestObject.GetComponent<EnemyList>().enemies.Count;
                    closestObject.GetComponent<EnemyList>().enemies.Add(gameObject);
                    toBeAdded = true;
                }
            }

            if (distanceBetween < 15 && distanceBetween > 7)
            {
                gameObject.isStatic = false;
                animator.SetBool("Walk", true);
                animator.SetBool("Idle", false);
                animator.SetBool("Attack", false);
                GetComponent<NavMeshAgent>().isStopped = false;

                if (index - 1 >= 0 && index - 1 < closestObject.GetComponent<EnemyList>().targetPositionList.Count)
                {
                    GetComponent<NavMeshAgent>().destination = closestObject.GetComponent<EnemyList>().targetPositionList[index - 1];
                }
            }
            else if (distanceBetween > 15)
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Idle", true);
                animator.SetBool("Attack", false);
                GetComponent<NavMeshAgent>().isStopped = true;
            }
            else
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Idle", false);
                animator.SetBool("Attack", true);
                transform.LookAt(closestObject.transform);

                if (time == 0)
                {
                    Instantiate(bullet, gunPos.transform.position, transform.rotation);
                }

                time += Time.deltaTime;

                if (time > 3)
                {
                    time = 0;
                }
            }
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);
            animator.SetBool("Attack", false);
            GetComponent<NavMeshAgent>().isStopped = true;
        }
    }

    public GameObject FindClosestEnemy()
    {
        gos = GameObject.FindGameObjectsWithTag("Main Player");
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (health > 0)
            {
                health -= 1;

                if (health == 0)
                {
                    if (blood != null)
                    {
                        Instantiate(blood, new Vector3(transform.position.x, 1.65f, transform.position.z), transform.rotation);
                    }

                    GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerController>().CoinInst();

                    if (GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerController>().coinBool)
                    {
                        if (PlayerPrefs.GetInt("Level", 1) > 1)
                        {
                            Instantiate(coin, new Vector3(transform.position.x, 2f, transform.position.z), coin.transform.rotation);
                        }
                        else
                        {
                            Instantiate(coin, new Vector3(transform.position.x, 0.35f, transform.position.z), coin.transform.rotation);
                        }
                    }

                    Destroy(gameObject);
                }

                Destroy(other.gameObject);
            }
            else
            {
                if (blood != null)
                {
                    Instantiate(blood, new Vector3(transform.position.x, 1.65f, transform.position.z), transform.rotation);
                }

                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    public float speed = 10f;
    public GameObject joyStick;
    private Rigidbody rb;
    private Vector3 moveDirection;
    private Animator playerAnimator;

    public int health = 3;
    public GameObject gunPos;
    public GameObject bullet;
    private float time = 0;

    private float distance;
    public GameObject[] gos;

    void Start()
    {
        // Initialize components
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        joyStick = GameObject.FindGameObjectWithTag("Joystick");

        health = Mathf.Clamp(PlayerPrefs.GetInt("Health", 3), 1, 5);
    }

    void Update()
    {
        if (health <= 0)
        {
            HandlePlayerDeath();
            return;
        }

        HandleMovement();

        GameObject closeEnemy = FindClosestEnemy();
        if (closeEnemy != null)
        {
            distance = Vector3.Distance(transform.position, closeEnemy.transform.position);
            HandleCombat(closeEnemy);
        }
        else
        {
            distance = float.MaxValue;
        }
    }

    private void HandleMovement()
    {
        if (joyStick == null) return;

        float horizontal = joyStick.GetComponent<Joystick>().Horizontal;
        float vertical = joyStick.GetComponent<Joystick>().Vertical;

        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            Vector3 targetPosition = rb.position + moveDirection * speed * Time.deltaTime;
            targetPosition.y = rb.position.y;
            rb.MovePosition(targetPosition);

            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);

            PlayAnimation("run");
        }
        else
        {
            rb.velocity = Vector3.zero;
            PlayAnimation("idle");
        }
    }

    private void HandleCombat(GameObject closeEnemy)
    {
        if (distance < 12)
        {
            transform.LookAt(closeEnemy.transform.position);

            if (moveDirection.magnitude > 0.1f)
            {
                PlayAnimation("run");
                FireBullet(1f);
            }
            else
            {
                PlayAnimation("shoot");
                FireBullet(3f);
            }
        }
    }

    private void FireBullet(float cooldown)
    {
        if (time == 0)
        {
            Instantiate(bullet, gunPos.transform.position, transform.rotation);
        }

        time += Time.deltaTime;

        if (time > cooldown)
        {
            time = 0;
        }
    }

    private void PlayAnimation(string state)
    {
        playerAnimator.SetBool("run", state == "run");
        playerAnimator.SetBool("idle", state == "idle");
        playerAnimator.SetBool("shoot", state == "shoot");
        playerAnimator.SetBool("hit", state == "hit");
    }

    private void HandlePlayerDeath()
    {
        PlayAnimation("hit");
        Gamemanager gamemanager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<Gamemanager>();
        if (gamemanager != null)
        {
            gamemanager.TriggerLoseMenu();
        }
    }

    public GameObject FindClosestEnemy()
    {
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject go in gos)
        {
            float curDistance = Vector3.Distance(transform.position, go.transform.position);
            if (curDistance < closestDistance)
            {
                closest = go;
                closestDistance = curDistance;
            }
        }

        return closest;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet Enemy"))
        {
            GameObject.FindGameObjectWithTag("Game Manager").GetComponent<Gamemanager>().UpdateHealth(-1);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Coin"))
        {
            GameObject.FindGameObjectWithTag("Game Manager").GetComponent<Gamemanager>().AddCoin(1);
            Destroy(other.gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public new GameObject camera;
    private int regularSpeed;
    public int speed;
    public int currentHealth;
    public int maxHealth;
    public float killTime;
    public float startingKillTime;
    public GameObject marker;
    public GameObject wrench;
    public int wrenchDamage;
    public LayerMask whatToHit;
    public GameObject timeBomb;
    public float maxSprint;
    private float sprintTime = 0;
    private float sprintCooldown = 0;
    public Animator anim;
    public GameObject pivot;
    public AudioSource wrenchSource;
    public TimeTravelPlayer PlayerTimeTravel;
    public Slider HealthSlider;
    public Text HealthText;

    public static PlayerController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    // Update is called once per frame
    private void Start()
    {
        currentHealth = maxHealth;
        HealthSlider.maxValue = maxHealth;
        regularSpeed = speed;
        sprintTime = maxSprint;
    }
    void Update()
    {
        if (!PlayerTimeTravel.rewinding)
        {
            sprintCooldown -= Time.deltaTime;
            if (sprintTime >= 0 && sprintCooldown <= 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    speed = regularSpeed * 2;
                    sprintTime -= Time.deltaTime;
                }
                else
                {
                    speed = regularSpeed;
                }
            }
            else if (sprintTime <= 0)
            {
                sprintTime = maxSprint;
                sprintCooldown = maxSprint * 2;
                speed = regularSpeed;
            }
            anim.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
            transform.position += (new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, 0));
            FaceMouseDirection();
            if (Input.GetMouseButtonDown(0))
            {
                SwingWrench();
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                Instantiate(timeBomb, pivot.transform.position, pivot.transform.rotation);
            }
        }
    }
    public TimeTravel[] ChangeHealth(int amt)
    {
        currentHealth += amt;
        HealthSlider.value = currentHealth;
        HealthText.text = "Health: " + currentHealth;
        if (currentHealth <= 0)
        {
            return EnemyManager.Instance.GetEnemiesTimeTravel();
        }
        return null;
    }
    public void SetHealth(int amt)
    {
        currentHealth = amt;
        HealthSlider.value = currentHealth;
        HealthText.text = "Health: " + currentHealth;
    }
    public void ChangeHealth(int amt, AIController killer)
    {
        TimeTravel[] travelers = ChangeHealth(amt);
        if (currentHealth <= 0)
        {
            StartCoroutine(DeathRewind(killer, travelers));
        }
    }
    public void FaceMouseDirection()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        pivot.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }
    IEnumerator DeathRewind(AIController killer, TimeTravel[] travelers)
    {
        Debug.Log("Kill timer started");
        killTime = startingKillTime;
        bool stillRewinding = true;
        while (stillRewinding)
        {
            foreach (TimeTravel tt in travelers)
            {
                if (tt != null)
                    stillRewinding = tt.rewinding;
                yield return null;
            }
        }
        Instantiate(marker, new Vector3(killer.transform.position.x, killer.transform.position.y + 1), killer.transform.rotation, killer.transform);
        while (killTime > 0)
        {
            killTime -= Time.deltaTime;
            yield return null;
        }
        if (killer.gameObject.GetComponent<enemyHealth>().health > 0)
        {
            SceneManager.LoadScene(4);
        }
    }
    void SwingWrench()
    {
        wrench.GetComponent<Animator>().SetTrigger("Swing");
        RaycastHit2D hit = Physics2D.Raycast(wrench.transform.position, transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition), 2f, whatToHit);
        if (hit.collider != null)
        {
            wrenchSource.Play();
            Debug.Log("Hit " + hit.collider.name);
            hit.collider.GetComponent<AIController>().ChangeHealth(wrenchDamage, false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class EnemyStats : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemyData;
    [HideInInspector] public float currentMoveSpeed;
    [HideInInspector] public float currentHealth;
    [HideInInspector] public float currentDamage;

    public float deSpawnDistance = 20f;   //the distance enemy will de-spawn when player walk out from the enemy
    private Transform player;

    [Header("Damage Feedback")]
    public Color damageColor = new Color(1, 0, 0, 1); //Color when take Damage form player
    public float damageFlashDuration = 0.2f; //Time flash when take damage
    public float deathFadeTime = 0.6f; //Time to fading the enemy when dead
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    private EnemyMovement enemyMovement;

    private void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.Health;
        currentDamage = enemyData.Damage;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        CheckDistanceFromPlayer();
    }
    public void TakeDamage(float dmg, Vector2 sourcePosition, float knockBackForce = 3f, float knockBackDuration = 0.2f)
    {
        currentHealth -= dmg;
        StartCoroutine(DamageFlash());

        if (dmg > 0)
        {
            GameManager.GenerateFloatingText(Mathf.FloorToInt(dmg).ToString(), transform);
        }

        //Aplly Knockback when knockback Force is not zero
        if (knockBackForce > 0)
        {
            Vector2 direction = (Vector2)transform.position - sourcePosition;
            enemyMovement.ReadyKnockBack(direction.normalized * knockBackForce, knockBackDuration);
        }
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    IEnumerator DamageFlash()
    { //Take Damage --> flash the color of sprite's enemy
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(deathFadeTime);
        spriteRenderer.color = originalColor;
    }

    public void Kill()
    {
        StartCoroutine(KillFade());
        AudioManager.instance.PlaySFX(FindObjectOfType<AudioManager>().enemyDeath);
    }

    IEnumerator KillFade()
    {
        //Wait for single frame
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        float time = 0;
        float originalAlpha = spriteRenderer.color.a;

        //Loop that enemy fade enemy frame
        while (time < deathFadeTime)
        {
            yield return wait;
            time += Time.deltaTime;

            //Set the color for every frame
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, (1 - time / deathFadeTime) * originalAlpha);
        }
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats player = other.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
            Debug.Log("Attacking");
        }
    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded)
        {
            return;
        }
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.OnEnemyKilled();
    }

    private void CheckDistanceFromPlayer()
    {
        if (Vector2.Distance(transform.position, player.position) >= deSpawnDistance)
        {
            ReturnEnemyClosePlayer();
        }
    }

    private void ReturnEnemyClosePlayer()
    {
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + enemySpawner.relativeSpawnPoints[Random.Range(0, enemySpawner.relativeSpawnPoints.Count)].position;
    }
}

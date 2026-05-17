using UnityEngine;
using UnityEngine.UI;

public class HeroHealth : MonoBehaviour
{
    public int health = 9;
    public Image heartDisplayImage;
    public Sprite[] heartSprites;

    public Transform spawnPoint;
    public EnemyHealth enemyScript;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0) health = 0;

        UpdateUI();

        if (health <= 0)
        {
            Debug.Log("«доровье закончилось. –естарт раунда!");
            Respawn();
        }
    }

    void UpdateUI()
    {
        if (health >= 0 && health < heartSprites.Length)
        {
            heartDisplayImage.sprite = heartSprites[health];
        }
    }

    public void Respawn()
    {
        transform.position = spawnPoint.position;
        health = 9;
        UpdateUI();

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        if (enemyScript != null)
        {
            enemyScript.ResetPosition();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathZone"))
        {
            // ƒЋя ѕјƒ≈Ќ»я 
            Debug.Log("–естарт раунда!");
            Respawn();
        }
    }
}
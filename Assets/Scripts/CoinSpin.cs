using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    [Header("Кадры анимации")]
    public Sprite[] frames;
    public float fps = 10f;

    private SpriteRenderer sr;
    private float timer;
    private int currentFrame;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (frames == null || frames.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= 1f / fps)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % frames.Length;
            sr.sprite = frames[currentFrame];
        }
    }
}

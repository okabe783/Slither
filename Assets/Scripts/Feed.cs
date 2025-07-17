using UnityEngine;

/// <summary>
/// 餌にアタッチ
/// </summary>
[RequireComponent(typeof(CircleCollider2D))]
public class Feed : MonoBehaviour
{
    private float _score;
    [SerializeField] private Color _color;

    public void Init(FeedSettings feedSettings)
    {
        _score = feedSettings.Score;
        _color = feedSettings.Color;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // スコア加算処理
        if (other.CompareTag("Player"))
        {
            // なんかダサい
            other.gameObject.GetComponent<PlayerController>().AddScore(_score);
        }
        
        Destroy(gameObject);
    }
}
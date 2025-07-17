using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _body;
    [SerializeField] private int _generateFeedCount;
    [SerializeField] private float _instanceBodyPos;
    [SerializeField] private FeedGenerator _feedGenerator;
    // 何スコアで身体を生成するか
    [SerializeField] private float _bodyPerScore = 0.5f;

    // 現在の長さをカウント
    private int _bodyUpCount;
    private float _score;
    public Action<Vector2> OnDeath;

    private void Awake()
    {
        _bodyUpCount = 0;
    }

    private void Start()
    {
        OnDeath += DestroyPlayer;
    }

    private void DestroyPlayer(Vector2 pos)
    {
        // 必要があれば割る値をserializeして設定
        int index = Mathf.Clamp(_bodyUpCount / 5, 0, _feedGenerator.FeedCount - 1);
        
        // 餌を生成
        for (int i = 0; i < _generateFeedCount; i++)
        {
            GameObject feed = _feedGenerator.GenerateFeed(index);
            // 少しずらして生成
            feed.transform.position = new Vector2(pos.x * i *2,pos.y);
        }
    }

    // スコアが溜まった時に長さを追加
    private void SetBody()
    {
        // Instanceして子に追加
        GameObject body = Instantiate(_body, transform);
        body.transform.localPosition = new Vector3(_bodyUpCount * _instanceBodyPos, 0, 0);
        body.GetComponent<Body>().Init(this);

        _bodyUpCount++;
    }
    
    private void RemoveBody()
    {
        if (transform.childCount > 0)
        {
            // 配列にするべきか
            Transform lastBody = transform.GetChild(transform.childCount - 1);
            Destroy(lastBody.gameObject);
            _bodyUpCount--;
        }
    }
    
    // スコアが更新された時に身体の数をチェック
    public void AddScore(float score)
    {
        _score += score;

        int targetLength = (int)(_score / _bodyPerScore);

        // 身体の変化処理
        while (_bodyUpCount < targetLength)
        {
            SetBody();
        }

        while (_bodyUpCount > targetLength)
        {
            RemoveBody();
        }
    }
}
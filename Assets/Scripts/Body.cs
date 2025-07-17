using UnityEngine;

/// <summary>
/// 追加される身体にアタッチ
/// </summary>
public class Body : MonoBehaviour
{
    private PlayerController _playerController;

    public void Init(PlayerController playerController)
    {
        _playerController = playerController;
    }

    private void OnDestroy()
    {
        // Feedを出す処理
        _playerController.OnDeath.Invoke(transform.position);
    }
}
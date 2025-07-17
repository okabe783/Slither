using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    
    [Header("CursorSettings")]
    private Vector2 _mousePosition;
    private Vector3 _target;

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Move(x,y);
    }

    private void Move(float x, float y)
    {
        transform.Translate(new Vector3(x,y) * (Time.deltaTime * _moveSpeed) );
        
        // マウスのcursor位置に向く(微妙)
        _target = Camera.main.ScreenToWorldPoint(transform.position);
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - _target);
        transform.rotation = rotation;
    }
}

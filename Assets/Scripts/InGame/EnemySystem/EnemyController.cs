using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody _rigidbody;

    [SerializeField, Tooltip("ドロップする経験値")]
    GameObject _dropItem = null;
    bool _isMove = true;

    private void Start()
    {
        if (!_dropItem)
        {
            _dropItem = Resources.Load<GameObject>("dango");
        }
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {

        Move();

    }

    private void Move()
    {
        if (_isMove)
        {
            Vector3 dir = GameManager.Instance.Player.transform.position - transform.position;

            _rigidbody.velocity = dir.normalized * 3f;
        }
    }

    public void SetPosition(Vector3 position)
    {
        this.transform.position = position;
    }

    public void Death()
    {
        EnemyManager.Instans.Pool.Return(this);
        EnemyManager.Instans.Pool.RemoveEnemy(this);
    }

    public void Pause()
    {
        _isMove = false;
    }

    public void Resume()
    {
        _isMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(_dropItem, this.transform.position, Quaternion.identity);
            Death();
        }
    }
}

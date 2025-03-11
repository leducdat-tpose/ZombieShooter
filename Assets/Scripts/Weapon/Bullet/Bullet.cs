using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 6.5f;
    private Vector2 direction = Vector2.zero;
    public void Initialise(Vector3 targetPosition)
    {
        direction = (targetPosition - transform.position).normalized;
        gameObject.SetActive(true);
    }
    private void Start() {
        StartCoroutine(ExistTime());
    }
    private void FixedUpdate() {
        transform.Translate(direction * _moveSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator ExistTime()
    {
        yield return new WaitForSecondsRealtime(3f);
        Destroy(this.gameObject);
    }
}

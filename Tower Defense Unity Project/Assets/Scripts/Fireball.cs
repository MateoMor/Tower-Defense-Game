using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 15f;
    public float radius = 3f;
    public int damage = 50;
    public GameObject explosionEffect;

    private Vector3 target;

    public void Initialize(Vector3 targetPosition)
    {
        target = targetPosition;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            Explode();
        }
    }

    void Explode()
    {
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Collider[] enemies = Physics.OverlapSphere(transform.position, radius);
        foreach (var enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}

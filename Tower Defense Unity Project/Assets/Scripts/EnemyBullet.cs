using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	private Transform target;

	public float speed = 70f;
	public int damage = 25;
	public float explosionRadius = 0f;
	public GameObject impactEffect;
	
	public void Seek (Transform _target)
	{
		Debug.Log("the target is: " + _target);
		target = _target;
	}

	// Update is called once per frame
	void Update () {

		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target);

	}

	void HitTarget ()
	{
		GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy(effectIns, 5f);

		if (explosionRadius > 0f)
		{
			Explode();
		} else
		{
			Damage(target);
		}

		Destroy(gameObject);
	}

	void Explode ()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider collider in colliders)
		{
			if (collider.CompareTag("Tower"))
			{
				Damage(collider.transform);
			}
		}
	}
	void Damage (Transform tower)
	{
		// Usar el sistema de salud de torres si está disponible
		TowerHealth towerHealth = tower.GetComponent<TowerHealth>();
		if (towerHealth != null)
		{
			towerHealth.TakeDamage(damage);
		}
		else
		{
			// Si la torre no tiene sistema de salud, mostrar un efecto y un mensaje
			Debug.Log("¡Torre dañada por bala enemiga!");
			
			Turret turret = tower.GetComponent<Turret>();
			if (turret != null)
			{
				// Añadir un efecto de daño
				GameObject damageEffect = (GameObject)Instantiate(impactEffect, tower.position, Quaternion.identity);
				Destroy(damageEffect, 2f);
			}
		}
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}

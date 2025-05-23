using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public float startSpeed = 10f;

	[HideInInspector]
	public float speed;

	public float startHealth = 100;
	private float health;

	public int worth = 50;

	public GameObject deathEffect;

	[Header("Unity Stuff")]
	public Image healthBar;

	private bool isDead = false;

	[Header("Enemy Attack Properties")]
	public bool canAttack = false;  // Parámetro para activar/desactivar la capacidad de atacar
	public float attackRange = 10f;  // Rango de ataque
	public GameObject bulletPrefab;  // Prefab de la bala
	public float fireRate = 1f;  // Disparos por segundo
	public Transform firePoint;  // Punto donde se generan las balas
	public string towerTag = "Tower";  // Tag de las torres

	private Transform target;  // Torre objetivo
	private float fireCountdown = 0f;

	void Start ()
	{
		speed = startSpeed;
		health = startHealth;

		// Si puede atacar, empezar a buscar torres cada cierto tiempo
		if (canAttack) 
		{
			InvokeRepeating("UpdateTarget", 0f, 0.5f);
		}
	}

	void Update()
	{
		// Si no puede atacar o no tiene objetivo, no hacer nada
		if (!canAttack || target == null)
			return;

		// Si tiene objetivo, disparar cuando el contador llegue a 0
		if (fireCountdown <= 0f)
		{
			Shoot();
			fireCountdown = 1f / fireRate;
		}

		fireCountdown -= Time.deltaTime;
	}

	void UpdateTarget()
	{
		if (!canAttack)
			return;

		// Encontrar todas las torres
		GameObject[] towers = GameObject.FindGameObjectsWithTag(towerTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestTower = null;

		// Encontrar la torre más cercana
		foreach (GameObject tower in towers)
		{
			float distanceToTower = Vector3.Distance(transform.position, tower.transform.position);
			if (distanceToTower < shortestDistance)
			{
				shortestDistance = distanceToTower;
				nearestTower = tower;
			}
		}

		// Si hay una torre cercana dentro del rango, convertirla en objetivo
		if (nearestTower != null && shortestDistance <= attackRange)
		{
			target = nearestTower.transform;
		}
		else
		{
			target = null;
		}
	}

	void Shoot()
	{
		// Crear bala en el punto de disparo
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

		EnemyBullet bullet = bulletGO.GetComponent<EnemyBullet>();

		if (bullet == null)
		{
			return;
		}

		// Indicar a la bala cuál es el objetivo
		if (bullet != null)
			bullet.Seek(target);
	}

	public void TakeDamage (float amount)
	{
		health -= amount;

		healthBar.fillAmount = health / startHealth;

		if (health <= 0 && !isDead)
		{
			Die();
		}
	}

	public void Slow (float pct)
	{
		speed = startSpeed * (1f - pct);
	}

	void Die ()
	{
		isDead = true;

		PlayerStats.Money += worth;

		GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 5f);

		WaveSpawner.EnemiesAlive--;

		Destroy(gameObject);
	}

	// Mostrar el rango de ataque en el editor
	void OnDrawGizmosSelected()
	{
		if (canAttack)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, attackRange);
		}
	}
}

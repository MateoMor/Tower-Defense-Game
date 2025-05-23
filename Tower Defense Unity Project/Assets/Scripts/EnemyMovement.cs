using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

	private Transform target;
	private int wavepointIndex = 0;
	
	// El transform del modelo o sprite que debe rotar
	public Transform modelTransform;

	private Enemy enemy;
	void Start()
	{
		enemy = GetComponent<Enemy>();

		target = Waypoints.points[0];
		
		// Si no se asignó el modelo en el inspector, intentar encontrarlo automáticamente
		if (modelTransform == null)
		{
			// Buscar un hijo que no sea el Canvas
			foreach (Transform child in transform)
			{
				if (child.GetComponent<Canvas>() == null)
				{
					modelTransform = child;
					break;
				}
			}
			
			// Si aún no lo encontramos, usar el transform principal como fallback
			if (modelTransform == null)
				modelTransform = transform;
		}
	}
	
	void Update()
	{
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

		// Hacer que solo el modelo del enemigo mire hacia donde se está moviendo
		if (dir != Vector3.zero && modelTransform != null)
		{
			Quaternion lookRotation = Quaternion.LookRotation(dir);
			modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, lookRotation, Time.deltaTime * 5f);
		}

		if (Vector3.Distance(transform.position, target.position) <= 0.4f)
		{
			GetNextWaypoint();
		}

		enemy.speed = enemy.startSpeed;
	}

	void GetNextWaypoint()
	{
		if (wavepointIndex >= Waypoints.points.Length - 1)
		{
			EndPath();
			return;
		}

		wavepointIndex++;
		target = Waypoints.points[wavepointIndex];
	}

	void EndPath()
	{
		PlayerStats.Lives--;
		WaveSpawner.EnemiesAlive--;
		Destroy(gameObject);
	}

}

using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour {

    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Visual")]
    public GameObject damageEffect;
    public Image healthBar;
    public bool showHealthBar = true;
    
    [Header("Tower Destruction")]
    public GameObject destructionEffect;
    public int refundAmount = 0; // Dinero reembolsado cuando la torre es destruida

    private bool isDestroyed = false;

    void Start()
    {
        currentHealth = maxHealth;
        
        // Si hay una barra de salud, inicializar
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
            // Inicialmente oculta a menos que se indique lo contrario
            healthBar.transform.parent.gameObject.SetActive(showHealthBar);
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDestroyed)
            return;

        currentHealth -= amount;
        
        // Actualizar barra de salud si existe
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
            // Mostrar barra de salud cuando recibe daño
            healthBar.transform.parent.gameObject.SetActive(true);
        }

        // Crear efecto de daño si existe
        if (damageEffect != null)
        {
            GameObject effect = Instantiate(damageEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }

        // Comprobar si la torre ha sido destruida
        if (currentHealth <= 0 && !isDestroyed)
        {
            DestroyTower();
        }
    }

    void DestroyTower()
    {
        isDestroyed = true;

        // Dar reembolso al jugador
        if (refundAmount > 0)
        {
            PlayerStats.Money += refundAmount;
        }

        // Crear efecto de destrucción si existe
        if (destructionEffect != null)
        {
            GameObject effect = Instantiate(destructionEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
        }

        FindAndResetNode();

        // Eliminar la torre
        Destroy(gameObject);
    }
      // Busca el nodo que contiene esta torre y resetea su estado isUpgraded
    void FindAndResetNode()
    {
        Node[] nodes = FindObjectsByType<Node>(FindObjectsSortMode.None);
        
        foreach (Node node in nodes)
        {
            if (node.turret == gameObject)
            {
                node.isUpgraded = false;
                break;
            }
        }
    }
}

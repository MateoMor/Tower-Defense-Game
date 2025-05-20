using UnityEngine;

public class FireballTargeting : MonoBehaviour
{
    public static FireballTargeting Instance { get; private set; }

    [Header("Settings")]
    public GameObject indicatorPrefab;
    public GameObject fireballPrefab;
    public LayerMask groundMask;
    public float fireballHeight = 10f;
    public float indicatorHeight = 2f;
    
    [Header("Economic")]
    public int fireballCost = 500;
    public KeyCode cancelKey = KeyCode.Escape;

    private GameObject indicatorInstance;
    public bool isActive = false;
    private Shop shopReference;

    public bool IsActive => isActive;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        // Método actualizado para encontrar el Shop
        shopReference = FindAnyObjectByType<Shop>();
        
        indicatorInstance = Instantiate(indicatorPrefab);
        indicatorInstance.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ToggleFireballMode(bool activate)
    {
        isActive = activate;
        gameObject.SetActive(activate);
        
        if (!activate && indicatorInstance != null)
        {
            indicatorInstance.SetActive(false);
        }
    }

    void Update()
    {
        if (!isActive) return;
        
        if (PlayerStats.Money < fireballCost)
        {
            Debug.Log("Not enough money for fireball");
            ToggleFireballMode(false);
            return;
        }

        if (Input.GetKeyDown(cancelKey))
        {
            ToggleFireballMode(false);
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 200f, groundMask))
        {
            indicatorInstance.SetActive(true);
            Vector3 hitPoint = hit.point;
            hitPoint.y = indicatorHeight;
            indicatorInstance.transform.position = hitPoint;

            if (Input.GetMouseButtonDown(0))
            {
                TryLaunchFireball(hitPoint);
            }
        }
        else
        {
            indicatorInstance.SetActive(false);
        }
    }

    void TryLaunchFireball(Vector3 targetPosition)
    {
        if (PlayerStats.Money >= fireballCost)
        {
            PlayerStats.Money -= fireballCost;
            SpawnFireball(targetPosition);
        }
        else
        {
            Debug.Log("Not enough money to launch fireball");
        }
    }

    void SpawnFireball(Vector3 targetPosition)
    {
        Vector3 spawnPosition = targetPosition + Vector3.up * fireballHeight;
        GameObject fireball = Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);
        fireball.GetComponent<Fireball>().Initialize(targetPosition);
    }
}
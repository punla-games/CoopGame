using UnityEngine;

public class CustomerManager:MonoBehaviour
{
    public Transform spawnPoint;
    public Transform deliveryPoint;

    public Customer customerPrefab;

    public const float SPAWN_DELAY = 1f;
    private float spawnTimer = 0f;

    public void Update()
    {
        spawnTimer=Mathf.Max(spawnTimer-Time.deltaTime,0f);
        
        int customerCount = FindObjectsByType<Customer>(FindObjectsSortMode.None).Length;
        if(customerCount>=CustomerQueueManager.Get.queuePoints.Length)
            return;

        if(spawnTimer>0f)
            return;
        spawnTimer=SPAWN_DELAY;

        SpawnCustomer();
    }

    public void SpawnCustomer()
    {
        var customer = Instantiate(customerPrefab,spawnPoint.position,spawnPoint.rotation,transform);
        customer.seatPos=deliveryPoint.position;
        customer.leavePos=spawnPoint.position;
        CustomerQueueManager.Get.Register(customer);
    }
}

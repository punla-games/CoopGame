using UnityEngine;

public class CustomerManager:MonoBehaviour
{
    public Transform spawnPoint;
    public Transform registerPoint;
    public Transform deliveryPoint;

    public Customer customerPrefab;

    public void Update()
    {
        var customerCount = FindObjectsByType<Customer>(FindObjectsSortMode.None).Length;
        if(customerCount>0)
            return;

        SpawnCustomer();
    }

    public void SpawnCustomer()
    {
        var customer = Instantiate(customerPrefab,spawnPoint.position,spawnPoint.rotation,transform);
        customer.enterPos=registerPoint.position;
        customer.seatPos=deliveryPoint.position;
        customer.leavePos=spawnPoint.position;
    }
}

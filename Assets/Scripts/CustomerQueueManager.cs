using System.Collections.Generic;
using UnityEngine;

public class CustomerQueueManager:SingletonBehaviour<CustomerQueueManager>
{
    public Transform[] queuePoints;

    public List<Customer> queue = new();

    public void UpdateQueue() 
    {
        for(int i = 0;i<queue.Count;i++)
        {
            queue[i].queueIndex=i;
            queue[i].queuePos=queuePoints[i].position;
        }
    }

    public void Register(Customer customer)
    {
        queue.Add(customer);
        UpdateQueue();
    }

    public void Unregister(Customer customer)
    {
        queue.Remove(customer);
        UpdateQueue();
    }
}
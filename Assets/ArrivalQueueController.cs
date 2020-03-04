using Assets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrivalQueueController : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform customerSpawn;
    public bool generatingArrivals = false;

    public List<GameObject> arrivalQueue = new List<GameObject>();

    public InputField arrivalRateInput;
    private float arrivalRate; // arrivals / sec (aka lambda)

    private float prevArrivalTime;
    private float counter;
    private float currentInterArrivalTime;

    void Start()
    {
        float.TryParse(arrivalRateInput.text, out arrivalRate);
        prevArrivalTime = 0;
        counter = 0;
        currentInterArrivalTime = 0;
    }

    void Update()
    {
        float.TryParse(arrivalRateInput.text, out arrivalRate);
        counter += Time.deltaTime;
        if (counter - prevArrivalTime > currentInterArrivalTime)
        {
            currentInterArrivalTime = Utils.ExpDist(arrivalRate);
            counter = 0;
            GameObject customer = Instantiate(customerPrefab, customerSpawn.position, Quaternion.identity);
            GameObject.Find("InfoController").GetComponent<InfoController>().AddClientInQueue();
            GameObject target;
            if (arrivalQueue.Count > 0)
            {
                target = arrivalQueue[arrivalQueue.Count - 1];
            }
            else
            {
                target = GameObject.Find("Desk");
            }
            customer.GetComponent<Customer>().SetTarget(target);
            arrivalQueue.Add(customer);
        }
    }
}

using Assets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServiceController : MonoBehaviour
{
    public GameObject exit;
    public GameObject arrivalQueueController;

    public InputField serviceRateInput;
    private float serviceRate; // services / sec (aka mu)

    private float prevServiceTime;
    private float counter;
    private float currentServiceTime;

    void Start()
    {
        float.TryParse(serviceRateInput.text, out serviceRate);
        prevServiceTime = 0;
        counter = 0;
        currentServiceTime = Utils.ExpDist(serviceRate);
    }

    void Update()
    {
        float.TryParse(serviceRateInput.text, out serviceRate);
        List<GameObject> arrivalQueue = arrivalQueueController.GetComponent<ArrivalQueueController>().arrivalQueue;
        if (arrivalQueue.Count == 0)
        {
            return;
        }

        GameObject firstClient = arrivalQueue[0];

        if (firstClient.GetComponent<Customer>().ready)
        {
            counter += Time.deltaTime;
            if (counter - prevServiceTime > currentServiceTime)
            {
                currentServiceTime = Utils.ExpDist(serviceRate);
                counter = 0;
                firstClient.GetComponent<Customer>().SetTarget(GameObject.Find("Exit"));
                arrivalQueue.RemoveAt(0);
                GameObject.Find("InfoController").GetComponent<InfoController>().RemoveClientFromQueue();
                GameObject.Find("InfoController").GetComponent<InfoController>().AddServedClient();
                if (arrivalQueue.Count == 0)
                {
                    return;
                }
                arrivalQueue[0].GetComponent<Customer>().SetTarget(GameObject.Find("Desk"));
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class InfoController : MonoBehaviour
{
    Stopwatch stopWatch;

    public Text clientsInTheQueueText;
    public Text clientsServedText;
    public Text timePassedText;

    private int clientsInTheQueue = 0;
    private int clientsServed = 0;

    public void AddClientInQueue()
    {
        SetClientsInQueue(clientsInTheQueue + 1);
    }

    public void RemoveClientFromQueue()
    {
        SetClientsInQueue(clientsInTheQueue - 1);
    }

    public void SetClientsInQueue(int clients)
    {
        clientsInTheQueue = clients;
        clientsInTheQueueText.text = "Clients in the queue: " + clients;
    }

    public void AddServedClient()
    {
        SetServedClients(clientsServed + 1);
    }

    public void SetServedClients(int clients)
    {
        clientsServed = clients;
        clientsServedText.text = "Clients served: " + clients;
    }

    public void ResetTimer()
    {
        stopWatch.Stop();
        stopWatch = new Stopwatch();
        stopWatch.Start();
    }

    void Start()
    {
        stopWatch = new Stopwatch();
        stopWatch.Start();
    }

    void Update()
    {
        System.TimeSpan ts = stopWatch.Elapsed;
        string elapsedTime = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
        timePassedText.text = "Time passed: " + elapsedTime;
    }
}

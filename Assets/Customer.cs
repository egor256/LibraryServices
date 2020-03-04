using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    private GameObject target;
    private NavMeshAgent navMeshAgent;

    public bool ready;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Exit")
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = 7.0f;
        ready = false;
    }

    void Update()
    {
        if (target != null && Vector3.Distance(transform.position, target.transform.position) > 2.0f)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(target.transform.position);
        }
        else
        {
            ready = target != null && target.name == "Desk";
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.ResetPath();
        }
    }
}

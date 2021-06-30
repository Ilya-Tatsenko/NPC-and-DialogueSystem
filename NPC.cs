using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public GameObject player;
    public float distance;
    NavMeshAgent nav;
    public float radius = 40;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float k = 10.0f;
        distance = Vector3.Distance(player.transform.position, transform.position);

       
         if (distance < radius && distance >= nav.stoppingDistance)
        {
            if (distance > radius - k)
            {
                nav.enabled = true;
                gameObject.GetComponent<Animator>().SetTrigger("Start Active");
            }
            else
            {
                nav.enabled = true;
                gameObject.GetComponent<Animator>().SetTrigger("Walk");
            }
        }
        if (distance > radius)
        {
            nav.enabled = false;
            gameObject.GetComponent<Animator>().SetTrigger("Idle");
        }

        if (distance < nav.stoppingDistance)
        {
            nav.enabled = true;
            gameObject.GetComponent<Animator>().SetTrigger("Attack");
        }
 
    }
}

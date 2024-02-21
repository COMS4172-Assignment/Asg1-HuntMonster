using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.GameCenter;
using static UnityEngine.GraphicsBuffer;

public class CreatureController : MonoBehaviour
{
    [SerializeField]
    public Creature creature_type;
    public bool in_range=false;
    public GameObject angry;
    public NavMeshAgent agent;
    public float range; //radius of sphere

    public Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    public float hit_interval = 3;
    public GameObject orbsPrefab;
    public Transform orbsSpawnPoint;
    public float orbsSpeed = 10;
    float time;

    Vector3 rotate_center;
    float rotate_time=0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        angry.SetActive(false);
        rotate_center = transform.position;
    }


    void Update()
    {
        if (in_range)
        {
            agent.SetDestination(playerPoint());
            time=time+Time.deltaTime;
            if (time > hit_interval)
            {
                time = 0;
                var orbs = Instantiate(orbsPrefab, orbsSpawnPoint.position, orbsSpawnPoint.rotation);
                orbs.GetComponent<Rigidbody>().velocity = (GameScript.Instance.player.transform.position-orbsSpawnPoint.position).normalized * orbsSpeed;
            }
        }
        else
        {
            if (creature_type==Creature.LandCreature)
            {
                if (agent.remainingDistance <= agent.stoppingDistance) //done with path
                {
                    Vector3 point;
                    if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
                    {
                        Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                        agent.SetDestination(point);
                    }
                }
            }
            else
            {
                transform.RotateAround(rotate_center+Vector3.left*20, Vector3.up, 10 * Time.deltaTime);
                if(creature_type==Creature.SeaCreature) 
                {
                    rotate_time=rotate_time+Time.deltaTime;
                    if (rotate_time > 4)
                    {
                        GetComponent<Animator>().SetTrigger("flip");
                        rotate_time = 0;
                    }
                }
            }
        }

    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    Vector3 playerPoint()
    {
        Vector3 playerPoint = GameScript.Instance.player.transform.position;
        playerPoint.y = centrePoint.position.y;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(playerPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            return hit.position;
        }
        return Vector3.zero;
    }

    public void on_hit()
    {
        Destroy(gameObject);
    }

    public void on_player_enter()
    {
        in_range = true;
        angry.SetActive(true);
        time = hit_interval - 1;
    }

    public void on_player_exit()
    {
        in_range = false;
        agent.ResetPath();
        angry.SetActive(false);
    }
}

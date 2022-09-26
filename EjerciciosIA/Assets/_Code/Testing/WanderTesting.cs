using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

public class WanderTesting : MonoBehaviour
{
    public GameObject seeker;
    public GameObject targetG;
    public Vector3 randomSphere;
    public Vector3 position;
    public Vector3 target;
    public Vector3 distance;
    public Vector3 velocity;
    public Vector3 Svelocity;
    public Vector3 SFvelocity;
    public Vector3 WanderCenter;
    public Vector3 WanderDir;

    private void Awake()
    {
        randomSphere = Random.insideUnitCircle;
        position = seeker.transform.position;
        target = targetG.transform.position;
        distance = target - position;
        velocity = new Vector3(-3.2f,3.7f,0f);


        Svelocity = position + velocity;
        WanderCenter = position + (velocity.normalized * 2f);
        WanderDir = WanderCenter + (randomSphere * 3f);
    }

    private void Update()
    {
        Debug.DrawLine(Vector3.zero, position, Color.red);
        Debug.DrawLine(Vector3.zero, target, Color.blue);
        Debug.DrawLine(position, distance.normalized * (5f/2), Color.magenta);
    }
}

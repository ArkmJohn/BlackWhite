using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dikstras : MonoBehaviour {

    public static Stack<GameObject> dijkstra(GameObject[] graph, GameObject source, GameObject target)
    {
        Dictionary<GameObject, float> dist = new Dictionary<GameObject, float>();
        Dictionary<GameObject, GameObject> previous = new Dictionary<GameObject, GameObject>();
        List<GameObject> Q = new List<GameObject>();

        foreach (GameObject v in graph)
        {
            dist[v] = Mathf.Infinity; // Sets value of each object to infinity
            previous[v] = null;
            Q.Add(v); // adds the set of all nodes in the Q
        }

        dist[source] = 0;

        while (Q.Count > 0) // Find the shortest distance and remove it from the Q
        {
            float shortestDistance = Mathf.Infinity;
            GameObject shortestDistanceNode = null;
            foreach (GameObject obj in Q)
            {
                if (dist[obj] < shortestDistance)
                {
                    shortestDistance = dist[obj];
                    shortestDistanceNode = obj;
                }
            }

            GameObject u = shortestDistanceNode;
            Q.Remove(u);

            // Check to see if we made it to the target
            if (u == target)
            {
                Stack<GameObject> S = new Stack<GameObject>();

                while (previous[u] != null)
                {
                    S.Push(u);
                    u = previous[u];
                }
                return S; // Returns the shortest path
            }

            if (dist[u] == Mathf.Infinity)
            {
                break;
            }

            foreach (GameObject v in u.GetComponent<Tile>().neighbours) // Gets the neighbors
            {
                float alt = dist[u] + (u.transform.position - v.transform.position).magnitude;

                if (alt < dist[v])
                {
                    dist[v] = alt;
                    previous[v] = u;
                }
            }
        }

        return null;


    }
}

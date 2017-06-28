using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class PathRequestManager : MonoBehaviour {


    Queue<PathResult> results = new Queue<PathResult>();
    static PathRequestManager instance;
    PathFinding pathFinding;

    

    void Awake()
    {
        instance = this;
        pathFinding = GetComponent<PathFinding>();
    }

    void Update()
    {
        if (results.Count > 0)
        {
            int itemsInQueue = results.Count;
            lock (results)
            {
                for (int i = 0; i < itemsInQueue; i++)
                {
                    PathResult result = results.Dequeue();
                    result.callback(result.path, result.success);

                }
            }
        }
    }

    public static void RequestPath(PathRequest request)
    {
        ThreadStart threadStart = delegate
        {
            instance.pathFinding.FindPath(request, instance.FinishedProcessingPath);
        };

        threadStart.Invoke();
    }

    

    public void FinishedProcessingPath(PathResult pathResult)
    {        
        lock(results){
            results.Enqueue(pathResult);
        }        
    }
    
}

public struct PathResult
{
    public Vector3[] path;
    public bool success;
    public Action<Vector3[], bool> callback;

    public PathResult(Vector3[] path, bool success, Action<Vector3[], bool> callback)
    {
        this.path = path;
        this.success = success;
        this.callback = callback;
    }
}

public struct PathRequest
{
    public Vector3 pathStart;
    public Vector3 pathEnd;
    public Action<Vector3[], bool> callBack;

    public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callBack)
    {

        pathStart = _start;
        pathEnd = _end;
        callBack = _callBack;

    }
}

#region Using Statements
using System;
using System.Collections.Generic;
using UnityEngine;
#endregion

/// <summary>
/// A generic-type object pool that can create new objects if empty.
/// </summary>
/// <author>Daniel Jost</author>
public class GameObjectPool
{
    #region Fields
    private Queue<GameObject> objPool = new Queue<GameObject>();
    [SerializeField]
    private GameObject masterObj;
    [SerializeField]
    private int startingSize;
    #endregion

    #region Constructor
    /// <summary>
    /// Create an expandable pool with a number of starting elements.
    /// </summary>
    /// <param name="startingSize">The size of the pool to start.</param>
    /// <param name="theObject">The object to be used as the master clone object.</param>
    public void Awake()
    {
        //fill up the pool
        for (int i = 0; i < startingSize; ++i)
        {
            Return(GetNewObject());
        }
    }
    #endregion

    #region Functions
    /// <summary>
    /// Returns an object, either from the pool, or a new object if the pool is empty.
    /// </summary>
    /// <returns>An object of type T.</returns>
    public GameObject Get()
    {
        GameObject result;
        if (objPool.Count > 0)
        {
            result = objPool.Dequeue();
            result.gameObject.SetActive(true);
        }
        else
        {
            result = GetNewObject();
        }
        return result;
    }

    /// <summary>
    /// Adds an object into the pool.
    /// </summary>
    /// <param name="data">The object being returned to the pool.</param>
    public void Return(GameObject data)
    {
        data.gameObject.SetActive(false);
        objPool.Enqueue(data);
    }

    /// <summary>
    /// Creates a new object if the pool is empty.
    /// </summary>
    /// <returns>A new object of the type T.</returns>
    private GameObject GetNewObject()
    {
        //create new instance of the master clone object
        GameObject newObj = GameObject.Instantiate(masterObj);

        return newObj;
    }
    #endregion
}

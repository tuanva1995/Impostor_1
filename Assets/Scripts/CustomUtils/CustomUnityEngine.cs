using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

namespace CustomUtils
{
    public static class GameObjectUtils
    {

        /// <summary>
        /// Find all GameObjects have the component T - even disabled GameObjects.
        /// </summary>
        /// <typeparam name="T">name Script or Component</typeparam>
        public static List<T> FindObjectsOfTypeAll<T>()
        {
            List<T> results = new List<T>();
            SceneManager.GetActiveScene().GetRootGameObjects().ToList().ForEach(g => results.AddRange(g.GetComponentsInChildren<T>(true)));

            if (results.Count == 0)
                Debug.Log("Don't have " + typeof(T) + " object at scene");
            return results;
        }

        /// <summary>
        /// Find GameObject have the component T - even disabled GameObject.
        /// if many Gameobjects have T, only return the first Gameobject which was found.
        /// </summary>
        /// <typeparam name="T">name Script or Component</typeparam>
        public static T FindObjectsOfType<T>()
        {
            List<T> results = new List<T>();
            SceneManager.GetActiveScene().GetRootGameObjects().ToList().ForEach(g => results.AddRange(g.GetComponentsInChildren<T>(true)));

            if (results.Count == 0)
                Debug.Log("Don't have " + typeof(T) + " object at scene");

            return results[0];
        }
    }
}

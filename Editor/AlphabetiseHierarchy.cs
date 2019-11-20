using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class AlphabetiseHierarchy : EditorWindow
{
    //[Shortcut("J-Scripts/AlphabetiseHierarchy")]
    [MenuItem("J-Scripts/Alphabetise/Hierarchy")]
    public static void Alphabetise()
    {
        if (EditorUtility.DisplayDialog("Alphabetise?","Are you sure you wish to Alphabetise " + SceneManager.GetActiveScene().name + "?", "Sure am!", "Nope"))
        {
            //Create a list to store the objects in hierarchy
            List<GameObject> rootObjects = new List<GameObject>();
            //Gets all Root Objects in the hierarchy and adds them to the rootObjects List
            SceneManager.GetActiveScene().GetRootGameObjects(rootObjects);

            //Use linq to order the list A-Z
            List<GameObject> sortedList = rootObjects.OrderBy(go => go.name).ToList();

            //Order Objects A-Z
            OrderObjects(sortedList);
        }
    }

    //[Shortcut("J-Scripts/AlphabetiseChildren")]
    [MenuItem("J-Scripts/Alphabetise/Children")]
    public static void AlphabetiseChildren()
    {
        
        //If no object is selected - don't do anything
        if (Selection.activeGameObject == null)
            return;

        Transform parent = Selection.activeGameObject.transform;

        if (EditorUtility.DisplayDialog("Alphabetise?","Are you sure you wish to Alphabetise " + parent.name + "?", "Sure am!", "Nope"))
        {
            //Create a list to store the objects in hierarchy
            List<GameObject> childObjects = new List<GameObject>();

            //Find all the children of the selected gameoject and them to the child list
            foreach (Transform child in parent.transform)
                {childObjects.Add(child.gameObject);}

            //Use linq to order the list A-Z
            List<GameObject> sortedList = childObjects.OrderBy(go => go.name).ToList();

            //Order Objects A-Z - Passing the parent puts the objects back in the right place
            OrderObjects(sortedList,parent);
        }

    }

    public static void OrderObjects(List<GameObject> sortedList,Transform parent = null)
    {
        //For the undo function to work. A temp object (g) is created. Objects are added to it, then removed in alphabetical order. NB SetSibling has no way to record undo (Which is lame)
        GameObject g = new GameObject();
        foreach (GameObject gameObject in sortedList)
        {
            Undo.SetTransformParent(gameObject.transform, g.transform, "Undo Alphabetise");
            gameObject.transform.SetParent(parent);
        };
        //Remove the temp object (It's like it was never there!)
        DestroyImmediate(g);
    }
}

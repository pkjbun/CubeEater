using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIListOfObjectsManager : MonoBehaviour
{
    #region Fields And Variables
    [SerializeField] private ItemList itemList;
    [SerializeField] ButtonCube cubeButtonPrefab;
    [SerializeField] Transform content;
    [SerializeField] List<ButtonCube> cubeList;
    #endregion
#region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        foreach(var item in itemList.Items) { 
             ButtonCube cubeItem=  Instantiate(cubeButtonPrefab,content);
            cubeItem.Setup(item);
            cubeList.Add(cubeItem);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
#endregion
#region Custom Methods
#endregion
}

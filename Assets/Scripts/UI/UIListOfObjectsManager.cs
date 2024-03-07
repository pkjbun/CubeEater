using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIListOfObjectsManager : MonoBehaviour
{
    #region Fields And Variables
    [SerializeField] private ItemList itemList;
    [SerializeField] ButtonCube cubeButtonPrefab;
    [SerializeField] Transform content;
    [SerializeField] List<ButtonCube> cubeList;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] UIAnimation uIAnimation;
    public UnityEvent<ButtonCube> buttonCubeEvent=new UnityEvent<ButtonCube>();
    public List<ButtonCube> CubeList { get => cubeList; }
    #endregion
    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        foreach(var item in itemList.Items) { 
            ButtonCube cubeItem=  Instantiate(cubeButtonPrefab,content);
            cubeItem.Setup(item);
            cubeItem.GetComponent<Button>()?.onClick.AddListener(() => OnItemClicked(cubeItem));
            cubeList.Add(cubeItem);
        }
        if(inputField != null)
        {
            inputField.onValueChanged.AddListener(Filter);
        }
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
#endregion
#region Custom Methods   
    /// <summary>
    /// Handles Button Clicked
    /// </summary>
    /// <param name="cubeItem">clicked ButtonCube</param>
    private void OnItemClicked(ButtonCube cubeItem)
    {
        uIAnimation.Animate();
        buttonCubeEvent?.Invoke(cubeItem);
    }
    /// <summary>
    /// Filters list basing on Input String
    /// </summary>
    /// <param name="text"></param>
    public void Filter(string text)
    {
        if(string.IsNullOrEmpty(text))
        {
            SetActiveAllItemsInList(cubeList, true);
            return;
        }
        SetActiveAllItemsInList(cubeList, false);
        SetActiveAllItemsInList( FilterCubes(cubeList, text), true);

    }
    /// <summary>
    /// Change active state of list of ButtonCubes
    /// </summary>
    /// <param name="list">List of Cubes</param>
    /// <param name="active">Set Active Game Object?</param>
    private void SetActiveAllItemsInList(List<ButtonCube> list, bool active)
    {
        foreach (ButtonCube bc in list)
        {
            bc.gameObject.SetActive(active);
        }
    }
    /// <summary>
    /// Returns List of ButtonCube that matches input string
    /// </summary>
    /// <param name="cubes">List of ButtonCubes</param>
    /// <param name="input">Input string used for filtering</param>
    /// <returns>Returns List of ButtonCube that matches input string</returns>
    public List<ButtonCube> FilterCubes(List<ButtonCube> cubes, string input){   
        return cubes.Where(cube => cube.GetItemName().IndexOf(input, StringComparison.InvariantCulture) >= 0).ToList();
    }
    #endregion
}

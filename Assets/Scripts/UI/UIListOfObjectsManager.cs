using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class UIListOfObjectsManager : MonoBehaviour
{
    #region Fields And Variables
    [SerializeField] private ItemList itemList;
    [SerializeField] ButtonCube cubeButtonPrefab;
    [SerializeField] Transform content;
    [SerializeField] List<ButtonCube> cubeList;
    [SerializeField] TMP_InputField inputField;
    public UnityEvent<ISpawnableButton> buttonCubeEvent=new UnityEvent<ISpawnableButton>();
    public List<ButtonCube> CubeList { get => cubeList; }
    [Inject]
    private ObjectSpawner _objectSpawner;
    [Inject]
    private UIAnimation _uiAnimation;
    [Inject]
    private ButtonCube.Factory _buttonCubeFactory;
    #endregion
    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        foreach(var item in itemList.Items)
        {
            CreateButton(item);
        }
        if (inputField != null)
        {
            inputField.onValueChanged.AddListener(Filter);
        }
    }



    void Update()
    {
        
    }
#endregion
#region Custom Methods     
    private void CreateButton(ItemData item)
    {
        ButtonCube cubeItem = _buttonCubeFactory.Create(item, content);
        cubeItem.transform.localScale = Vector3.one;
        cubeItem.GetComponent<Button>()?.onClick.AddListener(() => OnItemClicked(cubeItem));
      //  cubeItem.OnActionFinished.AddListener(OnSpawnFinished);
        cubeList.Add(cubeItem);
    } 
    /// <summary>
    /// Called after actionIsFinished or canceled
    /// </summary>
    public void OnSpawnFinished()
    {
        _uiAnimation?.MoveToOriginalPosition();
    }
    /// <summary>
    /// Handles Button Clicked
    /// </summary>
    /// <param name="item">clicked button</param>
    private void OnItemClicked(ISpawnableButton item)
    {
        _uiAnimation?.Animate();
        _objectSpawner.SpawnStart(item);
        buttonCubeEvent?.Invoke(item);
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
        return cubes.Where(cube => cube.GetItemName().IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
    }
    #endregion
}

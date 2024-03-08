using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class ButtonCube : MonoBehaviour, ISpawnableButton
{
    #region Fields And Variables
    [SerializeField] private TextMeshProUGUI buttonTitle;
    [SerializeField] private Image image;
    ItemData _itemData;
    public UnityEvent OnActionFinished = new UnityEvent();
    #endregion
    #region Unity Methods

    #endregion
    #region Custom Methods
    /// <summary>
    /// Zenject construct method
    /// </summary>
    /// <param name="itemData"></param>
    /// <param name="transform"></param>
    [Inject]
    public void Construct(ItemData itemData, Transform transform)
    {
        _itemData = itemData;
        gameObject.transform.SetParent ( transform);
        SetProperties();
    }
    private void SetProperties()
    {   if (_itemData == null) return;
        buttonTitle.text=_itemData.title;
        image.sprite = _itemData.thumbnail;
    }
    /// <summary>
    /// Get tile of object from object data
    /// </summary>
    /// <returns></returns>
    public string GetItemName()
    {   
      string title= _itemData?  _itemData.name :  "";
        return title;
    }

    public void OnClickToSpawn()
    {
        OnActionFinished?.Invoke();
    }

    public GameObject GetObjectToSpawn()
    {
        if(_itemData == null) return null;
        else
        {
           return _itemData.cubePrefab;
        }
    }
    public class Factory : PlaceholderFactory<ItemData, Transform, ButtonCube>
    {
    }

    #endregion
}

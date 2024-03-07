using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonCube : MonoBehaviour, ISpawnableButton
{
    #region Fields And Variables
    [SerializeField] private TextMeshProUGUI buttonTitle;
    [SerializeField] private Image image;
    [SerializeField] ItemData itemData;
    public UnityEvent OnActionFinished = new UnityEvent();
    #endregion
#region Unity Methods
    
#endregion
#region Custom Methods
    public void Setup(ItemData ItemData)
    {
        itemData = ItemData;
        SetProperties();
    }

    private void SetProperties()
    {   if (itemData == null) return;
        buttonTitle.text=itemData.title;
        image.sprite = itemData.thumbnail;
    }
    /// <summary>
    /// Get tile of object from object data
    /// </summary>
    /// <returns></returns>
    public string GetItemName()
    {   
      string title= itemData?  itemData.name :  "";
        return title;
    }

    public void OnClickToSpawn()
    {
        OnActionFinished?.Invoke();
    }

    public GameObject GetObjectToSpawn()
    {
        if(itemData == null) return null;
        else
        {
           return itemData.cubePrefab;
        }
    }
    #endregion
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCube : MonoBehaviour
{
    #region Fields And Variables
    [SerializeField] private TextMeshProUGUI buttonTitle;
    [SerializeField] private Image image;
    [SerializeField] ItemData itemData;

    #endregion
#region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
    #endregion
}


using UnityEngine;
using DG.Tweening;
public class UIAnimation : MonoBehaviour
{
    #region Fields And Variables

    [SerializeField] private RectTransform uiElement;
    [SerializeField] private RectTransform rectPosition1;
    [SerializeField] private RectTransform rectPosition2;
    [SerializeField] private float duration = 2f;
    private Vector2 position1;
    private Vector2 position2;

    public RectTransform UiElement { get => uiElement; }
    public RectTransform RectPosition1 { get => rectPosition1;}
    public RectTransform RectPosition2 { get => rectPosition2; }
    public float Duration { get => duration; }

    #endregion
    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        SetPostionsFromRect();
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
    #region Custom Methods
    /// <summary>
    /// To set UI Element and Rects form script
    /// </summary>
    /// <param name="objRectTransform"></param>
    /// <param name="pos1RectTransform"></param>
    /// <param name="pos2RectTransform"></param>
    public void SetRects(RectTransform objRectTransform, RectTransform pos1RectTransform, RectTransform pos2RectTransform)
    {
        uiElement=objRectTransform;
        rectPosition1 = pos1RectTransform;
        rectPosition2 = pos2RectTransform;
    }
    /// <summary>
    /// Use to Set Postions values basing on Rects
    /// </summary>
    public void SetPostionsFromRect()
    {   
       if(rectPosition1) position1 =  rectPosition1.anchoredPosition;
       if(rectPosition2)  position2 = rectPosition2.anchoredPosition;
    }

    /// <summary>
    /// Animate to the target position - position2
    /// </summary>
    [ContextMenu("Hide")]
    public void Animate()
    { 
        uiElement.DOAnchorPos(position2, duration); 
    }
    /// <summary>
    /// // Call this method to move the UI element back to its original position - pos1
    /// </summary>
    [ContextMenu("Show")]
    public void MoveToOriginalPosition()
    {
        uiElement.DOAnchorPos(position1, duration);
    }
    /// <summary>
    /// Can be used to move object to custom anchored postion
    /// </summary>
    /// <param name="newPosition">New custom anchor position </param>
    public void MoveToDefinedPosition(Vector2 newPosition)
    {
        uiElement.DOAnchorPos(newPosition, duration);
    }
    #endregion
}

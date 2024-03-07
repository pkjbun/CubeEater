
using UnityEngine;
using DG.Tweening;
public class UIAnimation : MonoBehaviour
{
    #region Fields And Variables

    [SerializeField] private RectTransform uiElement;
    [SerializeField] private RectTransform RectPosition1;
    [SerializeField] private RectTransform RectPosition2;
    [SerializeField] private float Duration = 2f;
    private Vector2 position1;
    private Vector2 position2;

    #endregion
    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        position1 = RectPosition1.anchoredPosition;
        position2 = RectPosition2.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
    #region Custom Methods
    /// <summary>
    /// Animate to the target position - position2
    /// </summary>
    [ContextMenu("Hide")]
    public void Animate()
    { 
        uiElement.DOAnchorPos(position2, Duration); 
    }
    /// <summary>
    /// // Call this method to move the UI element back to its original position - pos1
    /// </summary>
    [ContextMenu("Show")]
    public void MoveToOriginalPosition()
    {
        uiElement.DOAnchorPos(position1, Duration);
    }
    /// <summary>
    /// Can be used to move object to custom anchored postion
    /// </summary>
    /// <param name="newPosition">New custom anchor position </param>
    public void MoveToDefinedPosition(Vector2 newPosition)
    {
        uiElement.DOAnchorPos(newPosition, Duration);
    }
    #endregion
}

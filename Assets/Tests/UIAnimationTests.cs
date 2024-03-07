using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
public class UIAnimationTests
{
    
    [UnityTest]
 
    public IEnumerator AnimateMovesUIElementToPosition2()
    {
        // Setup the scene and objects
        var gameObject = new GameObject();
        var rectTransform = gameObject.AddComponent<RectTransform>();
        var uiAnimation = gameObject.AddComponent<UIAnimation>();
        uiAnimation.SetRects(rectTransform, new GameObject().AddComponent<RectTransform>(), new GameObject().AddComponent<RectTransform>());
        uiAnimation.RectPosition1.anchoredPosition = Vector2.zero; // Example starting position
        uiAnimation.RectPosition2.anchoredPosition = new Vector2(100, 100); // Example target position
        uiAnimation.SetPostionsFromRect();
        // Call the method to test
        uiAnimation.Animate();

        // Wait for the duration of the animation
        yield return new WaitForSeconds(uiAnimation.Duration);

        // Check the result
        // 0.01f is the tolerance
        Assert.AreEqual(uiAnimation.RectPosition2.anchoredPosition.x, uiAnimation.UiElement.anchoredPosition.x, 0.01f); 
        Assert.AreEqual(uiAnimation.RectPosition2.anchoredPosition.y, uiAnimation.UiElement.anchoredPosition.y, 0.01f);
    }
}

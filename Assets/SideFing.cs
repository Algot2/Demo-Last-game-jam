using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class SideFing : MonoBehaviour
{
    public RectTransform targetRect;

    Button currentButton;

    void Update()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        currentButton = null;
        Debug.Log(results.Count);
        
        foreach (RaycastResult result in results) {
            Button b = result.gameObject.GetComponent<Button>();

            if (b != null) {
                currentButton = b;
                break;
            }
        }

        transform.GetChild(0).gameObject.SetActive(currentButton != null);
        transform.GetChild(1).gameObject.SetActive(currentButton != null);

        if (currentButton != null) {   
            targetRect.position = currentButton.transform.position;
        }
    }
}

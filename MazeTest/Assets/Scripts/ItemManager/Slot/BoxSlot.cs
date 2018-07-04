using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoxSlot :Slot,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
{
    private BoxPanel curBoxPanel;
    private GameObject selectUI;
    private bool isSelected;

    void Awake()
    {
        base.Awake();
        curBoxPanel = transform.GetComponentInParent<BoxPanel>();
        selectUI = transform.Find("Frame").gameObject;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {

    }
    public override void OnDrag(PointerEventData eventData)
    {

    }
    public override void OnEndDrag(PointerEventData eventData)
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerId != -1)
        {
            return;
        }
        if (isSelected)
        {
            isSelected = false;
            curBoxPanel.RemoveSlot(this);;
            selectUI.SetActive(false);
        }
        else
        {
            isSelected = true;
            curBoxPanel.SelectSlot(this);
            selectUI.SetActive(true);
        }
    }
    public void Fresh()
    {
        isSelected = false;
        selectUI.SetActive(false);
        if (CurItem == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

}

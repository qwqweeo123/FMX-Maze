using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemInfoPanel : BasePanel
{
    private Text nameText;
    private Text desText;

    void Awake()
    {
        base.Awake();
        nameText = transform.Find("NameText").GetComponent<Text>();
        desText = transform.Find("DescriptionText").GetComponent<Text>();
    }
    public override void OnEnter(object param)
    {
        Item item = param as Item;
        nameText.text = item.Name;
        desText.text = item.Description;
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        SetDisplay(true);
        transform.SetAsLastSibling();
    }
    public override void OnExit()
    {
        SetDisplay(false);
    }
}

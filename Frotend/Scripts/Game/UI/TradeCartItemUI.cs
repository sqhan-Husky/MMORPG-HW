using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TradeCartItemUI : MonoBehaviour
{
	public Button button;
	public Text textCost;
	public Text textName;

	public string itemName;
    public int itemId;//new

	public Text textSc;
	public Text textGc;

	public Text SCurrentCost;

	void Awake()
	{
	}

	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Init(int pi_id)//new
	{
        //Debug.Log("cart pi_id:"+pi_id);
        string item_name = PlayerAttribute.trade_item[pi_id].name;
		Sprite sprite;
		if (button == null || textCost == null || textCost == null)
		{
			return;
		}
		if (!GetAllIcons.icons.TryGetValue(item_name, out sprite))
		{
			return;
		}
		itemName = item_name;
		button.image.sprite = sprite;
		button.onClick.AddListener(delegate ()
		{
            //GameObject.FindObjectOfType<TradeCartGridUI>().RemoveFromCart(pi_id);
            TradeCartGridUI gridHandler = transform.parent.GetComponent<TradeCartGridUI>();
            if (gridHandler != null)
            {
                gridHandler.RemoveFromCart(pi_id);
            }
        });
		textName.text = itemName;
		textCost.text = PlayerAttribute.trade_item[pi_id].s_coin.ToString();
		PlayerAttribute.sTradecurrentCost += PlayerAttribute.trade_item[pi_id].s_coin;
        SCurrentCost.text = string.Format("银币当前总额: ${0}", PlayerAttribute.sTradecurrentCost.ToString());
	}

	public void AfterBuy()
	{
		textSc.text = PlayerAttribute.s_coin.ToString();
		SCurrentCost.text = string.Format("银币当前总额: ${0}", PlayerAttribute.sTradecurrentCost.ToString());
	}
}


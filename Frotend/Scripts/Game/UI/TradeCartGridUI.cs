using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Gamekit3D.Network;


public class TradeCartGridUI : MonoBehaviour
{
	public GameObject TradeCartItem;
	public Text textSc;
	public Text textGc;
	public Text SCurrentCost;
	//public Text textname;

	public Dictionary<int, GameObject> t_items = new Dictionary<int, GameObject>();


	private void Awake()
	{
		TradeCartItem.SetActive(false);
	}
	// Use this for initialization

	void Start()
	{
		textSc.text = PlayerAttribute.s_coin.ToString();
		textGc.text = PlayerAttribute.g_coin.ToString();
		SCurrentCost.text = string.Format("银币当前总额: ${0}", PlayerAttribute.sTradecurrentCost);
	}


	// Update is called once per frame
	void Update()
	{

	}

	public void AddToCart(int pi_id)//new
	{
		Sprite sprite;
		GameObject item;
		string item_name = PlayerAttribute.trade_item[pi_id].name;

		if (!GetAllIcons.icons.TryGetValue(item_name, out sprite))
		{
			return;
		}
		bool exists = t_items.TryGetValue(pi_id, out item);

		if (!exists)
		{
			item = GameObject.Instantiate(TradeCartItem);
			if (item == null)
			{
				return;
			}
			item.transform.SetParent(transform, false);
			item.SetActive(true);
			t_items.Add(pi_id, item);
			TradeCartItemUI handler = item.GetComponent<TradeCartItemUI>();
			if (handler == null)
			{
				return;
			}
			handler.Init(pi_id);
		}
		else
		{
			//告诉用户该商品已加入购物车
		}
		/*if (exists)
		{
			handler.Increase(name);
		}
		else
		{
			handler.Init(name);
		}*/
	}

	public void RemoveFromCart(int pi_id)
	{
		GameObject item;
		if (t_items.TryGetValue(pi_id, out item))
		{
			t_items.Remove(pi_id);
			Destroy(item);
		}
		PlayerAttribute.sTradecurrentCost -= PlayerAttribute.trade_item[pi_id].s_coin;
		SCurrentCost.text = string.Format("银币当前总额: ${0}", PlayerAttribute.sTradecurrentCost.ToString());
	}

	public void OnSBuyButtonClicked()
	{
		GameObject item;
		CTradeBuy request = new CTradeBuy();
		request.m_items = new Dictionary<int, int>();

		foreach (var kv in t_items)
		{
			item = kv.Value;
			//CartItemUI handler = item.GetComponent<CartItemUI>();
			request.m_items.Add(kv.Key, PlayerAttribute.trade_item[kv.Key].s_coin);
		}

		request.sum_cost = PlayerAttribute.sTradecurrentCost;
		if (request.m_items.Count > 0)
		{
			Client.Instance.Send(request);
		}
	}

	public void AfterBuy()
	{
		foreach (var kv in t_items)
		{
			TradeCartItemUI handler = kv.Value.GetComponent<TradeCartItemUI>();
			TradeShelfGridUI Tradeshelf = GameObject.FindObjectOfType<TradeShelfGridUI>();
			Tradeshelf.items[kv.Key].GetComponent<TradeShelfItemUI>().textOwner.text = string.Format("售罄");
			Tradeshelf.items[kv.Key].GetComponent<TradeShelfItemUI>().button.interactable = false;
			//Destroy(Tradeshelf.items[kv.Key]);
		}
		textSc.text = PlayerAttribute.s_coin.ToString();
		textGc.text = PlayerAttribute.g_coin.ToString();
		SCurrentCost.text = string.Format("银币当前总额: ${0}", PlayerAttribute.sTradecurrentCost.ToString());
		foreach (var kv in t_items)
		{
			Destroy(kv.Value);
		}
		t_items.Clear();
	}
}


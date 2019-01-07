using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Gamekit3D.Network;

public class CartGridUI : MonoBehaviour
{
    public GameObject CartItem;
	public Text textSc;
	public Text textGc;
	public Text GCurrentCost;
	public Text SCurrentCost;
	public Text textCost;

	private Dictionary<string, GameObject> m_items = new Dictionary<string, GameObject>();
	

	private void Awake()
    {
        CartItem.SetActive(false);
    }
    // Use this for initialization
    void Start()
    {
		textSc.text = PlayerAttribute.s_coin.ToString();
		textGc.text = PlayerAttribute.g_coin.ToString();
		GCurrentCost.text = string.Format("金币当前总额: ${0}", PlayerAttribute.gcurrentCost);
		SCurrentCost.text = string.Format("银币当前总额: ${0}", PlayerAttribute.scurrentCost);
	}


	// Update is called once per frame
	void Update()
    {

    }

    public void AddToCart(string name)
    {
        Sprite sprite;
        GameObject item;
        if (!GetAllIcons.icons.TryGetValue(name, out sprite))
        {
            return;
        }
        bool exists = m_items.TryGetValue(name, out item);

        if (!exists)
        {
            item = GameObject.Instantiate(CartItem);
            if (item == null)
            {
                return;
            }
            item.transform.SetParent(transform, false);
            item.SetActive(true);
            m_items.Add(name, item);
        }
        CartItemUI handler = item.GetComponent<CartItemUI>();
        if (handler == null)
        {
            return;
        }

        if (exists)
        {
            handler.Increase(name);
        }
        else
        {
            handler.Init(name);
        }
    }

    public void RemoveFromCart(string name)
    {
        GameObject item;
        if (m_items.TryGetValue(name, out item))
        {
            m_items.Remove(name);
            Destroy(item);
        }
    }

    public void OnSBuyButtonClicked()
    {
		GameObject item;
		CBuyItems msg = new CBuyItems();
		msg.m_items = new Dictionary<string, int>();
		foreach (var kv in m_items)
		{
			item = kv.Value;
			CartItemUI handler = item.GetComponent<CartItemUI>();
			msg.m_items.Add(kv.Key, handler.count);
		}
		msg.gold_or_silver = "silver";
		msg.sum_cost = PlayerAttribute.scurrentCost;
		if (msg.m_items.Count > 0)
		{
			Client.Instance.Send(msg);
		}
	}

	public void OnGBuyButtonClicked()
	{
		GameObject item;
		CBuyItems msg = new CBuyItems();
		msg.m_items = new Dictionary<string, int>();
		foreach (var kv in m_items)
		{
			item = kv.Value;
			CartItemUI handler = item.GetComponent<CartItemUI>();
			msg.m_items.Add(kv.Key, handler.count);
		}
		msg.gold_or_silver = "golden";
		msg.sum_cost = PlayerAttribute.gcurrentCost;
		if (msg.m_items.Count > 0)
		{
			Client.Instance.Send(msg);
		}
	}

	public void AfterBuy()
	{
		foreach (var kv in m_items)
		{
			CartItemUI handler = kv.Value.GetComponent<CartItemUI>();
			ShelfGridUI shelf = GameObject.FindObjectOfType<ShelfGridUI>();
			shelf.items[kv.Key].GetComponent<ShelfItemUI>().textCost.text = string.Format("拥有{0}件", handler.count);
			//textCost.text = string.Format("拥有{0}件", handler.count);
		}
		textSc.text = PlayerAttribute.s_coin.ToString();
		textGc.text = PlayerAttribute.g_coin.ToString();
		GCurrentCost.text = string.Format("金币当前总额: ${0}", PlayerAttribute.gcurrentCost.ToString());
		SCurrentCost.text = string.Format("银币当前总额: ${0}", PlayerAttribute.scurrentCost.ToString());
		foreach (var kv in m_items)
		{
			//CartItemUI handler = kv.Value.GetComponent<CartItemUI>();
			//textCost.text = string.Format("拥有{0}件", handler.count);
			Destroy(kv.Value);
		}
		m_items.Clear();
	}
}

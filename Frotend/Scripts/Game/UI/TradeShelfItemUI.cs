using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TradeShelfItemUI : MonoBehaviour
{
    public int itemId;
	public string itemName;
	public GameObject cartContent;

	public GameObject ItemDetailsView;
	public GameObject GetGoldCoinsView;
	public Button button;
	public Button detailbutton;
	public Text textName;
	public Text textOwner;
	TradeCartGridUI handler;
	public Text ItemName;
	public Text ItemType;
	public Text ItemAttr;
	public Text ItemValue;
	public Text SilverCost;
	public Text Smarket;
	public Text Gmarket;
	public Text Owner;


	private void Awake()
	{
		if (cartContent != null)
		{
			handler = cartContent.GetComponent<TradeCartGridUI>();
		}
	}
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Init(int pi_id)//new
	{
		itemName = PlayerAttribute.trade_item[pi_id].name;
        itemId = pi_id;

        //Debug.Log("in trade "+ pi_id);
		Sprite sprite;
		if (button == null || textName == null || textOwner == null)
		{
			return;
		}
		if (!GetAllIcons.icons.TryGetValue(itemName, out sprite))
		{
			return;
		}
		button.image.sprite = sprite;
		detailbutton.onClick.AddListener(delegate ()
		{
			OnClickItem(pi_id);
		});
		textName.text = itemName;
		int count = 0;
		foreach (var kv in PlayerAttribute.m_item)
		{
			if (Equals(kv.Value.name, name))
			{
				count++;
			}
		}
		textOwner.text = string.Format("拥有{0}件", count.ToString());
	}

	public void AddToCart()//new
	{
		if (handler != null)
			handler.AddToCart(itemId);
	}

	public void OnClickItem(int pi_id)//new
	{
		ItemDetailsView.SetActive(true);
        itemName = PlayerAttribute.trade_item[pi_id].name;
		GameObject.Find("tItemImg").GetComponent<Image>().sprite = GetAllIcons.icons[itemName];
        Owner.text = string.Format("持有者：{0}", PlayerAttribute.trade_item[pi_id].owner_name.ToString()); ;
        ItemName.text = itemName;
        ItemType.text = string.Format("种类：{0}", PlayerAttribute.trade_item[pi_id].atype.ToString());
		ItemAttr.text = string.Format("属性：{0}", PlayerAttribute.trade_item[pi_id].mtype.ToString());
		ItemValue.text = string.Format("数值：{0}", PlayerAttribute.trade_item[pi_id].value.ToString());
        SilverCost.text = string.Format("银币：${0}", PlayerAttribute.trade_item[pi_id].s_coin.ToString());
		Gmarket.text = string.Format("${0}", PlayerAttribute.market_item[itemName].g_coin.ToString());
		Smarket.text = string.Format("${0}", PlayerAttribute.market_item[itemName].s_coin.ToString());
    }
}


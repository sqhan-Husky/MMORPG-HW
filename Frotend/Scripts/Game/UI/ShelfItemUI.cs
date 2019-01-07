using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShelfItemUI : MonoBehaviour
{
    public string itemName;
    public GameObject cartContent;

	public GameObject ItemDetailsView;
	public GameObject GetGoldCoinsView;
	public Button button;
	public Button detailbutton;
	public Text textName;
    public Text textCost;
    CartGridUI handler;
	public Text ItemName;
	public Text ItemType;
	public Text ItemAttr;
	public Text ItemValue;
	public Text SilverCost;
	public Text GoldCost;


	private void Awake()
    {
        if (cartContent != null)
        {
            handler = cartContent.GetComponent<CartGridUI>();
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

    public void Init(string name)
    {
        itemName = name;
		Debug.Log(name);
        Sprite sprite;
        if (button == null || textName == null || textCost == null)
        {
            return;
        }
        if (!GetAllIcons.icons.TryGetValue(name, out sprite))
        {
            return;
        }
		button.image.sprite = sprite;
		detailbutton.onClick.AddListener(delegate ()
		{
			OnClickItem(name);
		});
		textName.text = name;
		int count = 0;
		foreach (var kv in PlayerAttribute.m_item)
		{
			if (Equals(kv.Value.name, name)){
				count++;
			}
		}
		textCost.text = string.Format("拥有{0}件",count.ToString());
	}

    public void AddToCart()
    {
        if (handler != null)
            handler.AddToCart(itemName);
    }

	public void OnClickItem(string name)
	{
		//Debug.Log("111");
		//GameObject cloned = GameObject.Instantiate(ItemDetailsView);
		//cloned.SetActive(true);
		//cloned.transform.SetParent(this.transform, false);
		ItemDetailsView.SetActive(true);

		//Debug.Log("222");
		GameObject.Find("ItemImg").GetComponent<Image>().sprite = GetAllIcons.icons[name];
		ItemName.text = name;
		ItemType.text = string.Format("种类：{0}",PlayerAttribute.market_item[name].atype.ToString());
		ItemAttr.text = string.Format("属性：{0}", PlayerAttribute.market_item[name].mtype.ToString());
		ItemValue.text = string.Format("数值：{0}", PlayerAttribute.market_item[name].value.ToString());
		SilverCost.text = string.Format("银币：${0}", PlayerAttribute.market_item[name].s_coin.ToString());
		GoldCost.text = string.Format("金币：${0}", PlayerAttribute.market_item[name].g_coin.ToString());
	}
}

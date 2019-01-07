using System;
using UnityEngine;
using UnityEngine.UI;

public class CartItemUI : MonoBehaviour
{
    public Button button;
    public Text textCost;
    public InputField inputCount;
    public int count = 0;
    string itemName;

	public Button increaseButton;
	public Button decreaseButton;

	public Text textSc;
	public Text textGc;
	public Text GCurrentCost;
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

    public void Init(string name)
    {
        Sprite sprite;
        if (button == null || textCost == null || textCost == null)
        {
            return;
        }
        if (!GetAllIcons.icons.TryGetValue(name, out sprite))
        {
            return;
        }
        itemName = name;
		Debug.Log(itemName);
        count++;
        button.image.sprite = sprite;
        inputCount.text = System.Convert.ToString(count);
        textCost.text = itemName;

		PlayerAttribute.gcurrentCost += PlayerAttribute.market_item[itemName].g_coin;
		GCurrentCost.text = string.Format("金币当前总额: ${0}", PlayerAttribute.gcurrentCost.ToString());

		PlayerAttribute.scurrentCost += PlayerAttribute.market_item[itemName].s_coin;
		SCurrentCost.text = string.Format("银币当前总额: ${0}", PlayerAttribute.scurrentCost.ToString());

		increaseButton.onClick.AddListener(delegate ()
		{
			Increase(itemName);              
		});

		decreaseButton.onClick.AddListener(delegate ()
		{
			Decrease(itemName);
		});

	}

    public void Increase(string name)
    {
        count++;

        inputCount.text = System.Convert.ToString(count);
		textCost.text = name;
		PlayerAttribute.gcurrentCost += PlayerAttribute.market_item[name].g_coin;
		GCurrentCost.text = string.Format("金币当前总额: ${0}", PlayerAttribute.gcurrentCost.ToString());

		PlayerAttribute.scurrentCost += PlayerAttribute.market_item[name].s_coin;
		SCurrentCost.text = string.Format("银币当前总额: ${0}", PlayerAttribute.scurrentCost.ToString());

	}

    public void Decrease(string name)
    {
        count--;

		PlayerAttribute.gcurrentCost -= PlayerAttribute.market_item[name].g_coin;
		GCurrentCost.text = string.Format("金币当前总额: ${0}", PlayerAttribute.gcurrentCost.ToString());

		PlayerAttribute.scurrentCost -= PlayerAttribute.market_item[name].s_coin;
		SCurrentCost.text = string.Format("银币当前总额: ${0}", PlayerAttribute.scurrentCost.ToString());

		if (count == 0)
        {
            if (transform.parent == null)
            {
                return;
            }
            CartGridUI gridHandler = transform.parent.GetComponent<CartGridUI>();
            if (gridHandler != null)
            {
                gridHandler.RemoveFromCart(itemName);
            }
        }
        else
        {
            inputCount.text = System.Convert.ToString(count);
			textCost.text = name;
        }
    }

	public void AfterBuy()
	{
		textSc.text = PlayerAttribute.s_coin.ToString();
		textGc.text = PlayerAttribute.s_coin.ToString();
		GCurrentCost.text = string.Format("金币当前总额: ${0}", PlayerAttribute.gcurrentCost.ToString());
		SCurrentCost.text = string.Format("银币当前总额: ${0}", PlayerAttribute.scurrentCost.ToString());
	}
}

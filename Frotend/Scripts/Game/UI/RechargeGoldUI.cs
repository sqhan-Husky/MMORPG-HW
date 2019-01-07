using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Gamekit3D.Network;

public class RechargeGoldUI : MonoBehaviour
{
	public InputField GoldNum;
	public Button rechargeSuccess;
	

	private void Awake()
	{
		
	}
	private void OnEnable()
	{
		rechargeSuccess.onClick.AddListener(delegate ()
		{
			RefreshCoin();
		});
	}

	public void RefreshCoin()
	{
		var input = GameObject.Find("moneyInputField");
		if (input != null)
		{
			int goldnum = Convert.ToInt32(input.GetComponent<InputField>().text);
			PlayerAttribute.g_coin += goldnum;
			CartGridUI cart = GameObject.FindObjectOfType<CartGridUI>();
			cart.GetComponent<CartGridUI>().textGc.text = PlayerAttribute.g_coin.ToString();
			//GameObject.Find("moneyInputField").GetComponent<InputField>().text = 0.ToString();

			CAddCoin msg = new CAddCoin();
			msg.gold_or_silver = "gold";
			msg.num = PlayerAttribute.g_coin;

			Client.Instance.Send(msg);
			GameObject.Find("GetGoldCoinsView").SetActive(false);
		}
	}




}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Gamekit3D.Network;

public class RechargeSilverUI : MonoBehaviour
{
	public InputField Num;
	public Button rechargeSSuccess;


	private void Awake()
	{

	}
	private void OnEnable()
	{
		rechargeSSuccess.onClick.AddListener(delegate ()
		{
			RefreshCoin();
		});
	}

	public void RefreshCoin()
	{
		var input = GameObject.Find("GmoneyInputField");
		if (input != null)
		{
			int addnum = Convert.ToInt32(input.GetComponent<InputField>().text);
			PlayerAttribute.s_coin += addnum * 2;
			PlayerAttribute.g_coin -= addnum;
			CartGridUI cart = GameObject.FindObjectOfType<CartGridUI>();
			cart.GetComponent<CartGridUI>().textGc.text = PlayerAttribute.g_coin.ToString();
			cart.GetComponent<CartGridUI>().textSc.text = PlayerAttribute.s_coin.ToString();
			//GameObject.Find("GmoneyInputField").GetComponent<InputField>().text = 0.ToString();

			CAddCoin msg = new CAddCoin();
			msg.gold_or_silver = "silver";
			msg.num = addnum;

			Client.Instance.Send(msg);
			GameObject.Find("GetSilverCoinsView").SetActive(false);
		}
	}




}

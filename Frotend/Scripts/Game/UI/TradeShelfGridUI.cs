using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Gamekit3D;
public class TradeShelfGridUI : MonoBehaviour
{
	public GameObject TradeShelfItem;

	public Dictionary<int, GameObject> items = new Dictionary<int, GameObject>();

	private void Awake()
	{

	}

    // Use this for initialization
    void Start()//new
    {
		/*
        foreach (KeyValuePair<int, M_Item> kv in PlayerAttribute.trade_item)
        {
            int pi_id = kv.Key;
            GameObject cloned = GameObject.Instantiate(TradeShelfItem);
            if (cloned == null)
            {
                continue;
            }
            cloned.SetActive(true);
            cloned.transform.SetParent(this.transform, false);
            items.Add(pi_id, cloned);

            TradeShelfItemUI handler = cloned.GetComponent<TradeShelfItemUI>();
            if (handler == null)
            {
                continue;
            }
            handler.Init(pi_id);
        }
        TradeShelfItem.SetActive(false);
		*/
		TradeShelfItem.SetActive(false);
	}

	private void OnEnable()
	{
		items = new Dictionary<int, GameObject>();
		foreach (KeyValuePair<int, M_Item> kv in PlayerAttribute.trade_item)
		{
			int pi_id = kv.Key;
			if (!Equals(PlayerAttribute.trade_item[pi_id].owner_name.ToString(), PlayerAttribute.user))
			{
				GameObject cloned = GameObject.Instantiate(TradeShelfItem);
				if (cloned == null)
				{
					continue;
				}
				cloned.SetActive(true);
				cloned.transform.SetParent(this.transform, false);
				items.Add(pi_id, cloned);

				TradeShelfItemUI handler = cloned.GetComponent<TradeShelfItemUI>();
				if (handler == null)
				{
					continue;
				}
				handler.Init(pi_id);
			}
		}
		//TradeShelfItem.SetActive(false);
		PlayerMyController.Instance.EnabledWindowCount++;
	}

	private void OnDisable()
	{
		foreach (Transform tf in this.transform)
		{
			Destroy(tf.gameObject);
		}
		PlayerMyController.Instance.EnabledWindowCount--;
	}

	// Update is called once per frame
	void Update()
	{

	}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Gamekit3D;
using UnityEngine.UI;



public class RoleUI : MonoBehaviour
{

	public TextMeshProUGUI HPValue;
	public TextMeshProUGUI InteligenceValue;
	public TextMeshProUGUI SpeedValue;
	public TextMeshProUGUI LevelValue;
	public TextMeshProUGUI AttackValue;
	public TextMeshProUGUI DefenseValue;
	public GameObject ValueButton;
	public GameObject NameButton;
	public GameObject AttributeButton;
	public GameObject StatusButton;
	private Damageable m_damageable;
	private PlayerController m_controller;
	public Text EquippedCount;

	public Sprite myImg;
	public void A() { }
		private void Awake()
		{

		}
		// Use this for initialization
		void Start()
		{

		}

	private void OnEnable()
	{
		PlayerMyController.Instance.EnabledWindowCount++;
		if (m_controller == null || m_damageable == null)
		{
			m_controller = PlayerController.Mine;
			m_damageable = PlayerController.Mine.GetComponent<Damageable>();
		}
		GameObject.Find("ItemImage").GetComponent<Image>().sprite = myImg;
		ValueButton.GetComponentInChildren<Text>().text = string.Format("数值");

		NameButton.GetComponentInChildren<Text>().text = string.Format("名称");
		StatusButton.GetComponentInChildren<Text>().text = string.Format("状态"); 

		AttributeButton.GetComponentInChildren<Text>().text = string.Format("种类");
		EquippedCount.text = string.Format("装备数量：{0}/6",PlayerAttribute.equipped_count);

		string hp = string.Format("{0}/{1}", m_damageable.currentHitPoints, m_damageable.maxHitPoints);
		HPValue.SetText(hp, true);
		LevelValue.SetText(PlayerAttribute.level.ToString(), true);
		InteligenceValue.SetText(PlayerAttribute.Intelligence.ToString(), true);
		AttackValue.SetText(PlayerAttribute.Attack.ToString(), true);
		DefenseValue.SetText(PlayerAttribute.Defense.ToString(), true);
		SpeedValue.SetText(PlayerAttribute.Speed.ToString(), true);


	}

		private void OnDisable()
		{
			PlayerMyController.Instance.EnabledWindowCount--;
		}

		// Update is called once per frame
		void Update()
		{

		}

		void Test()
		{
			//HPValue.text = "100";
			//InteligenceValue.text = "100";
		}

		public void InitUI(PlayerController controller)
		{
			m_damageable = controller.GetComponent<Damageable>();
			m_controller = controller;
		}


}
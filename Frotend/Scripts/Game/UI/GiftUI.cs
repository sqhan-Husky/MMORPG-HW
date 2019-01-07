using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gamekit3D;
using Gamekit3D.Network;
using System;
using Common;
using TMPro;

public class GiftUI : MonoBehaviour
{
	public GameObject ValueButton;
	public GameObject NameButton;
	public GameObject AttributeButton;
	public GameObject StatusButton;

	// Use this for initialization

	private void Awake()
    {
        
    }

    private void OnEnable()
    {
        PlayerMyController.Instance.EnabledWindowCount++;
    }

    private void OnDisable()
    {
        PlayerMyController.Instance.EnabledWindowCount--;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button kickButton;
    [SerializeField] private Button autoKickButton;
    // Start is called before the first frame update

    void OnEnable()
    {
        Player.KickAvailabilityChanged += HandleKickAvailable;
    }

    void OnDisable()
    {
        Player.KickAvailabilityChanged -= HandleKickAvailable;
    }

    void Start()
    {
        kickButton.gameObject.SetActive(false);
    }

    private void HandleKickAvailable(bool canKick)
    {
        kickButton.gameObject.SetActive(canKick);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Mirror;
using TMPro;
using UnityEngine;

public class MainMenuHelper : MonoBehaviour
{
    public NetworkManager networkManager;
    public TMP_InputField inputField;
    
    private void Start()
    {
        networkManager = NetworkManager.singleton;
    }

    public void OnHostButtonPressed()
    {
        networkManager.StartHost();
    }

    public void OnClientButtonPressed()
    {
        string URL = inputField.text;
    }
    
    private Uri TryParseIpAddress()
    {
        UriBuilder uriBuilder = new UriBuilder();
        uriBuilder.Scheme = "tcp4";
        if (inputField &&
            IPAddress.TryParse(inputField.text, out IPAddress address))
        {
            uriBuilder.Host = address.ToString();
        }
        else
        {
            uriBuilder.Host = "localhost";
        }

        var uri = new Uri(uriBuilder.ToString(), UriKind.Absolute);
        return uri;
    }
}

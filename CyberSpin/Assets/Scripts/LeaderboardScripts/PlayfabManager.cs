using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{
    public string _name;
    void Start()
    {
        Login();
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = _name,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request , OnSuccess , OnError);
    }

    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login/account create! " + _name);
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
        
    }
}

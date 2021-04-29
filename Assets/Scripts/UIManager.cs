using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  public static UIManager instance;
  public bool NewUser = false;

  public GameObject startMenu;
  public GameObject loadingScreen;

  //Alert
  [Header("Alert")]
  public GameObject AlertModal;
  public TextMeshProUGUI AlertText;

  //Login
  [Header("Login")]
  public TextMeshProUGUI username;
  public TMP_InputField passwordAttempt;

  //New User
  [Header("New User")]
  public TextMeshProUGUI newUserName;
  public TextMeshProUGUI newUserEmail;
  public TMP_InputField newUserPassword;

  protected bool InvalidLog = false;

  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else if (instance != this)
    {
      Debug.Log("Instance already exists, destroying object!");
      Destroy(this);
    }
  }

  public void InvalidLogin(string _msg)
  {
    InvalidLog = true;
    AlertText.text = _msg;
    AlertModal.SetActive(true);
  }

  public void NewPlayer()
  {
    ClientSend.NewPlayer();
  }

  public void ConnectToServer(bool newUser = false)
  {
    NewUser = newUser;
    Client.instance.ConnectToServer();
  }
}
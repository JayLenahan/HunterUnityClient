using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  public static UIManager instance;
  public bool NewUser = false;

  [Header("Main & Overlays")]
  public GameObject StartMenu;
  public GameObject loadingScreen;
  public GameObject newPlayerScreen;

  //Character select screen
  [Header("Logged In")]
  public GameObject CharacterSelectScreen;

  //Alert
  [Header("Alert")]
  public GameObject AlertModal;
  public TextMeshProUGUI AlertText;

  //Login
  [Header("Login")]
  public TMP_InputField username;
  public TMP_InputField passwordAttempt;

  //New User
  [Header("New User")]
  public TMP_InputField newUserName;
  public TMP_InputField newUserEmail;
  public TMP_InputField newUserPassword;

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

  public void LoadCharacterList(List<Character> _characters)
  {
    foreach (var character in _characters)
    {

    }
    StartMenu.SetActive(false);
    CharacterSelectScreen.SetActive(true);
    Loading(false);
  }
  
  public void Alert(string _msg)
  {
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

  public void Loading(bool loading)
  {
    loadingScreen.SetActive(loading);
  }

  public void Disconnected(string _msg)
  {
    newPlayerScreen.SetActive(false);
    CharacterSelectScreen.SetActive(false);
    StartMenu.SetActive(true);
    Loading(false);
    Alert(_msg);
    Client.instance.ReInit();
  }
}
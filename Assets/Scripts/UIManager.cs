using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  public static UIManager instance;

  public GameObject startMenu;
  public TextMeshProUGUI usernameField;

  //New User
  [Header("New User")]
  public TextMeshProUGUI newUserName;
  public TextMeshProUGUI newUserEmail;
  public TextMeshProUGUI newUserPassword;

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

  public void NewPlayer()
  {
    Client.instance.ConnectToServer();
    //ClientSend.NewPlayer();
  }

  public void ConnectToServer()
  {
    //startMenu.SetActive(false);
    //usernameField.interactable = false;
    //Client.instance.ConnectToServer();
  }
}
﻿using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
  public static void Welcome(Packet _packet)
  {
    int _myId = _packet.ReadInt();
    string _msg = _packet.ReadString();

    Debug.Log($"Message from server: {_msg}");
    Client.instance.myId = _myId;

    ClientSend.Welcome(UIManager.instance.NewUser);

    Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
  }
  public static void InvalidLogin(Packet _packet)
  {
    int _myId = _packet.ReadInt();
    string _msg = _packet.ReadString();

    UIManager.instance.InvalidLogin(_msg);
    Client.instance.ReInit();
  }
  public static void SpawnPlayer(Packet _packet)
  {
    int _id = _packet.ReadInt();
    string _username = _packet.ReadString();
    Vector3 _position = _packet.ReadVector3();
    Quaternion _rotation = _packet.ReadQuaternion();

    GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
  }

  public static void PlayerPosition(Packet _packet)
  {
    int _id = _packet.ReadInt();
    Vector3 _position = _packet.ReadVector3();

    GameManager.players[_id].transform.position = _position;
  }

  public static void PlayerRotation(Packet _packet)
  {
    int _id = _packet.ReadInt();
    Quaternion _rotation = _packet.ReadQuaternion();

    GameManager.players[_id].transform.rotation = _rotation;
  }
}
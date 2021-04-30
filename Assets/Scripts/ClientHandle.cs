using System.Collections;
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

    UIManager.instance.Loading(false);
    ClientSend.Welcome(UIManager.instance.NewUser);
    Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
  }

  public static void LoginSuccessful(Packet _packet)
  {
    int _myId = _packet.ReadInt();
    string _username = _packet.ReadString();
    int _characterCount = _packet.ReadInt();
    List<Character> _characters = new List<Character>();

    for (var i = 0; i < _characterCount; i++)
    {
      _characters.Add(new Character
      {
        Id = _packet.ReadLong(),
        Name = _packet.ReadString()
      });
    }
    GameManager.instance.SuccessfulLogin(_myId, _username, _characters);
  }

  public static void Alert(Packet _packet)
  {
    int _myId = _packet.ReadInt();
    string _msg = _packet.ReadString();

    UIManager.instance.Disconnected(_msg);
  }

  public static void NewUserCreated(Packet _packet)
  {
    int _myId = _packet.ReadInt();
    string _msg = _packet.ReadString();

    UIManager.instance.Disconnected(_msg);
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
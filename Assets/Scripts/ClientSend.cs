using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientSend : MonoBehaviour
{
  private static void SendTCPData(Packet _packet)
  {
    _packet.WriteLength();
    Client.instance.tcp.SendData(_packet);
  }

  private static void SendUDPData(Packet _packet)
  {
    _packet.WriteLength();
    Client.instance.udp.SendData(_packet);
  }

  #region Packets
  public static void Welcome(bool newUser)
  {
    using (Packet _packet = new Packet((int)ClientPackets.welcome))
    {
      _packet.Write(Client.instance.myId);
      _packet.Write(newUser);
      _packet.Write(UIManager.instance.username.text);
      _packet.Write(UIManager.instance.passwordAttempt.text);
      if(!newUser)
        UIManager.instance.Loading(true);
      SendTCPData(_packet);
    }
  }

  public static void NewPlayer()
  {
    using (Packet _packet = new Packet((int)ClientPackets.newUser))
    {
      _packet.Write(Client.instance.myId);
      _packet.Write(UIManager.instance.newUserName.text);
      _packet.Write(UIManager.instance.newUserEmail.text);
      _packet.Write(UIManager.instance.newUserPassword.text);
      UIManager.instance.Loading(true);
      SendTCPData(_packet);
    }
  }

  public static void PlayerMovement(bool[] _inputs)
  {
    using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
    {
      _packet.Write(_inputs.Length);
      foreach (bool _input in _inputs)
      {
        _packet.Write(_input);
      }
      _packet.Write(GameManager.players[Client.instance.myId].transform.rotation);

      SendUDPData(_packet);
    }
  }
  #endregion
}
using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;

public class Client : MonoBehaviour {
    /// <summary>
    /// 連線至主機
    /// </summary>
    public void ConnectToServer()
    {
        //先建立IPAddress物件,IP為欲連線主機之IP
        IPAddress ipa = IPAddress.Parse(ConnectManager.ServerIP);

        //建立IPEndPoint
        IPEndPoint ipe = new IPEndPoint(ipa, ConnectManager.Port);

        //先建立一個TcpClient;
        TcpClient tcpClient = new TcpClient();

        //開始連線
        try
        {
            Debug.Log("主機IP=(" + ipa.ToString() + ") Port(" + ConnectManager.Port + ")");
            Debug.Log("客戶端連線至主機中...\n");
            tcpClient.Connect(ipe);
            if (tcpClient.Connected)
            {
                Debug.Log("客服端連線成功!");
                CommunicationBase cb = new CommunicationBase();
                cb.SendMsg("這是客戶端傳給主機的訊息", tcpClient);
                Debug.Log(cb.ReceiveMsg(tcpClient));
            }
            else
            {
                Debug.Log("客服端連線失敗!");
            }
        }
        catch (System.Exception ex)
        {
            tcpClient.Close();
            Debug.Log(ex.Message);
        }
    }

}

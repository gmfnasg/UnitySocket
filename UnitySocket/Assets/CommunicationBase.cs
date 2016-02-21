using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class CommunicationBase : MonoBehaviour {
    /// <summary>
    /// 傳送訊息
    /// </summary>
    /// <param name="msg">要傳送的訊息</param>
    /// <param name="tmpTcpClient">TcpClient</param>
    public void SendMsg(string msg, TcpClient tmpTcpClient)
    {
        NetworkStream ns = tmpTcpClient.GetStream();
        if (ns.CanWrite)
        {
            byte[] msgByte = Encoding.Default.GetBytes(msg);
            ns.Write(msgByte, 0, msgByte.Length);
        }
    }

    /// <summary>
    /// 接收訊息
    /// </summary>
    /// <param name="tmpTcpClient">TcpClient</param>
    /// <returns>接收到的訊息</returns>
    public string ReceiveMsg(TcpClient tmpTcpClient)
    {
        string receiveMsg = string.Empty;
        byte[] receiveBytes = new byte[tmpTcpClient.ReceiveBufferSize];
        int numberOfBytesRead = 0;
        NetworkStream ns = tmpTcpClient.GetStream();

        if (ns.CanRead)
        {
            do
            {
                numberOfBytesRead = ns.Read(receiveBytes, 0, tmpTcpClient.ReceiveBufferSize);
                receiveMsg = Encoding.Default.GetString(receiveBytes, 0, numberOfBytesRead);
            }
            while (ns.DataAvailable);
        }
        return receiveMsg;
    }
}


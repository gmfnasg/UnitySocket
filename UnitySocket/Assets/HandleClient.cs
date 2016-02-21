using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;

public class HandleClient : MonoBehaviour {

    /// <summary>
    /// private attribute of HandleClient class
    /// </summary>
    private TcpClient mTcpClient;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="_tmpTcpClient">傳入TcpClient參數</param>
    public HandleClient(TcpClient _tmpTcpClient)
    {
        this.mTcpClient = _tmpTcpClient;
    }

    /// <summary>
    /// Communicate
    /// </summary>
    public void Communicate()
    {
        try
        {
            CommunicationBase cb = new CommunicationBase();
            string msg = cb.ReceiveMsg(this.mTcpClient);
            Debug.Log(msg + "\n");
            cb.SendMsg("伺服器主機回傳訊息!", this.mTcpClient);
        }
        catch
        {
            Debug.Log("客戶端主動關閉連線!");
            this.mTcpClient.Close();
        }
    } // end HandleClient()
}

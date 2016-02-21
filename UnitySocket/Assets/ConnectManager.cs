using UnityEngine;
using System.Collections;
using System.Threading;
using System.Net;
using System.Net.Sockets;

public class ConnectManager : MonoBehaviour {
    Server Ser;
    Client Cli;

    public ModeTypeEnum ConnectMode = ModeTypeEnum.None;

    public static string ServerIP = "192.168.0.1";
    public static int Port = 9898;

    public static bool DebubMode = true;

    public enum ModeTypeEnum{
        None,
        Server,
        Client,
        Complex, //綜合模式
    }

    void Start()
    {
        ShowConnectInfo();

        switch (ConnectMode)
        {
            case ModeTypeEnum.Server:
                StartServer();
                break;
            case ModeTypeEnum.Client:
                StartClient();
                break;
            case ModeTypeEnum.Complex:
                StartServer();
                StartClient();
                break;
            default :
                break;
        }
    }

    void StartServer()
    {
        Ser = new Server();
        Thread serverThread = new Thread(Ser.ListenToConnection);
        serverThread.Start();
    }
    void StartClient()
    {
        Cli = new Client();
        Thread clientThread = new Thread(Cli.ConnectToServer);
        clientThread.Start();
    }

    /// <summary>
    /// 顯示連線相關資訊
    /// </summary>
    void ShowConnectInfo()
    {
        Debug.Log("本機資訊 Start ---------------");
        Debug.Log("除錯模式狀態: " + DebubMode);
        Debug.Log("連線模式狀態: " + ConnectMode);
        string hostName = Dns.GetHostName();
        Debug.Log("本機名稱=" + hostName);

        //取得本機IP
        IPAddress[] ipa = Dns.GetHostAddresses(hostName);
        for (int i = 0; i < ipa.Length; i++)
        {
            if (ConnectMode == ModeTypeEnum.Server && i == 0)
            {
                ServerIP = ipa[i].ToString();
                Debug.Log("指定伺服器IP=" + ipa[i].ToString());
            }
            Debug.Log("本機IP[" + i + "]=" + ipa[i].ToString());
        }
        Debug.Log("本機資訊 End ---------------");
    }

    void OnApplicationQuit()
    {
        
    }
}

using UnityEngine;
using System.Collections;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

public class Server{

    /// <summary>
    /// 等待客戶端的連線
    /// </summary>
    public void ListenToConnection()
    {
        //取得本機名稱
        string hostName = Dns.GetHostName();
        Debug.Log("伺服器名稱=" + hostName);

        //取得本機IP
        IPAddress[] ipa = Dns.GetHostAddresses(hostName);
        for (int i = 0; i < ipa.Length; i++)
        {
            Debug.Log("伺服器IP[" + i + "]=" + ipa[i].ToString());
        }

        //建立本機端的IPEndPoint物件
        IPEndPoint ipe = new IPEndPoint(ipa[0], 1234);

        //建立TcpListener物件
        TcpListener tcpListener = new TcpListener(ipe);

        //開始監聽port
        tcpListener.Start();
        Debug.Log("伺服器等待客戶端連線中...");

        TcpClient tmpTcpClient;
        int numberOfClients = 0;
        while (true)
        {
            try
            {
                //建立與客戶端的連線
                tmpTcpClient = tcpListener.AcceptTcpClient();

                if (tmpTcpClient.Connected)
                {
                    Debug.Log("伺服器與客戶端連線成功!");
                    HandleClient handleClient = new HandleClient(tmpTcpClient);
                    Thread myThread = new Thread(new ThreadStart(handleClient.Communicate));
                    numberOfClients += 1;
                    myThread.IsBackground = true;
                    myThread.Start();
                    myThread.Name = tmpTcpClient.Client.RemoteEndPoint.ToString();
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        } // end while
    } // end ListenToConnect()

}
using UnityEngine;
using System.Collections;
using System.Threading;

public class TestThread : MonoBehaviour {
    My m;

    void Start()
    {
        m = new My();
        Thread t = new Thread(m.RunMe);
        t.Start();
    }

    void OnApplicationQuit()
    {
        m.Stop();
    }
}
class My
{
    int i = 0;
    bool isRun = true;

    public void RunMe()
    {
        while (isRun)
        {
            MonoBehaviour.print("Run : " + i++);
            Thread.Sleep(1000);
        }
    }

    public void Stop()
    {
        isRun = false;
    }
}
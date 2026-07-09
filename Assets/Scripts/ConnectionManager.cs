using FishNet.Managing;
using FishNet.Managing.Scened;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ConnectionManager : MonoBehaviour
{
    [SerializeField] private NetworkManager _networkManager;

    [SerializeField] private string _gameScene = "OutdoorsScene";

    public void StartHost()
    {
        StartCoroutine(StartHostRoutine());
    }

    private IEnumerator StartHostRoutine()
    {
        _networkManager.ServerManager.StartConnection();

        while (!_networkManager.ServerManager.Started)
            yield return null;

        SceneLoadData sld = new SceneLoadData(_gameScene)
        {
            ReplaceScenes = ReplaceOption.All
        };

        _networkManager.SceneManager.LoadGlobalScenes(sld);

        _networkManager.ClientManager.StartConnection();
    }

    public void StopHost()
    {
        StopClient();
        StopServer();
    }

    public void StartServer()
    {
        _networkManager.ServerManager.StartConnection();
    }

    public void StartClient()
    {
        _networkManager.ClientManager.StartConnection();
    }

    public void StopServer()
    {
        _networkManager.ServerManager.StopConnection(true);
    }

    public void StopClient()
    {
        _networkManager.ClientManager.StopConnection();
    }

    public void SetIPAddress(string text)
    {
        _networkManager.TransportManager.Transport.SetClientAddress(text);
    }
}
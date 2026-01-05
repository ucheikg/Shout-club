using Unity.Netcode;
using UnityEngine.SceneManagement;
public class Death : NetworkBehaviour
{
    public NetworkVariable<bool> isDead = new NetworkVariable<bool>(false);
    private bool sceneLoaded = false;


    public override void OnNetworkSpawn()
    {
        isDead.OnValueChanged += OnDeathChanged;
    }
    private void OnDeathChanged(bool oldValue, bool newValue)
    {
        if (!newValue || sceneLoaded) return;

        sceneLoaded = true;

        if (IsOwner)
        {
            SceneManager.LoadScene("LossScreen");
        }
        else
        {
            SceneManager.LoadScene("WinScreen");
        }
        
        NetworkManager.Singleton.Shutdown();
    }

    public override void OnNetworkDespawn()
    {
        isDead.OnValueChanged -= OnDeathChanged;
    }

    [ServerRpc]
    public void DieServerRpc()
    {
        isDead.Value = true;
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (!IsOwner) return;
        if (isDead.Value) return;
        
        if (collision.gameObject.CompareTag("outside"))
        {
            DieServerRpc();
        }
    }
}

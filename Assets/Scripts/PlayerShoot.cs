using UnityEngine.Networking;
using UnityEngine;

public class PlayerShoot : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";

    public PlayerWeapon weapon;

    [SerializeField]
    private Camera Camera;

    [SerializeField]
    private LayerMask mask;

    private void Start()
    {
        if (Camera == null)
        {
            Debug.LogError("PlayerShoot: No Camera Referenced!");
            this.enabled = false;
        }

    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    [Client]
        void Shoot ()
    {
        RaycastHit _hit;
        if (Physics.Raycast(Camera.transform.position,Camera.transform.forward, out _hit, weapon.range,mask))
        {
            if (_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name);
            }
        }

    }

    [Command]
    void CmdPlayerShot (string _ID)
    {
        Debug.Log(_ID + " has been shot!");

       
    }
}

using UnityEngine;

public class WeaponVisualController : MonoBehaviour
{
    [SerializeField] private Transform[] gunTransforms;
    [SerializeField] private Transform pistol;
    [SerializeField] private Transform revolver;
    [SerializeField] private Transform autoRifle;
    [SerializeField] private Transform shotgun;
    [SerializeField] private Transform SniperRifle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Update()
    {
        SwitchWeapons();
    }

    private void SwitchWeapons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchOn(pistol);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchOn(revolver);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchOn(autoRifle);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchOn(shotgun);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchOn(SniperRifle);
        }
    }

    private void SwitchOn(Transform gun)
    {
        SwitchOffGuns();
        gun.gameObject.SetActive(true);
    }

    private void SwitchOffGuns()
    {         
       foreach (var gun in gunTransforms)
        {
            gun.gameObject.SetActive(false);
        }
    }
}

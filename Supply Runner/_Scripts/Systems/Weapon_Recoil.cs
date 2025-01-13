using UnityEngine;
using Cinemachine;
using System.Collections;

public class Weapon_Recoil : MonoBehaviour
{
    [SerializeField] 
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField] 
    private float recoilMagnitude ;
    [SerializeField] 
    private float returnSpeed;



    private void Start()
    {   
        ActiveWeapon activeWeapon = GetComponent<ActiveWeapon>();
        if(activeWeapon){
            RaycastWeapon raycastWeapon = activeWeapon.raycastWeapon;
            if(raycastWeapon){
                recoilMagnitude = raycastWeapon.recoilMagnitude;
                returnSpeed = raycastWeapon.returnSpeed;
            }
        }
        // Get the Virtual Camera
        if (virtualCamera == null)
        {
            // If virtualCamera is not set, try to find it on the same GameObject
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }
    }

    public void RecoilFire()
    {
        // Adjust the aim properties of the Cinemachine Virtual Camera for recoil
        var noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (noise != null)
        {
            noise.m_AmplitudeGain = recoilMagnitude;
            noise.m_FrequencyGain = recoilMagnitude;
        }

        // Start a coroutine to reset the recoil
        StartCoroutine(ResetRecoil());
    }

    private IEnumerator ResetRecoil()
    {
        yield return new WaitForSeconds(returnSpeed);

        // Reset the aim properties of the Cinemachine Virtual Camera
        var noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (noise != null)
        {
            noise.m_AmplitudeGain = 0.0f;
            noise.m_FrequencyGain = 0.0f;
        }
    }
}

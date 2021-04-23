using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorModeSystem : MonoBehaviour
{
    public Transform mirroredPlayerSpawnPoint;
    public Camera defaultCamera;
    public List<Camera> mirrorModeCameras;
    public PlayerController player;
    public Color mirroredPlayerColor = Color.white;
    public Canvas mirrorModeCanvas;
    
    private PlayerController _mirroredPlayer;
    
    public void ActivateMirrorMode()
    {
        defaultCamera.gameObject.SetActive(false);
        foreach (Camera camera in mirrorModeCameras)
        {
            camera.gameObject.SetActive(true);
        }

        _mirroredPlayer = Instantiate(player.gameObject).GetComponent<PlayerController>();
        _mirroredPlayer.SetToMirrored();
        _mirroredPlayer.GetComponent<SpriteRenderer>().color = mirroredPlayerColor;

        _mirroredPlayer.transform.position = mirroredPlayerSpawnPoint.position;

        mirrorModeCameras[1].GetComponent<CameraMovement>().player = _mirroredPlayer.transform;
        
        mirrorModeCanvas.gameObject.SetActive(true);
    }
}

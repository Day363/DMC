using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class depoleia_tree_counsel : MonoBehaviour
{
    public GameObject player;
    public GameObject cammanager;
    public GameObject gamemanager;
    public GameObject campos;
    public GameObject letterbox;
    public GameObject counselmirror;
    public GameObject counselcollider;
    public GameObject chat;
    public GameObject background;

    public bool firstmet;


    PlayerMove playerPlayerMove;
    CameraManager cammanagerCameraManager;

    public void Start()
    {
        playerPlayerMove = player.GetComponent<PlayerMove>();
        cammanagerCameraManager = cammanager.GetComponent<CameraManager>();
    }

    public void OnEnable()
    {
        chatmanager.OnchatEnd += GoToCounsel;
    }

    public void FixedUpdate()
    {
        if (!firstmet && transform.position.x - player.transform.position.x < 5)
        {
            firstmet = true;
            gamemanager.GetComponent<chatmanager>().enemychatbox = chat;
            campos.transform.position = new Vector3((transform.position.x + player.transform.position.x) / 2, (transform.position.y + player.transform.position.y) / 2, 0);
            cammanagerCameraManager.LookCounsel(campos);
            letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
            playerPlayerMove.canmove = false;
            playerPlayerMove.Stop();
            gamemanager.GetComponent<chatmanager>().CallDialogue(2);

        }
    }

    public void GoToCounsel()
    {
        GameObject currentcounselmirror = Instantiate(counselmirror, new Vector3(-450f, 26.7f, 0), Quaternion.identity);
        tocounselmanager currentcounselmirrortocounselmanager = currentcounselmirror.GetComponent<tocounselmanager>();
        currentcounselmirrortocounselmanager.player = player;
        currentcounselmirrortocounselmanager.cammanager = cammanager;
    }
}

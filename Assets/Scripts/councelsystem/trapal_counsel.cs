using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class trapal_counsel : MonoBehaviour
{
    public GameObject player;
    public GameObject attackcore;
    public GameObject cammanager;
    public GameObject gamemanager;
    public GameObject campos;
    public GameObject letterbox;
    public GameObject chat;
    public GameObject deny;

    public bool firstmet;

    PlayerMove playerPlayerMove;
    CameraManager cammanagerCameraManager;

    public void Start()
    {
        playerPlayerMove = player.GetComponent<PlayerMove>();
        cammanagerCameraManager = cammanager.GetComponent<CameraManager>();
    }

    public void FixedUpdate()
    {
        if (!firstmet && transform.position.x - player.transform.position.x < 10)
        {
            firstmet = true;

            campos.transform.position = new Vector3((transform.position.x + player.transform.position.x) / 2, (transform.position.y + player.transform.position.y) / 2, 0);
            cammanagerCameraManager.LookCounsel(campos);
            cammanagerCameraManager.CinemachineInvalidateCache();
            letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
            playerPlayerMove.canmove = false;
            playerPlayerMove.Stop();
            gamemanager.GetComponent<chatmanager>().enemychatbox = chat;
            gamemanager.GetComponent<chatmanager>().CallDialogue(3);

        }
    }

    public void Setdeny()
    {
        deny.SetActive(true);
        deny.transform.localScale = new Vector3(0, 0, 1);
        deny.transform.DOScale(new Vector3(0.38f, 0.38f, 1), 0.7f).SetEase(Ease.InOutQuart);
    }

    public void BattleStart()
    {
        StartCoroutine(BattleStart_co());
        
    }

    IEnumerator BattleStart_co()
    {
        GetComponent<trapal_script>().canattack = true;
        yield return new WaitForSeconds(2);
        attackcore.GetComponent<attackcore>().BattleStart();
    }
}

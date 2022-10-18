using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyComputer : MonoBehaviour
{
    public static bool CafeOpen;
    MyCharacterController myControler;
    [SerializeField] Transform cameraPos;
    [SerializeField] GameObject Crosshair;
    bool sittingComputer;
    #region PcPanels
    [Header("Pc Panel's")]
    [SerializeField] GameObject openLoadPanel;
    [SerializeField] GameObject pcWindows;
    #endregion
    MyInputs myInputs = new();
    Camera cam;
    private void Start()
    {
        myControler = MyCharacterController.instance;
        cam = Camera.main;
    }
    private void Update()
    {
        //Debug.Log(CafeOpen);
    }
    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (myInputs.DownE && !sittingComputer) Sitting(null, cameraPos.position, false, CursorLockMode.Confined, true, false, true);
        if (myInputs.DownQ && sittingComputer) Sitting(myControler.transform, new(0, 0.90f, 0), true, CursorLockMode.Locked, false, true, false);
        if (myInputs.DownSpace && sittingComputer) StartCoroutine(OpenComputer());
    }
    void Sitting(Transform cameraParent, Vector3 cameraTarget, bool crosshairSetacive, CursorLockMode cursorMode, bool cursorVisible, bool playerCanMove, bool SittingComputer)
    {
        StopAllCoroutines();
        cam.transform.SetParent(cameraParent);
        StartCoroutine(MoveCam(cameraTarget));
        Crosshair.SetActive(crosshairSetacive);
        Cursor.lockState = cursorMode;
        Cursor.visible = cursorVisible;
        myControler.canMove = playerCanMove;
        sittingComputer = SittingComputer;
    }
    IEnumerator OpenComputer()
    {
        openLoadPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        openLoadPanel.SetActive(false);
        pcWindows.SetActive(true);
    }
    IEnumerator MoveCam(Vector3 Target)
    {
        yield return new WaitForSeconds(0.3f);
        float elapsedTime = 0f;
        float duration = 1f;
        //elapsed time duration while ici mantýgýný sor
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, Target, elapsedTime);
            cam.transform.localRotation = Quaternion.Lerp(cam.transform.localRotation, Quaternion.identity, elapsedTime);
            yield return null;
        }
    }
    public void OpenCafe(Button button)
    {
        CafeOpen = !CafeOpen;
        if (CafeOpen) button.GetComponent<Image>().color = Color.green;
        else button.GetComponent<Image>().color = Color.red;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Joystick : MonoBehaviour, IPointerDownHandler,IPointerUpHandler, IDragHandler
{
    private GameObject player;

    private RectTransform background;
    private RectTransform handle;
    private Vector2 currentPos;

    private Canvas canvas;
    private Camera cam;

    /*
    public float HandleRange
    {
        get { return handleRange; }
        set { handleRange = Mathf.Abs(value); }
    }

    public float DeadZone
    {
        get { return deadZone; }
        set { deadZone = Mathf.Abs(value); }
    }
*/
    private float handleRange = 1;
    private float deadZone = 0;

    void Start()
    {
        player      = GameObject.FindGameObjectWithTag("Player");
        background  = GetComponent<RectTransform>();
        handle      = transform.GetChild(0).GetComponent<RectTransform>();

        canvas = GetComponentInParent<Canvas>();
        cam = Camera.main;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        cam = null;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            cam = canvas.worldCamera;

        Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        Vector2 radius = background.sizeDelta / 2;
        currentPos = (eventData.position - position) / (radius * canvas.scaleFactor);
        HandleInput(currentPos.magnitude, currentPos.normalized, radius, cam);

        handle.anchoredPosition = currentPos * radius * handleRange;

        if (player != null)
            player.GetComponent<PlayerMovement>().SetMovementDirection(currentPos.normalized);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        handle.anchoredPosition = Vector2.zero;

        if(player != null)
            player.GetComponent<PlayerMovement>().SetMovementDirection(Vector2.zero);
    }

    protected void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > deadZone)
        {
            if (magnitude > 1)
                currentPos = normalised;
        }
        else
            currentPos = Vector2.zero;
    }

}

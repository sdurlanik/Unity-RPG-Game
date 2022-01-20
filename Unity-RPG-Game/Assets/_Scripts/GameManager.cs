using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;

    private NPC currentTarget;

    private void Update()
    {
        ClickTarget();
    }

    private void ClickTarget()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity,512);
            if (hit.collider != null)
            {
                if (currentTarget != null)
                {
                    currentTarget.Deselect();
                }

                currentTarget = hit.collider.GetComponent<NPC>();

                player.MyTarget = currentTarget.Select();
            }
            else
            {
                if (currentTarget != null)
                {
                    currentTarget.Deselect();
                }

                currentTarget = null;
                player.MyTarget = null;
            }
       
        }
    }
}

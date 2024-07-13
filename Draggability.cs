using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggability : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition ;
    private Vector3 overlapBoxsize = new Vector3(0.8f,0.8f,0.8f);
    private GameObject SpawnedObject ;

    [SerializeField] private LayerMask placementSlotsLayer ;
    [SerializeField] private GameObject objectToSpawn ;

    public void OnBeginDrag(PointerEventData eventData)
    {
       startPosition = transform.position;
       
    }

    public void OnDrag(PointerEventData eventData)
    {
      transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x , Camera.main.ScreenToWorldPoint(Input.mousePosition).y , 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Collider2D overlap = Physics2D.OverlapBox(transform.position, overlapBoxsize , 0 , placementSlotsLayer);
        if (overlap != null)
         {
          if(SpawnedObject != null)
          {
           Destroy(SpawnedObject);
          }
          SpawnedObject = Instantiate(objectToSpawn , overlap.transform.position , overlap.transform.rotation);
         }
          transform.position = startPosition ;
    }

    void OnDrawGizmos()
    {
      Gizmos.color = Color.red;
      Gizmos.DrawWireCube(transform.position, overlapBoxsize);
    }
}

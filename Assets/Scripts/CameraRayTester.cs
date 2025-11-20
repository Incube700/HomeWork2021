using UnityEngine;

public class MouseRayTester : MonoBehaviour
{
   [SerializeField] private Camera _camera;

   private void Update()
   {
      if (Input.GetMouseButton(0))
      {
         Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

         if (Physics.Raycast(ray, out RaycastHit hitInfo))
         {
            Debug.Log("Попал по: " + hitInfo.collider.name);
         }
      }
   }
}

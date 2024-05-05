using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Transform cameraTarget;

    [Header("Zoom")] 
    [SerializeField] 
    private CinemachineCameraOffset cameraOffset;
    [SerializeField] 
    private float zoomSensibility;

    [SerializeField] 
    private Vector2 zoomRange;

    [SerializeField] 
    private float zoomSmoothTime;
    [Header("Rotation")] 
    [SerializeField] 
    private float rotationSensibility;  
    [SerializeField] 
    private float rotationSmoothTime;
    // Start is called before the first frame update

    private Plane m_plane;
    private Vector3 m_lastMousePosition;
    private float m_zoomVelocity = 0f;
  //  private float m_rotationVelocity = 0f;
   // private float m_rotationTarget = 0f;
    private float m_zoomTarget= 0f;

    void Start()
    {
        m_plane = new Plane(Vector3.up, Vector3.zero);
        m_zoomTarget = cameraOffset.m_Offset.z;
    //    m_rotationTarget = cameraTarget.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        #region Pan Map
        if (Input.GetMouseButtonDown(0))
        {
            m_lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
           
                //Get the point that is clicked
                Vector3 hitPoint = GetMouseOnPlane(Input.mousePosition);
                Vector3 lastPosition = GetMouseOnPlane(m_lastMousePosition);
                //Move your cube GameObject to the point where you clicked
                Vector3 offset = hitPoint - lastPosition;
                cameraTarget.position -= offset; // * (deltaTime * panSpeed) ;
                m_lastMousePosition = Input.mousePosition;
        }
        #endregion

        #region Zoom
        m_zoomTarget += Input.mouseScrollDelta.y * zoomSensibility;
        m_zoomTarget = Mathf.Clamp(m_zoomTarget,zoomRange.y , zoomRange.x);

        cameraOffset.m_Offset.z=Mathf.SmoothDamp(cameraOffset.m_Offset.z, m_zoomTarget, ref m_zoomVelocity, zoomSmoothTime);
        #endregion

        #region Rotation

        float axis = 0f;
        if (Input.GetKey(KeyCode.Q))
        {
            axis = 1f;
        }
        
        if (Input.GetKey(KeyCode.E))
        {
            axis = -1f;
        }

        cameraTarget.localEulerAngles =
            new Vector3(cameraTarget.localEulerAngles.x, cameraTarget.localEulerAngles.y + axis * rotationSensibility * Time.deltaTime, cameraTarget.localEulerAngles.z);

        #endregion


    }

    private Vector3 GetMouseOnPlane(Vector3 p_position)
    {
        Ray ray = mainCamera.ScreenPointToRay(p_position);

        if (m_plane.Raycast(ray, out var enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            hitPoint.y = 0f;
            return hitPoint;
        }

        return Vector3.zero;
    }
}

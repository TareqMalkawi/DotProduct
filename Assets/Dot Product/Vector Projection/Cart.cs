using UnityEngine;

public class Cart : MonoBehaviour
{
    [SerializeField] private Rigidbody cartRB;
    [SerializeField] private float cartForwardForce;
    [SerializeField] private GameObject cartLocalCenterOfMass;

    [SerializeField] private GameObject frontWheel;
    [SerializeField] private GameObject rearWheel;
    
    private Vector3 frontWheelPos;
    private Vector3 rearWheelPos;

    private float d;
    
    private void Start()
    {
        cartRB = gameObject.GetComponent<Rigidbody>();
        if (cartRB)
            cartRB.centerOfMass = cartLocalCenterOfMass.transform.localPosition;
    }

    private void FixedUpdate()
    {
        MoveCart();
    }

    private void MoveCart()
    {
        frontWheelPos = new Vector3(frontWheel.transform.position.x, frontWheel.transform.position.y - 0.5f,
            frontWheel.transform.position.z);
        rearWheelPos = new Vector3(frontWheel.transform.position.x, frontWheel.transform.position.y - 0.5f,
            frontWheel.transform.position.z);
        
        cartRB.AddForceAtPosition(Vector3.forward * cartForwardForce, frontWheelPos);
        cartRB.AddForceAtPosition(Vector3.forward * cartForwardForce, rearWheelPos);
    }

    private void OnTriggerStay(Collider other)
    {
        if(!other.CompareTag("upward-force")) return;
        
        Vector3 f = new Vector3(0, - 120 * 9.8f, 0);
        Vector3 v = new Vector3(Mathf.Cos(14.0f * Mathf.Deg2Rad), Mathf.Sin(14.0f * Mathf.Deg2Rad), 0);
        
        Vector3 force = Vector3.Project(f, v);

        float forceMagnitude = force.magnitude;
        
        Debug.Log(Vector3.Dot(f,v));
        
        cartRB.AddForceAtPosition(-Vector3.forward * forceMagnitude, frontWheelPos);
        cartRB.AddForceAtPosition(-Vector3.forward * forceMagnitude, rearWheelPos);
    }
}
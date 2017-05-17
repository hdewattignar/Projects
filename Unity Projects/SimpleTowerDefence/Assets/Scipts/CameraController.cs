using UnityEngine;


public class CameraController : MonoBehaviour {

    private bool doMove = true;
    public float panSpeed = 10f;
    public float offSet = 40f;
    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 50f;

	// Update is called once per frame
	void Update () {

        //toggle movement
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMove = !doMove;
        }

        if (!doMove)
        {
            return;
        }

        //X,Y movement
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - offSet)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= offSet)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= offSet)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - offSet)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        //Zooming
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;



	}
}

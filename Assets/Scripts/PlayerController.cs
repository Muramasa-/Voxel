using UnityEngine;

public class PlayerController : MonoBehaviour {

	public static float maxInteratDist = 40;
	private float speed = 2.0f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, maxInteratDist)) {
				int chunkX = Mathf.FloorToInt(hit.point.x) / Chunk.cSize;
				int chunkZ = Mathf.FloorToInt(hit.point.z) / Chunk.cSize;
				Chunk chunk = Chunk.generator.currentLoadedChunks[chunkX, chunkZ];
				if (chunk != null) {
					//Debug.Log(Mathf.FloorToInt(hit.point.x) / Chunk.cSize + " - " + Mathf.FloorToInt(hit.point.y) + " - " + Mathf.FloorToInt(hit.point.z) / Chunk.cSize);
					//chunk.blocks[(chunkX * Chunk.cSize) - Mathf.FloorToInt(hit.point.x), Mathf.FloorToInt(hit.point.y), (chunkZ * Chunk.cSize) - Mathf.FloorToInt(hit.point.z)] = Block.Air.id;
					//chunk.requiresDataUpdate = true;
				}
				else {
					Debug.Log("Chunk null");
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.G)) {
			Chunk.generator.UpdateVisibleChunks();
		}
		
		/*if (Input.GetKey(KeyCode.D)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.A)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S)) {
			transform.position += Vector3.forward * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.W)) {
			transform.position += Vector3.back * speed * Time.deltaTime;
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Cursor.visible = !Cursor.visible;
			Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Confined;
		}*/
	}
}

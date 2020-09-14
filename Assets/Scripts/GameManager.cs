using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager INSTANCE;
    public World world = new World();

	//[SerializeField]
	//private ComputeShader simplexShader;

	void Start() {
		#if UNITY_EDITOR
			UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
		#endif
		
		INSTANCE = this;

		Chunk.generator = GetComponent<TerrainGenerator>();
		Chunk.cSize = Chunk.generator.cSize;
		Chunk.cHeight = Chunk.generator.cHeight;
		//Chunk.sectionCount = Chunk.cHeight / ChunkSection.size;

		//Chunk.generator.enableDynamicChunks = true;
		//Chunk.generator.UpdateVisibleChunks();



		/*var size = 16 * 16 * 16;
		var stride = sizeof(float);
		ComputeBuffer output = new ComputeBuffer(size, stride);
		simplexShader.SetBuffer(0, "voxels", output);
		simplexShader.Dispatch(0, 4, 4, 4);
		
		Debug.Log(output.count);*/

		/*ComputeBuffer buffer = new ComputeBuffer(4, sizeof(int));
		simplexShader.SetFloat("cWidth", Chunk.cWidth);
		simplexShader.SetFloat("cHeight", Chunk.cHeight);
		simplexShader.SetBuffer(0, "buffer1", buffer);
		simplexShader.Dispatch(0, 1, 1, 1);
		
		int[] data = new int[Chunk.cWidth * Chunk.cHeight * Chunk.cWidth];
		buffer.GetData(data);

		for (int i = 0; i < data.Length; i++) {
			Debug.Log(data[i]);
		
		buffer.Release();	}*/



		//Graphics.DrawMeshInstancedIndirect();
	}
}
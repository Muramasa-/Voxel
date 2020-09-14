using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (TerrainGenerator))]
public class TerrainGeneratorEditor : Editor {

    public override void OnInspectorGUI() {
        TerrainGenerator terrainGenerator = (TerrainGenerator)target;

		DrawDefaultInspector();

        if (GUILayout.Button ("Generate Chunks")) {
            if (terrainGenerator.chunkHolder != null) {
                for (int i = 0; i < terrainGenerator.chunkHolder.transform.childCount; i++) {
                    Destroy(terrainGenerator.chunkHolder.transform.GetChild(i).gameObject);
                }
            }
            //terrainGenerator.nameToChunkDict.Clear();
            terrainGenerator.chunkTimeTotal = 0;
            terrainGenerator.UpdateVisibleChunks();
            
            //if (Chunk.allChunks != null && Chunk.allChunks.Count > 0) {
//                Debug.Log("Avg Chunk Noise Time: " + Chunk.generator.chunkTimeNoise / Chunk.generator.allChunks.Count + "ms");
//                Debug.Log("Avg Chunk Voxel Time: " + Chunk.generator.chunkTimeVoxels / Chunk.generator.allChunks.Count + "ms");
//                Debug.Log("Avg Chunk Mesh Time: " + Chunk.generator.chunkTimeMesh / Chunk.generator.allChunks.Count + "ms");
//                Debug.Log("Avg Chunk Total Time: " + Chunk.generator.chunkTimeTotal / Chunk.generator.allChunks.Count + "ms");
            //}
        }
    }
}
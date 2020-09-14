using System;
using System.Collections.Generic;
using UnityEngine;

public class Chunk {
    public GameObject gameObject;
    public Bounds bounds;
    public Vector3 chunkWorldPos;

    public float[] noiseSet;

    public BlockStack[,,] blocks;

    public Mesh mesh = new Mesh();
    private readonly MeshFilter meshFilter;
    private readonly MeshCollider meshCollider;
    private MeshRenderer meshRenderer;
    public MeshBuffer tempMeshBuffer = new MeshBuffer();
    public int faceCount = 0;

    public readonly int cX, cY, cZ, cXReal, cYReal, cZReal;

    public static GameObject chunkPrefab;
    public static int chunkLayer = LayerMask.NameToLayer("Chunk");
    public static TerrainGenerator generator;
    public static int cSize, cHeight;
    public static Chunk lastMeshedChunk = null;
    
    public static byte NONE = 0;
    public static byte U = 1 << 0;
    public static byte D = 1 << 1;
    public static byte L = 1 << 2;
    public static byte R = 1 << 3;
    public static byte F = 1 << 4;
    public static byte B = 1 << 5;

    //TODO USE PREFAB

	public Chunk(GameObject gameObject, Transform parent, int x, int y, int z, Material material) {
	    var watch = System.Diagnostics.Stopwatch.StartNew();
	    blocks = new BlockStack[cSize, cHeight, cSize]; //cSize + 2: Used to generate faces across chunk borders

        cX = x;
	    cY = y;
        cZ = z;
	    cXReal = x * cSize;
	    cYReal = y * cHeight;
	    cZReal = z * cSize;
	    chunkWorldPos = new Vector3(cXReal, cYReal, cZReal);
	    bounds = new Bounds(chunkWorldPos, Vector3.one * cSize);

	    this.gameObject = gameObject; 
	    
	    gameObject.name = String.Format("Chunk ({0}, {1})", cX, cZ);
        gameObject.transform.position = chunkWorldPos;

	    mesh.MarkDynamic();
        meshFilter = gameObject.GetComponent<MeshFilter>();
        //meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.TwoSided;


	    noiseSet = generator.fastNoise.GetNoiseSet(cXReal, cYReal, cZReal, cSize, cHeight, cSize); //45ms
	    GenerateBlockData(); //167ms (231 total)
	    GenerateMeshData(); //255ms
	    UpdateMesh(); //12ms

	    watch.Stop();
	}

    public float GetNoiseValue(int x, int y, int z) {
        return noiseSet[x * cHeight * cSize + y * cSize + z];
    }

    public byte GetBlockIdFromNoise(int x, int y, int z) {
        //return noiseSet[x * cHeight * cSize + y * cSize + z] >= 0 ? Block.Stone.id : Block.Air.id;
        return noiseSet[x + cSize * (y + cSize * z)] >= 0 ? Block.Stone.id : Block.Air.id;
    }

    public void UpdateBlock(int x, int y, int z) { //Update block and surrounding ids
        
    }
    
//    public void GenerateBlockData() {
//        int nIndex = 0;
//        BlockStack stack;
//        
//        for (var x = 0; x < cSize; x++) {
//            for (var y = 0; y < cHeight; y++) { //Looping over Y first changes noise?
//                for (var z = 0; z < cSize; z++) {
//                    //Block has been processed
//                    if (blocks[x, y, z].id != Block.Invalid.id) continue;
//                   
//                    stack = new BlockStack(noiseSet[nIndex++] >= 0 ? Block.Stone.id : Block.Air.id);
//                    
//                    if (stack.ABOVE == Block.Invalid.id) {
//                        if (y < cHeight - 1) {
//                            stack.ABOVE = GetBlockIdFromNoise(x, y + 1, z);
//                            blocks[x, y + 1, z] = new BlockStack(stack.ABOVE);
//                        } else {
//                            //Over chunk border
//                        }
//                    }
//
//                    if (stack.BELOW == Block.Invalid.id) {
//                        if (y > 0) {
//                            stack.BELOW = GetBlockIdFromNoise(x, y - 1, z);
//                            blocks[x, y - 1, z] = new BlockStack(stack.BELOW);
//                        }
//                    }
//
//                    if (stack.BACK == Block.Invalid.id) {
//                        if (x < cSize - 1) {
//                            stack.BACK = GetBlockIdFromNoise(x + 1, y, z);
//                            blocks[x + 1, y, z] = new BlockStack(stack.BACK);
//                        }
//                    }
//
//                    if (stack.FRONT == Block.Invalid.id) {
//                        if (x > 0) {
//                            stack.FRONT = GetBlockIdFromNoise(x - 1, y, z);
//                            blocks[x - 1, y, z] = new BlockStack(stack.FRONT);
//                        }
//                    }
//
//                    if (stack.RIGHT == Block.Invalid.id) {
//                        if (z < cSize - 1) {
//                            stack.RIGHT = GetBlockIdFromNoise(x, y, z + 1);
//                            blocks[x, y, z + 1] = new BlockStack(stack.RIGHT);
//                        }
//                    }
//
//                    if (stack.LEFT == Block.Invalid.id) {
//                        if (z > 0) {
//                            stack.LEFT = GetBlockIdFromNoise(x, y, z - 1);
//                            blocks[x, y, z - 1] = new BlockStack(stack.LEFT);
//                        }
//                    }
//
//                    blocks[x, y, z] = stack;
//                }
//            }
//        }
//    }

    public void GenerateBlockData() {
        int nIndex = 0;
        for (var x = 0; x < cSize /*+ 2*/; x++) {
//            if (x == 16) { //Neg X border
//                
//            }
//            if (x == 17) { //Pos X Border
//                
//            }
            for (var y = 0; y < cHeight /*+ 2*/; y++) { //Looping over Y first changes noise?
                for (var z = 0; z < cSize /*+ 2*/; z++) {
                    
                    
                    blocks[x, y, z] = new BlockStack(noiseSet[nIndex++] >= 0 ? Block.Stone.id : Block.Air.id);
                }
            }
        }
    }
    
//    public void GenerateBlockData() {
//        int nIndex = 0;
//        for (var x = 0; x < cSize; x++) {
//            for (var y = 0; y < cHeight; y++) { //Looping over Y first changes noise?
//                for (var z = 0; z < cSize; z++) {
//                    blocks[x, y, z] = new BlockStack(noiseSet[nIndex++] >= 0 ? Block.Stone.id : Block.Air.id);
//                }
//            }
//        }
//    }
    
    public byte GetBlock(int x, int y, int z) {
        if (x >= cSize || x <= 0 || y >= cHeight || y <= 0 || z >= cSize || z <= 0) return Block.Invalid.id;
        BlockStack stack = blocks[x, y, z];
        return stack.id != 0 ? stack.id : Block.Invalid.id;
    }
    public void GenerateMeshData() {
        tempMeshBuffer.vertices = new List<Vector3>();
        tempMeshBuffer.triangles = new List<int>();
        for (var y = 0; y < cHeight; y++) {
            for (var x = 0; x < cSize; x++) {
                for (var z = 0; z < cSize; z++) {
                    if (blocks[x, y, z].id == Block.Air.id) continue;
                    tempMeshBuffer.AddVoxel(this, x, y, z);
                }
            }
        }
        //lastMeshedChunk = generator.currentLoadedChunks[cX, cZ];
    }

//    public void GenerateMeshData() {
//        tempMeshBuffer.vertices = new List<Vector3>();
//        tempMeshBuffer.triangles = new List<int>();
//        BlockStack stack;
//        for (var x = 0; x < cSize; x++) {
//            for (var y = 0; y < cHeight; y++) { //TODO swap?
//                for (var z = 0; z < cSize; z++) {
//                    stack = blocks[x, y, z];
//                    if (stack.id == Block.Air.id) continue;
//                    if (stack.ABOVE == Block.Air.id) {
//                        tempMeshBuffer.AddFace(0, 1, 2, 3, 2, 1);
//                        tempMeshBuffer.AddVertex(x, y, z, MeshBuffer.vertexRef[0], MeshBuffer.vertexRef[1], MeshBuffer.vertexRef[2], MeshBuffer.vertexRef[3]);
//                    }
//                        
//                    
////                    if (blocks[x, y, z].id == Block.Air.id) continue;
////                    if (y + 1 < cHeight && blocks[x, y + 1, z].id == Block.Air.id) { // Top Face
////                        tempMeshBuffer.AddFace(0, 1, 2, 3, 2, 1);
////                        tempMeshBuffer.AddVertex(x, y, z, 0, 1, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1);
////                    }
////                    if (y - 1 > 0 && blocks[x, y - 1, z].id == Block.Air.id) { // Bottom Face
////                        tempMeshBuffer.AddFace(0, 2, 1, 3, 1, 2);
////                        tempMeshBuffer.AddVertex(x, y, z, 0, 0, 0, 0, 0, 1, 1, 0, 0, 1, 0, 1);
////                    }
////                    if (x + 1 < cSize && blocks[x + 1, y, z].id == Block.Air.id) { // Back Face
////                        tempMeshBuffer.AddFace(0, 2, 1, 3, 1, 2);
////                        tempMeshBuffer.AddVertex(x, y, z, 1, 0, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1);
////                    }
////                    if (x - 1 > 0 && blocks[x - 1, y, z].id == Block.Air.id) { // Front Face
////                        tempMeshBuffer.AddFace(0, 1, 2, 3, 2, 1);
////                        tempMeshBuffer.AddVertex(x, y, z, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 1);
////                    }
////                    if (z + 1 < cSize && blocks[x, y, z + 1].id == Block.Air.id) { // Right Face
////                        tempMeshBuffer.AddFace(0, 1, 2, 3, 2, 1);
////                        tempMeshBuffer.AddVertex(x, y, z, 0, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1);
////                    }
////                    if (z - 1 > 0 && blocks[x, y, z - 1].id == Block.Air.id) { // Left Face
////                        tempMeshBuffer.AddFace(0, 2, 1, 3, 1, 2);
////                        tempMeshBuffer.AddVertex(x, y, z, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0);
////                    }
//                }
//            }
//        }
//    }

    public void UpdateMesh() {
        mesh.vertices = tempMeshBuffer.vertices.ToArray();
        mesh.triangles = tempMeshBuffer.triangles.ToArray();
        tempMeshBuffer = new MeshBuffer();
        mesh.RecalculateNormals();
        mesh.UploadMeshData(true);
        meshFilter.sharedMesh = mesh;
        //meshCollider.sharedMesh = mesh;
    }
}
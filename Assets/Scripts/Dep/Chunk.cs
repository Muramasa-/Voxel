//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;
//
//public class Chunk {
//    public GameObject gameObject;
//    public Bounds bounds;
//    public Vector3 chunkWorldPos;
//
//    public float[] noiseSet;
//
//    public BlockStack[,,] blocks;
//
//    public ChunkSection[] sections;
//    //public static int sectionCount;
//
//    //public bool requiresMeshUpdate;
//    //public bool requiresDataUpdate;
//
//    //public bool hasBeenGenerated;
//
//    public Mesh mesh = new Mesh();
//    private readonly MeshFilter meshFilter;
//    private readonly MeshCollider meshCollider;
//    private MeshRenderer meshRenderer;
//    public MeshBuffer tempMeshBuffer = new MeshBuffer();
//    public int faceCount = 0;
//
//    public readonly int cX, cY, cZ, cXReal, cYReal, cZReal;
//
//    public static int chunkLayer = LayerMask.NameToLayer("Chunk");
//    public static TerrainGenerator generator;
//    public static int cSize, cHeight;
//    public static Chunk lastMeshedChunk = null;
//    
//    public static byte NONE = 0;
//    public static byte U = 1 << 0;
//    public static byte D = 1 << 1;
//    public static byte L = 1 << 2;
//    public static byte R = 1 << 3;
//    public static byte F = 1 << 4;
//    public static byte B = 1 << 5;
//
//    //TODO USE PREFAB
//
//	public Chunk(Transform parent, int x, int y, int z, Material material) {
//	    var watch = System.Diagnostics.Stopwatch.StartNew();
//	    blocks = new BlockStack[cSize + 2, cHeight + 2, cSize + 2]; //cSize + 2: Used to generate faces across chunk borders
//
//	    sections = new ChunkSection[(cHeight / ChunkSection.size) + 1];
//	    for (int i = 0; i < sections.Length; i++) {
//	        sections[i] = new ChunkSection();
//	    }
//
//        cX = x;
//	    cY = y;
//        cZ = z;
//	    cXReal = x * cSize;
//	    cYReal = y * cHeight;
//	    cZReal = z * cSize;
//	    chunkWorldPos = new Vector3(cXReal, cYReal, cZReal);
//	    bounds = new Bounds(chunkWorldPos, Vector3.one * cSize);
//
//        gameObject = new GameObject(string.Format("Chunk ({0}, {1})", cX, cZ));
//        gameObject.transform.position = chunkWorldPos;
//        gameObject.transform.parent = parent;
//        gameObject.layer = chunkLayer;
//	    gameObject.isStatic = true;
//
//	    mesh.MarkDynamic();
//        meshFilter = gameObject.AddComponent<MeshFilter>();
//        meshCollider = gameObject.AddComponent<MeshCollider>();
//	    meshRenderer = gameObject.AddComponent<MeshRenderer>();
//        meshRenderer.material = material;
//        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.TwoSided;
//
//	    //generator.RequestMeshData(this);
//
//
//	    noiseSet = generator.fastNoise.GetNoiseSet(cXReal, cYReal, cZReal, cSize, cHeight, cSize); //45ms
//	    GenerateBlockData(); //167ms (231 total)
//	    GenerateMeshData(); //255ms
//	    //GenerateChunk();
//	    UpdateMesh(); //12ms
//
//	    watch.Stop();
//	    //Debug.Log(watch.ElapsedMilliseconds);
//	}
//
//    /*private void GenerateChunkNoise() {
//        //float[,,] noise = new float[cSize + 2, cSize, cSize + 2];
//        //int cSizeWithBorder = cSize + 1;
//        //float[] chunkNoiseSet = generator.fastNoise.GetNoiseSet(cXReal, cYReal, cZReal, cSize, cSize, cSize);
//        int index = 0;
//        for (var x = 0; x < cSize; x++) {
//            for (var y = 0; y < cSize; y++) {
//                for (var z = 0; z < cSize; z++) {
//                    //float sample = generator.fastNoise.GetNoise(x + cXReal, y, z + cZReal);
//                    //sample += -(((float) y / cHeight) );
//                    //noise[x == -1 ? cWidth : x == cWidth ? cSizeWithBorder : x, y, z == -1 ? cWidth : z == cWidth ? cSizeWithBorder : z] = sample;
//
//                    noise[x, y, z] = noiseSet[index++];
//                }
//            }
//        }
//        //return noise;
//    }*/
//
//    public float GetNoiseValue(int x, int y, int z) {
//        return noiseSet[x * cHeight * cSize + y * cSize + z];
//    }
//
////    public int GetBlock(int x, int y, int z) {
////        if (blocks[x, y, z] == null) {
////            if (GetNoiseValue(x, y, z) >= 0) {
////                blocks[x, y, z] = new BlockStack(Block.Stone.id);
////            } else {
////                blocks[x, y, z] = new BlockStack(Block.Air.id);
////            }
////        }
////        return blocks[x, y, z].id;
////    }
//
//    public void GenerateBlockData() {
//        int nIndex = 0;
//        for (var x = 0; x < cSize; x++) {
//            for (var y = 0; y < cHeight; y++) { //Looping over Y first changes noise?
//                for (var z = 0; z < cSize; z++) {
//                    //int sectionIndex = cHeight / ChunkSection.size;
//                    //sections[sectionIndex].yStart = ChunkSection.size * sectionIndex;
//                    if (noiseSet[nIndex++] >= 0) {
//                        blocks[x, y, z] = new BlockStack(Block.Stone.id);
//                    } else {
//                        blocks[x, y, z] = new BlockStack(Block.Air.id);
//                    }
//                }
//            }
//        }
//    }
//
//    // private void GenerateBlockDataSection() {
//    //     int index = 0;
//    //     int currentBlockId = -1;
//    //     int blockCount = 0;
//    //
//    //
//    //     for (int x = 0; x < ChunkSection.size; x++) {
//    //         for (int y = 0; y < cHeight; y++) {
//    //             int sectionIndex = y / ChunkSection.size;
//    //             int fixedY = y - sectionIndex * ChunkSection.size;
//    //             for (int z = 0; z < ChunkSection.size; z++) {
//    //                 if (noiseSet[index++] >= 0) {
//    //                     //sections[sectionIndex].blocks[x, fixedY, z] = new BlockStack(Block.Stone.id);
//    //                     if (currentBlockId == -1) {
//    //                         currentBlockId = Block.Stone.id;
//    //                     } else if (currentBlockId == Block.Stone.id) {
//    //                         blockCount++;
//    //                     }
//    //                 } else {
//    //                     //sections[sectionIndex].blocks[x, fixedY, z] = new BlockStack(Block.Air.id);
//    //                     if (currentBlockId == -1) {
//    //                         currentBlockId = Block.Air.id;
//    //                     } else if (currentBlockId == Block.Air.id) {
//    //                         blockCount++;
//    //                     }
//    //                 }
//    //             }
//    //             if (fixedY == ChunkSection.size - 1) { //End of a chunk section
//    //                 sections[sectionIndex].hasDominantId = blockCount == ChunkSection.totalSectionBlockCount;
//    //                 sections[sectionIndex].dominantId = currentBlockId;
//    //             }
//    //         }
//    //     }
//    // }
//
//    // public void GenerateMeshData() {
//    //     for (var x = 0; x < cSize; x++) {
//    //         for (var y = 0; y < cHeight; y++) {
//    //             for (var z = 0; z < cSize; z++) {
//    //                 if (blocks[x, y, z].id == 1) {
//    //                    MeshHelper.SetVoxelConfig(this, x, y, z);
//    //                 }
//    //             }
//    //         }
//    //     }
//    //     //tempMeshData.InitArrays();
//    //     mesh.vertices = new Vector3[faceCount * 4];
//    //     mesh.triangles = new int[faceCount * 6];
//    //     MeshHelper.triangleCount = MeshHelper.vertexCount = 0;
//    //
//    //     for (var x = 0; x < cSize; x++) {
//    //         for (var y = 0; y < cHeight; y++) {
//    //             for (var z = 0; z < cSize; z++) {
//    //                 MeshHelper.SetVoxelData(this, blocks[x, y, z].faceConfig, x, y, z);
//    //                 //tempMeshData.SetVoxelData(blocks[x, y, z].faceConfig, x, y, z);
//    //             }
//    //         }
//    //     }
//    //     //lastMeshedChunk = generator.currentLoadedChunks[cX, cZ];
//    // }
//
//    public void GenerateMeshData() {
////        for (var x = 0; x < cSize; x++) {
////            for (var y = 0; y < cHeight; y++) {
////                for (var z = 0; z < cSize; z++) {
////                    if (blocks[x, y, z].id == 1) {
////                        blocks[x, y, z].faceConfig = GetFaceConfig(x, y, z);
////                    }
////                }
////            }
////        }
//        //tempMeshBuffer.vertices = new Vector3[64998/*faceCount * 4*/];
//        //tempMeshBuffer.triangles = new int[64998/*faceCount * 6*/];
//        tempMeshBuffer.vertices = new List<Vector3>();
//        tempMeshBuffer.triangles = new List<int>();
//        for (var x = 0; x < cSize; x++) {
//            for (var y = 0; y < cHeight; y++) {
//                for (var z = 0; z < cSize; z++) {
//                    if (GetBlock(x, y + 1, z) == Block.Air.id) { // Top Face
//                        tempMeshBuffer.AddFace(0, 1, 2, 3, 2, 1);
//                        tempMeshBuffer.AddVertex(x, y, z, 
0, 1, 0, 
0, 1, 1, 
1, 1, 0, 
1, 1, 1);
//                    }
//                    if (GetBlock(x, y - 1, z) == Block.Air.id) { // Bottom Face
//                        tempMeshBuffer.AddFace(0, 2, 1, 3, 1, 2);
//                        tempMeshBuffer.AddVertex(x, y, z, 
0, 0, 0, 
0, 0, 1, 
1, 0, 0, 
1, 0, 1);
//                    }
//                    if (GetBlock(x + 1, y, z) == Block.Air.id) { // Back Face
//                        tempMeshBuffer.AddFace(0, 2, 1, 3, 1, 2);
//                        tempMeshBuffer.AddVertex(x, y, z, 1, 0, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1);
//                    }
//                    if (GetBlock(x - 1, y, z) == Block.Air.id) { // Front Face
//                        tempMeshBuffer.AddFace(0, 1, 2, 3, 2, 1);
//                        tempMeshBuffer.AddVertex(x, y, z, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 1);
//                    }
//                    if (GetBlock(x, y, z + 1) == Block.Air.id) { // Right Face
//                        tempMeshBuffer.AddFace(0, 1, 2, 3, 2, 1);
//                        tempMeshBuffer.AddVertex(x, y, z, 0, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1);
//                    }
//                    if (GetBlock(x, y, z - 1) == Block.Air.id) { // Left Face
//                        tempMeshBuffer.AddFace(0, 2, 1, 3, 1, 2);
//                        tempMeshBuffer.AddVertex(x, y, z, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0);
//                    }
//                }
//            }
//        }
//        //lastMeshedChunk = generator.currentLoadedChunks[cX, cZ];
//    }
//
//    public void Tick() {
//        // if (!hasBeenGenerated) {
//        //     generator.RequestMeshData(UpdateMesh);
//        //     hasBeenGenerated = true;
//        // } else if (requiresDataUpdate) {
//        //     Debug.Log("Rebuilding Chunk: " + gameObject.transform.name);
//        //     UpdateMesh(GenerateMeshData());
//        //     requiresDataUpdate = false;
//        // } else if (requiresMeshUpdate) {
//        //     UpdateMesh(chunkMeshData);
//        //     requiresMeshUpdate = false;
//        // }
//    }
//
//    public void UpdateMesh() {
//        mesh.Clear();
//        mesh.vertices = tempMeshBuffer.vertices.ToArray();
//        mesh.triangles = tempMeshBuffer.triangles.ToArray();
//        tempMeshBuffer = new MeshBuffer();
//        mesh.UploadMeshData(false);
//        mesh.RecalculateNormals();
//        MeshUtility.Optimize(mesh);
//        meshFilter.sharedMesh = mesh;
//        //meshCollider.sharedMesh = mesh;
//    }
//    
//    public static int cSizeNeg = cSize - 1;
//    public static int airId = Block.Air.id;
//
////    public FaceConfig GetFaceConfigNew(int x, int y, int z) {
////        return FaceConfig.NONE;
////    }
//
//    public sbyte GetBlock(int x, int y, int z) {
//        if (x >= cSize || x <= 0 || y >= cHeight || y <= 0 || z >= cSize || z <= 0) return Block.Invalid.id;
//        BlockStack stack = blocks[x, y, z];
//        return stack != null ? stack.id : Block.Invalid.id;
//    }
//    
//    //public void AddFaceConfig
//    
//    
//    public byte GetFaceConfig(int x, int y, int z) {
//        byte faceConfig = NONE;
//        if (GetBlock(x, y + 1, z) == Block.Air.id) {
//            faceConfig |= U;
//            faceCount++;
//        }
//        if (GetBlock(x, y - 1, z) == Block.Air.id) {
//            faceConfig |= D;
//            faceCount++;
//        }
//        if (GetBlock(x + 1, y, z) == Block.Air.id) {
//            faceConfig |= B;
//            faceCount++;
//        }
//        if (GetBlock(x - 1, y, z) == Block.Air.id) {
//            faceConfig |= F;
//            faceCount++;
//        }
//        if (GetBlock(x, y, z + 1) == Block.Air.id) {
//            faceConfig |= R;
//            faceCount++;
//        }
//        if (GetBlock(x, y, z - 1) == Block.Air.id) {
//            faceConfig |= L;
//            faceCount++;
//        }
////        if (y < cHeight - 1 && blocks[x, y + 1, z].id == airId) { // Top Face
////            faceConfig |= FaceConfig.U;
////            faceCount++;
////        }
////        if (y > 0 && blocks[x, y - 1, z].id == airId) { // Bottom Face
////            faceConfig |= FaceConfig.D;
////            faceCount++;
////        }
////        if (x < cSize && blocks[x + 1, y, z].id == airId) { // Back Face BROKEN
////            faceConfig |= FaceConfig.B;
////            faceCount++;
////        }
////        if (x > 0 && blocks[x - 1, y, z].id == airId) { // Front Face
////            faceConfig |= FaceConfig.F;
////            faceCount++;
////        }
////        if (z < cSize && blocks[x, y, z + 1].id == airId) { // Right Face BROKEN
////            faceConfig |= FaceConfig.R;
////            faceCount++;
////        }
////        if (z > 0 && blocks[x, y, z - 1].id == airId) { // Left Face ?
////            faceConfig |= FaceConfig.L;
////            faceCount++;
////        }
//        return faceConfig;
//    }
//}
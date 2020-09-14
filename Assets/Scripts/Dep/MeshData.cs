// using System.CodeDom.Compiler;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class MeshData {
//     public static int cSize = ChunkSection.size;
//     public static int cHeight = Chunk.cHeight;
//     public static int cSizeNeg = cSize - 1;
//     public static int airId = Block.Air.id;
//
//     public Vector3[] vertices;
//     public int[] triangles;
//
//     public List<Vector3> vertexList = new List<Vector3>();
//     public List<int> triangleList = new List<int>();
//
//     public float xf, yf, zf;
//     //public Vector3 posVec = new Vector3(-0.5f, -0.5f, -0.5f);
//     public Vector3 posVec = Vector3.one;
//     //public short faceCount;
//
//     public void InitArrays() {
//         //triangles = new int[faceCount * 6];
//         //vertices = new Vector3[faceCount * 4];
//     }
//
//     public int triangleCount = 0;
//     public void AddFace(int a, int b, int c, int d, int e, int f) {
//         triangles[triangleCount + 0] = vertexCount + a;
//         triangles[triangleCount + 1] = vertexCount + b;
//         triangles[triangleCount + 2] = vertexCount + c;
//         triangles[triangleCount + 3] = vertexCount + d;
//         triangles[triangleCount + 4] = vertexCount + e;
//         triangles[triangleCount + 5] = vertexCount + f;
//         triangleCount += 6;
//     }
//
//
//     public int vertexCount = 0;
//     public void AddVertex(int x, int y, int z, int a, int b, int c, int d, int e, int f, int g, int h, int i, int j, int k, int l) {
//         xf = x - 0.5f; yf = y - 0.5f; zf = z - 0.5f;
//         vertices[vertexCount + 0] = new Vector3(xf + a, yf + b, zf + c);
//         vertices[vertexCount + 1] = new Vector3(xf + d, yf + e, zf + f);
//         vertices[vertexCount + 2] = new Vector3(xf + g, yf + h, zf + i);
//         vertices[vertexCount + 3] = new Vector3(xf + j, yf + k, zf + l);
//         vertexCount += 4;
//     }
//
//     public void AddVertexNew(int x, int y, int z, Vector3 a, Vector3 b, Vector3 c, Vector3 d) {
//         posVec.Set(x - 0.5f, y - 0.5f, z - 0.5f);
//         //xf = x - 0.5f; yf = y - 0.5f; zf = z - 0.5f;
//         vertices[vertexCount + 0] = posVec + a;
//         vertices[vertexCount + 1] = posVec + b;
//         vertices[vertexCount + 2] = posVec + c;
//         vertices[vertexCount + 3] = posVec + d;
//         vertexCount += 4;
//     }
//
//     public void AddFaceList(int a, int b, int c, int d, int e, int f) {
//         vertexCount = vertexList.Count;
//         triangleList.Add(vertexCount + a);
//         triangleList.Add(vertexCount + b);
//         triangleList.Add(vertexCount + c);
//         triangleList.Add(vertexCount + d);
//         triangleList.Add(vertexCount + e);
//         triangleList.Add(vertexCount + f);
//     }
//
//     public void AddVertexList(int x, int y, int z, int a, int b, int c, int d, int e, int f, int g, int h, int i, int j, int k, int l) {
//         xf = x - 0.5f; yf = y - 0.5f; zf = z - 0.5f;
//         vertexList.Add(new Vector3(xf + a, yf + b, zf + c));
//         vertexList.Add(new Vector3(xf + d, yf + e, zf + f));
//         vertexList.Add(new Vector3(xf + g, yf + h, zf + i));
//         vertexList.Add(new Vector3(xf + j, yf + k, zf + l));
//     }
//
//     public static MeshData[] meshDataConfigs = { };
//
//     public void SetVoxelAll(Chunk chunk, int x, int y, int z) {
//         if (y < cSizeNeg && chunk.GetBlock(x, y + 1, z) == airId) { // Top Face
//             AddFaceList(0, 1, 2, 3, 2, 1);
//             AddVertexList(x, y, z, 0, 1, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1);
//         }
//         if (y > 0 && chunk.GetBlock(x, y - 1, z) == airId) { // Bottom Face
//             AddFaceList(0, 2, 1, 3, 1, 2);
//             AddVertexList(x, y, z, 0, 0, 0, 0, 0, 1, 1, 0, 0, 1, 0, 1);
//         }
//         if (x < cSizeNeg && chunk.GetBlock(x + 1, y, z) == airId) { // Back Face
//             AddFaceList(0, 2, 1, 3, 1, 2);
//             AddVertexList(x, y, z, 1, 0, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1);
//         }
//         if (x > 0 && chunk.GetBlock(x - 1, y, z) == airId) { // Front Face
//             AddFaceList(0, 1, 2, 3, 2, 1);
//             AddVertexList(x, y, z, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 1);
//         }
//         if (z < cSizeNeg && chunk.GetBlock(x, y, z + 1) == airId) { // Right Face
//             AddFaceList(0, 1, 2, 3, 2, 1);
//             AddVertexList(x, y, z, 0, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1);
//         }
//         if (z > 0 && chunk.GetBlock(x, y, z - 1) == airId) { // Left Face
//             AddFaceList(0, 2, 1, 3, 1, 2);
//             AddVertexList(x, y, z, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0);
//         }
//     }
//
//     public FaceConfig SetVoxelConfig(Chunk chunk, FaceConfig faceConfig, int x, int y, int z) {
//         if (y < cHeight - 1 && chunk.blocks[x, y + 1, z].id == airId) { // Top Face
//             faceConfig |= FaceConfig.U;
//             chunk.faceCount++;
//         }
//         if (y > 0 && chunk.blocks[x, y - 1, z].id == airId) { // Bottom Face
//             faceConfig |= FaceConfig.D;
//             chunk.faceCount++;
//         }
//         if (x < cSizeNeg && chunk.blocks[x + 1, y, z].id == airId) { // Back Face
//             faceConfig |= FaceConfig.B;
//             chunk.faceCount++;
//         }
//         if (x > 0 && chunk.blocks[x - 1, y, z].id == airId) { // Front Face
//             faceConfig |= FaceConfig.F;
//             chunk.faceCount++;
//         }
//         if (z < cSizeNeg && chunk.blocks[x, y, z + 1].id == airId) { // Right Face
//             faceConfig |= FaceConfig.R;
//             chunk.faceCount++;
//         }
//         if (z > 0 && chunk.blocks[x, y, z - 1].id == airId) { // Left Face
//             faceConfig |= FaceConfig.L;
//             chunk.faceCount++;
//         }
//         return faceConfig;
//     }
//
//     public static Vector3[] vecterRef = {
//         new Vector3(0, 1, 0), new Vector3(0, 1, 1), new Vector3(1, 1, 0), new Vector3(1, 1, 1),
//         new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 0), new Vector3(1, 0, 1),
//     };
//
//     public void SetVoxelData(FaceConfig faceConfig, int x, int y, int z) {
//         if ((faceConfig & FaceConfig.U) != 0) { // Top Face
//             AddFace(0, 1, 2, 3, 2, 1);
//             //AddVertexNew(x, y, z, vecterRef[0], vecterRef[1], vecterRef[2], vecterRef[3]);
//         }
//         if ((faceConfig & FaceConfig.D) != 0) { // Bottom Face
//             AddFace(0, 2, 1, 3, 1, 2);
//             //AddVertexNew(x, y, z, vecterRef[4], vecterRef[5], vecterRef[6], vecterRef[7]);
//         }
//         if ((faceConfig & FaceConfig.B) != 0) { // Back Face
//             AddFace(0, 2, 1, 3, 1, 2);
//             //AddVertexNew(x, y, z, vecterRef[6], vecterRef[7], vecterRef[2], vecterRef[3]);
//         }
//         if ((faceConfig & FaceConfig.F) != 0) { // Front Face
//             AddFace(0, 1, 2, 3, 2, 1);
//             //AddVertexNew(x, y, z, vecterRef[4], vecterRef[5], vecterRef[0], vecterRef[1]);
//         }
//         if ((faceConfig & FaceConfig.R) != 0) { // Right Face
//             AddFace(0, 1, 2, 3, 2, 1);
//             //AddVertexNew(x, y, z, vecterRef[5], vecterRef[7], vecterRef[1], vecterRef[3]);
//         }
//         if ((faceConfig & FaceConfig.L) != 0) { // Left Face
//             AddFace(0, 2, 1, 3, 1, 2);
//             //AddVertexNew(x, y, z, vecterRef[4], vecterRef[6], vecterRef[0], vecterRef[2]);
//         }
//     }
// }
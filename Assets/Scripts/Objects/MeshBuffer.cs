using System.Collections.Generic;
using UnityEngine;

public struct MeshBuffer {
    //public Vector3[] vertices;
    //public int[] triangles;

    public List<Vector3> vertices;
    public List<int> triangles;

    float xf, yf, zf;
    int vertexCount;
    private Vector3 pos;

    static readonly int tA = 0;
    static readonly int tB = 1;
    static readonly int tC = 2;
    static readonly int tD = 3;
    
    static readonly Vector3 vA = new Vector3(0, 1, 0);
    static readonly Vector3 vB = new Vector3(0, 1, 1);
    static readonly Vector3 vC = new Vector3(1, 1, 0);
    static readonly Vector3 vD = new Vector3(1, 1, 1);
    static readonly Vector3 vE = new Vector3(0, 0, 0);
    static readonly Vector3 vF = new Vector3(0, 0, 1);
    static readonly Vector3 vG = new Vector3(1, 0, 0);
    static readonly Vector3 vH = new Vector3(1, 0, 1);

    public void AddVoxel(Chunk chunk, int x, int y, int z) {
        pos = new Vector3(x + 0.5f, y + 0.5f, z + 0.5f);
        if (chunk.GetBlock(x, y + 1, z) == Block.Air.id) { // Top Face
            AddFace(tA, tB, tC, tD, tC, tB);
            AddVertex(vA, vB, vC, vD);
        }
        if (chunk.GetBlock(x, y - 1, z) == Block.Air.id) { // Bottom Face
            AddFace(tA, tC, tB, tD, tB, tC);
            AddVertex(vE, vF, vG, vH);
        }
        if (chunk.GetBlock(x + 1, y, z) == Block.Air.id) { // Back Face
            AddFace(tA, tC, tB, tD, tB, tC);
            AddVertex(vG, vH, vC, vD);
        }
        if (chunk.GetBlock(x - 1, y, z) == Block.Air.id) { // Front Face
            AddFace(tA, tB, tC, tD, tC, tB);
            AddVertex(vE, vF, vA, vB);
        }
        if (chunk.GetBlock(x, y, z + 1) == Block.Air.id) { // Right Face
            AddFace(tA, tB, tC, tD, tC, tB);
            AddVertex(vF, vH, vB, vD);
        }
        if (chunk.GetBlock(x, y, z - 1) == Block.Air.id) { // Left Face
            AddFace(tA, tC, tB, tD, tB, tC);
            AddVertex(vE, vG, vA, vC);
        }
    }
    
    public void AddFace(int a, int b, int c, int d, int e, int f) {
        triangles.Add(vertexCount + a);
        triangles.Add(vertexCount + b);
        triangles.Add(vertexCount + c);
        triangles.Add(vertexCount + d);
        triangles.Add(vertexCount + e);
        triangles.Add(vertexCount + f);
    }
    
    public void AddVertex(Vector3 a, Vector3 b, Vector3 c, Vector3 d) {
        vertices.Add(a + pos);
        vertices.Add(b + pos);
        vertices.Add(c + pos);
        vertices.Add(d + pos);
        vertexCount += 4;
    }

//    public void AddVoxel(byte faceConfig, int x, int y, int z) {
//        if (faceConfig == NONE) return;
//        if ((faceConfig & U) != 0) { // Top Face
//            AddFace(0, 1, 2, 3, 2, 1);
//            AddVertex(x, y, z, 0, 1, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1);
//        }
//        if ((faceConfig & D) != 0) { // Bottom Face
//            AddFace(0, 2, 1, 3, 1, 2);
//            AddVertex(x, y, z, 0, 0, 0, 0, 0, 1, 1, 0, 0, 1, 0, 1);
//        }
//        if ((faceConfig & B) != 0) { // Back Face
//            AddFace(0, 2, 1, 3, 1, 2);
//            AddVertex(x, y, z, 1, 0, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1);
//        }
//        if ((faceConfig & F) != 0) { // Front Face
//            AddFace(0, 1, 2, 3, 2, 1);
//            AddVertex(x, y, z, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 1);
//        }
//        if ((faceConfig & R) != 0) { // Right Face
//            AddFace(0, 1, 2, 3, 2, 1);
//            AddVertex(x, y, z, 0, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1);
//        }
//        if ((faceConfig & L) != 0) { // Left Face
//            AddFace(0, 2, 1, 3, 1, 2);
//            AddVertex(x, y, z, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0);
//        }
//    }
    
//    public void AddVoxel(byte faceConfig, int x, int y, int z) {
//        if (faceConfig == NONE) return;
//        if ((faceConfig & U) != 0) { // Top Face
//            AddFace(0, 1, 2, 3, 2, 1);
//            AddVertex(x, y, z, 0, 1, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1);
//        }
//        if ((faceConfig & D) != 0) { // Bottom Face
//            AddFace(0, 2, 1, 3, 1, 2);
//            AddVertex(x, y, z, 0, 0, 0, 0, 0, 1, 1, 0, 0, 1, 0, 1);
//        }
//        if ((faceConfig & B) != 0) { // Back Face
//            AddFace(0, 2, 1, 3, 1, 2);
//            AddVertex(x, y, z, 1, 0, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1);
//        }
//        if ((faceConfig & F) != 0) { // Front Face
//            AddFace(0, 1, 2, 3, 2, 1);
//            AddVertex(x, y, z, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 1);
//        }
//        if ((faceConfig & R) != 0) { // Right Face
//            AddFace(0, 1, 2, 3, 2, 1);
//            AddVertex(x, y, z, 0, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1);
//        }
//        if ((faceConfig & L) != 0) { // Left Face
//            AddFace(0, 2, 1, 3, 1, 2);
//            AddVertex(x, y, z, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0);
//        }
//    }
}

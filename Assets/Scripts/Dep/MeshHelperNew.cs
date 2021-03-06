﻿//using System.Collections.Generic;
//using UnityEngine;
//
//public static class MeshHelperNew {
//    //public List<Vector3> vertices;
//    //public List<int> triangles;
//
////    public Vector3[] vertices;
////    public int[] triangles;
////
//    public static int cSize = ChunkSection.size;
//    public static int cHeight = Chunk.cHeight;
//    public static int cSizeNeg = cSize - 1;
//    public static int airId = Block.Air.id;
////    public float xf, yf, zf;
////
////    public int faceCount = 0;
////    
////    public void InitArrays() {
////        vertices = new Vector3[faceCount * 4];
////        triangles = new int[faceCount * 6];
////    }
//
//    private int triangleCount = 0;
//    private void AddFace(Chunk chunk, int a, int b, int c, int d, int e, int f) {
//        triangles[triangleCount + 0] = vertexCount + a;
//        triangles[triangleCount + 1] = vertexCount + b;
//        triangles[triangleCount + 2] = vertexCount + c;
//        triangles[triangleCount + 3] = vertexCount + d;
//        triangles[triangleCount + 4] = vertexCount + e;
//        triangles[triangleCount + 5] = vertexCount + f;
//        triangleCount += 6;
//    }
//
//    private int vertexCount = 0;
//    private void AddVertex(int x, int y, int z, int a, int b, int c, int d, int e, int f, int g, int h, int i, int j, int k, int l) {
//        xf = x - 0.5f; yf = y - 0.5f; zf = z - 0.5f;
//        vertices[vertexCount + 0] = new Vector3(xf + a, yf + b, zf + c);
//        vertices[vertexCount + 1] = new Vector3(xf + d, yf + e, zf + f);
//        vertices[vertexCount + 2] = new Vector3(xf + g, yf + h, zf + i);
//        vertices[vertexCount + 3] = new Vector3(xf + j, yf + k, zf + l);
//        vertexCount += 4;
//    }
//    
//    public static FaceConfig GetVoxelConfig(Chunk chunk, int x, int y, int z) {
//        FaceConfig faceConfig = FaceConfig.NONE;
//        if (y < cHeight - 1 && chunk.blocks[x, y + 1, z].id == airId) { // Top Face
//            faceConfig |= FaceConfig.U;
//            chunk.faceCount++;
//        }
//        if (y > 0 && chunk.blocks[x, y - 1, z].id == airId) { // Bottom Face
//            faceConfig |= FaceConfig.D;
//            chunk.faceCount++;
//        }
//        if (x < cSizeNeg && chunk.blocks[x + 1, y, z].id == airId) { // Back Face
//            faceConfig |= FaceConfig.B;
//            chunk.faceCount++;
//        }
//        if (x > 0 && chunk.blocks[x - 1, y, z].id == airId) { // Front Face
//            faceConfig |= FaceConfig.F;
//            chunk.faceCount++;
//        }
//        if (z < cSizeNeg && chunk.blocks[x, y, z + 1].id == airId) { // Right Face
//            faceConfig |= FaceConfig.R;
//            chunk.faceCount++;
//        }
//        if (z > 0 && chunk.blocks[x, y, z - 1].id == airId) { // Left Face
//            faceConfig |= FaceConfig.L;
//            chunk.faceCount++;
//        }
//        return faceConfig;
//    }
//
//    public void AddVoxel(FaceConfig faceConfig, int x, int y, int z) {
//        if ((faceConfig & FaceConfig.U) != 0) { // Top Face
//            AddFace(0, 1, 2, 3, 2, 1);
//            AddVertex(x, y, z, 0, 1, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1);
//        }
//        if ((faceConfig & FaceConfig.D) != 0) { // Bottom Face
//            AddFace(0, 2, 1, 3, 1, 2);
//            AddVertex(x, y, z, 0, 0, 0, 0, 0, 1, 1, 0, 0, 1, 0, 1);
//        }
//        if ((faceConfig & FaceConfig.B) != 0) { // Back Face
//            AddFace(0, 2, 1, 3, 1, 2);
//            AddVertex(x, y, z, 1, 0, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1);
//        }
//        if ((faceConfig & FaceConfig.F) != 0) { // Front Face
//            AddFace(0, 1, 2, 3, 2, 1);
//            AddVertex(x, y, z, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 1);
//        }
//        if ((faceConfig & FaceConfig.R) != 0) { // Right Face
//            AddFace(0, 1, 2, 3, 2, 1);
//            AddVertex(x, y, z, 0, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1);
//        }
//        if ((faceConfig & FaceConfig.L) != 0) { // Left Face
//            AddFace(0, 2, 1, 3, 1, 2);
//            AddVertex(x, y, z, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0);
//        }
//    }
//}

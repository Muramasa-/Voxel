﻿using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
    [HideInInspector] public long chunkTimeTotal;
    [HideInInspector] public long chunkTimeNoise;
    [HideInInspector] public long chunkTimeVoxels;
    [HideInInspector] public long chunkTimeMesh;

    //[HideInInspector] public Chunk[,,] chunks;
    [HideInInspector] public GameObject chunkHolder;
    public static Material chunkMaterial;
    public static Material chunkMaterialEdge;
    public bool enableDynamicChunks = true;

    public bool enableRandomSeeds;

    public int cSize;
    public int cHeight;
    [Range(1, 32)] public int cGenRadius;
    //[Range(1, 32)] public int cGenRadiusY;
    //public int maxViewMulti;

    public FastNoiseSIMD fastNoise = new FastNoiseSIMD();

    const float viewerMoveThresholdForChunkUpdate = 25f;
    const float sqrViewerMoveThresholdForChunkUpdate = viewerMoveThresholdForChunkUpdate * viewerMoveThresholdForChunkUpdate;

    [HideInInspector] public float maxViewDist;
    [HideInInspector] public int chunksVisibleInViewDist;

    public Transform viewer;
    [HideInInspector] public Vector3 viewerPosition = Vector3.zero;
    [HideInInspector] public Vector3 viewerPositionOld = Vector3.zero;

    List<Chunk> terrainChunksVisibleLastUpdate = new List<Chunk>();

    public Chunk[,] currentLoadedChunks;

    public Queue<Chunk> chunkMeshDataQueue = new Queue<Chunk>();

    void Awake() {
        Chunk.chunkPrefab = Resources.Load<GameObject>("Chunk");
        chunkHolder = new GameObject("Chunks");
        chunkMaterial = Resources.Load<Material>("ChunkMat"); //#383535FF
        chunkMaterialEdge = Resources.Load<Material>("ChunkMatEdge"); //#383535FF
        //maxViewDist = cSize * 8;
        chunksVisibleInViewDist = 4;//Mathf.RoundToInt(maxViewDist / cSize);

        currentLoadedChunks = new Chunk[cGenRadius, cGenRadius];

        fastNoise.SetSeed(0);
        fastNoise.SetFractalOctaves(2);
        fastNoise.SetFractalGain(0.5f);
    }

    void Start() {
        UpdateVisibleChunks();
    }

    void Update() {
        /*if (enableDynamicChunks) {
            viewerPosition = new Vector3(viewer.position.x, viewer.position.y, viewer.position.z);
            if ((viewerPositionOld - viewerPosition).sqrMagnitude > sqrViewerMoveThresholdForChunkUpdate) {
                viewerPositionOld = viewerPosition;
                UpdateVisibleChunks();
            }
        }*/
        /*if (chunkMeshDataQueue.Count > 0) {
            for (int i = 0; i < chunkMeshDataQueue.Count; i++) {
                chunkMeshDataQueue.Dequeue().UpdateMesh();
            }
        } */
    }

    public void UpdateVisibleChunks() {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        CreateChunkObjects(); //43ms
        watch.Stop();
        chunkTimeTotal += watch.ElapsedMilliseconds;
        //TickChunks(); //312ms

        Debug.Log("Total Time All Chunks: " + chunkTimeTotal + "ms (" + chunkTimeTotal / currentLoadedChunks.Length + "ms avg)");
        Debug.Log("Total Chunks: " + currentLoadedChunks.Length);
    }

    void CreateChunkObjects() {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        /*for (var i = 0; i < terrainChunksVisibleLastUpdate.Count; i++) {
            terrainChunksVisibleLastUpdate[i].gameObject.SetActive(false);
        }
        currentLoadedChunks = new Chunk[cGenRadius, cGenRadius];
        terrainChunksVisibleLastUpdate.Clear();*/

        int cX = Mathf.RoundToInt(viewerPosition.x / cSize);
        int cZ = Mathf.RoundToInt(viewerPosition.z / cSize);



        //TODO CHUNK GAMEOBJS ARE BEING CREATED REGARDLESS IF ALREADY EXISITNG

        for (int radiusOffsetX = 0; radiusOffsetX < cGenRadius; radiusOffsetX++) {
            for (int radiusOffsetZ = 0; radiusOffsetZ < cGenRadius; radiusOffsetZ++) {
                Chunk chunk = new Chunk(Instantiate(Chunk.chunkPrefab, chunkHolder.transform), chunkHolder.transform, cX + radiusOffsetX, 0, cZ + radiusOffsetZ, chunkMaterial);
                //Debug.log(chunk);


                /*if (currentLoadedChunks[radiusOffsetX, radiusOffsetZ] != null) {
                    chunk.gameObject.SetActive(MuraUtils.ChunkDistToViewer(chunk.bounds, viewerPosition, maxViewDist));
                    terrainChunksVisibleLastUpdate.Add(chunk);
                } else {
                    currentLoadedChunks[radiusOffsetX, radiusOffsetZ] = chunk;
                }*/
            }
        }
        watch.Stop();
        Debug.Log(watch.ElapsedMilliseconds);
    }

    // private void TickChunks() {
    //     foreach (var chunk in currentLoadedChunks) {
    //         if (chunk == null) continue; //TODO remove...
    //         //if (chunk.gameObject.activeSelf) {
    //             chunk.Tick();
    //         //}
    //     }
    // }

    // public void RequestMeshData(Chunk chunk) {
    //     ThreadStart threadStart = delegate { MeshDataThread(chunk); };
    //     new Thread(threadStart).Start();
    // }
    //
    // //TODO improve from Sebs repo
    // private void MeshDataThread(Chunk chunk) {
    //     chunk.noiseSet = fastNoise.GetNoiseSet(chunk.cXReal, chunk.cYReal, chunk.cZReal, cSize, cHeight, cSize);
    //     chunk.GenerateBlockData();
    //     chunk.GenerateMeshData();
    //     lock (chunkMeshDataQueue) chunkMeshDataQueue.Enqueue(chunk);
    // }
}                
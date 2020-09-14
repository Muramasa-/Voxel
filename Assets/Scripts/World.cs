using System.Collections.Generic;
using UnityEngine;

public class World {
    //public List<Chunk> allChunks = new List<Chunk>();

	public World() {

	}

	public void Init() {
        //for (int i = 0; i < allChunks.Count; i++) if (allChunks[i] != null) allChunks[i].Init();
	}

	public void Tick() {
        //for (int i = 0; i < allChunks.Count; i++) if (allChunks[i] != null) allChunks[i].Tick();
	}

    public Chunk GetChunk(Vector3 chunkPos) {
        //for (int i = 0; i < allChunks.Count; i++) if (allChunks[i] != null && Vector3.Distance(allChunks[i].chunkPos, chunkPos) <= 0.001f) return allChunks[i];
        return null;
    }
}

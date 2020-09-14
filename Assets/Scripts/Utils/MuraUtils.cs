using System;
using UnityEngine;

public class MuraUtils {
    
    public static bool ChunkDistToViewer(Bounds bounds, Vector3 viewPos, float maxViewDist) {
        var viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance (viewPos));
        return viewerDstFromNearestEdge <= maxViewDist;
    }

    public static Vector3 WorldPosToLocalPos(Vector3 worldPos) {
        return Vector3.zero;
    }
    
    public struct ThreadInfo<T> {
        public readonly Action<T> callback;
        public readonly T parameter;

        public ThreadInfo (Action<T> callback, T parameter) {
            this.callback = callback;
            this.parameter = parameter;
        }
    }
}
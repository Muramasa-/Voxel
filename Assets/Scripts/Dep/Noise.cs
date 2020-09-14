using UnityEngine;

public static class Noise {
    public static FastNoise fastNoise = new FastNoise();
    public static System.Random prng = new System.Random (seed);

	public static float scale = 20;
	public static int octaves = 1;
	public static float persistence = 0.9f;
	public static float lacunarity = 2f;
	public static int seed = 0;
	public static Vector3 offset = Vector3.zero;

    public static float[,,] GenerateNoiseMapMarch(int chunkSize, int chunkHeight, float chunkPosX, float chunkPosZ) {
        var noiseMap = new float[chunkSize, chunkHeight, chunkSize];

        var sampleCentre = new Vector3(chunkPosX, 0, chunkPosZ) * chunkSize;
        for (var y = 0; y < chunkHeight; y++) {
            for (var x = 0; x < chunkSize; x++) {
                for (var z = 0; z < chunkSize; z++) {
                    /*if (y == 0) {
                        noiseMap[x, y, z] = 1;
                        continue;
                    }*/
                    var noiseSample = fastNoise.GetNoise(x + sampleCentre.x, y + sampleCentre.y, z + sampleCentre.z);
                    //noiseSample += -(((float) y / chunkHeight) - 0.25f);
                    noiseMap[x, y, z] = noiseSample;
                }
            }
        }

        return noiseMap;
    }

    public static Block[,,] GenerateNoiseMapNew(int chunkSize, int chunkHeight, float chunkPosX, float chunkPosZ) {
        var noiseMap = new Block[chunkSize, chunkHeight, chunkSize];

        var sampleCentre = new Vector3(chunkPosX * chunkSize, 0, chunkPosZ* chunkSize);
        for (var y = 0; y < chunkHeight; y++) {
            for (var x = 0; x < chunkSize; x++) {
                for (var z = 0; z < chunkSize; z++) {
                    if (y == 0) {
                        noiseMap[x, y, z] = Block.Stone;
                        continue;
                    }
                    var noiseSample = fastNoise.GetNoise(x + sampleCentre.x, y + sampleCentre.y, z + sampleCentre.z);
                    //noiseSample += -((float) y / chunkHeight - 0.25f);
	                noiseMap[x, y, z] = noiseSample >= 0 ? Block.Stone : Block.Air;
                }
            }
        }

        return noiseMap;
    }

    public static float[,,] GenerateNoiseMapTest(int chunkSize, int chunkHeight, float chunkPosX, float chunkPosZ) {
        var noiseMap = new float[chunkSize, chunkHeight, chunkSize];

        var sampleCentre = new Vector3(chunkPosX, 0, chunkPosZ) * chunkSize;
        var offsetX = prng.Next (-100000, 100000) + offset.x + sampleCentre.x;
        var offsetY = prng.Next (-100000, 100000) - offset.y - sampleCentre.y;
        var offsetZ = prng.Next (-100000, 100000) + offset.z + sampleCentre.z;

        for (var y = 0; y < chunkHeight; y++) {
			for (var x = 0; x < chunkSize; x++) {
				for (var z = 0; z < chunkSize; z++) {
                    var simplexValue = fastNoise.GetNoise(x + offsetX, y + offsetY, z + offsetZ);
                    //simplexValue += -(((float) y / chunkHeight) - 0.75f);
                    noiseMap[x, y, z] = simplexValue;
				}
			}
		}


        /*float halfWidth = chunkSize / 2f;
        float halfHeight = chunkHeight / 2f;

        Vector3 sampleCentre = new Vector3(chunkPosX, 0, chunkPosZ) * chunkSize;
        float offsetX = prng.Next (-100000, 100000) + offset.x + sampleCentre.x;
        float offsetY = prng.Next (-100000, 100000) - offset.y - sampleCentre.y;
        float offsetZ = prng.Next (-100000, 100000) + offset.z + sampleCentre.z;

        for (int y = 0; y < chunkHeight; y++) {
			for (int x = 0; x < chunkSize; x++) {
				for (int z = 0; z < chunkSize; z++) {
                    float sampleX = (x-halfWidth + offsetX) / scale;
                    float sampleY = (y-halfHeight + offsetY) / scale;
                    float sampleZ = (z-halfWidth + offsetZ) / scale;
                    float simplexValue = simplexNoise.noise (sampleX, sampleY, sampleZ);
                    simplexValue += -(((float) y / chunkHeight) - 0.75f);
                    noiseMap[x, y, z] = simplexValue;
				}
			}
		}*/
        return noiseMap;
    }

	public static Block[,,] GenerateNoiseMap(int chunkSize, int chunkHeight, float chunkPosX, float chunkPosZ) {
		var noiseMap = new Block[chunkSize, chunkHeight, chunkSize];
        float maxPossibleHeight = 0;
        float amplitude = 1;
        float frequency = 1;

		var octaveOffsets = new Vector3[octaves];
        var sampleCentre = new Vector3(chunkPosX, 0, chunkPosZ) * chunkSize;

		for (var i = 0; i < octaves; i++) {
			var offsetX = prng.Next (-100000, 100000) + offset.x + sampleCentre.x;
			var offsetY = prng.Next (-100000, 100000) - offset.y - sampleCentre.y;
			var offsetZ = prng.Next (-100000, 100000) + offset.z + sampleCentre.z;
			octaveOffsets [i] = new Vector3 (offsetX, offsetY, offsetZ);

			maxPossibleHeight += amplitude;
			amplitude *= persistence;
		}

		var halfWidth = chunkSize / 2f;
		var halfHeight = chunkHeight / 2f;

		for (var y = 0; y < chunkHeight; y++) {
			for (var x = 0; x < chunkSize; x++) {
				for (var z = 0; z < chunkSize; z++) {
					amplitude = 1;
					frequency = 1;
					float noiseHeight = 0;

					for (var i = 0; i < octaves; i++) {
						var sampleX = (x-halfWidth + octaveOffsets[i].x) / scale * frequency;
						var sampleY = (y-halfHeight + octaveOffsets[i].y) / scale * frequency;
						var sampleZ = (z-halfWidth + octaveOffsets[i].z) / scale * frequency;

                        var noiseValue = fastNoise.GetNoise(sampleX, sampleY, sampleZ);
						noiseHeight += noiseValue * amplitude;

						amplitude *= persistence;
						frequency *= lacunarity;
					}

                    noiseHeight += -(((float) y / chunkHeight) - 0.75f);
					noiseMap[x, y, z] = noiseHeight >= 0 ? Block.Stone : Block.Air;
				}
			}
		}
		return noiseMap;
	}
}
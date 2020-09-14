public class ChunkSection {
    public static byte size = 16;
    public static int totalSectionBlockCount = size * size * size;

    public int yStart = 0;
    public int dominantId = 0;
    public bool hasDominantId = false;
}
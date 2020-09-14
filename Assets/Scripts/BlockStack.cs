public struct BlockStack {
    public static BlockStack Stone = new BlockStack(Block.Stone.id);
    public static BlockStack Air = new BlockStack(Block.Air.id);
    
    public byte id;
    //public byte faceConfig = 0;

    public byte ABOVE;
    public byte BELOW;
    public byte BACK;
    public byte FRONT;
    public byte RIGHT;
    public byte LEFT;
    
    public BlockStack(byte id) {
        this.id = id;
        ABOVE = Block.Invalid.id;
        BELOW = Block.Invalid.id;
        BACK = Block.Invalid.id;
        FRONT = Block.Invalid.id;
        RIGHT = Block.Invalid.id;
        LEFT = Block.Invalid.id;
    }

    public bool isAir() {
        return id == Block.Air.id;
    }
}
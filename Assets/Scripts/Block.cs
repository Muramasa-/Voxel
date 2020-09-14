using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Block {
    //public static Texture2D atlas = TextureHelper.ATLAS;
    public static LinkedList<byte> registeredBlocks = new LinkedList<byte>();
    //private static byte currentId;

    public byte id { get; private set; }
    public bool isTransparent { get; private set; }
    public string name { get; private set; }
    public string textureName { get; private set; }
    public Vector2[] uvs { get; set; }
    
    public static Block Invalid = new Block("Invalid", 0, true);
    public static Block Air = new Block("Air", 1, true);
    public static Block Stone = new Block("Stone", 2, "textures/blocks/stone.png", false);
    public static Block Dirt = new Block("Dirt", 3, "textures/blocks/dirt.png", false);
    public static Block Grass = new Block("Grass", 4, "textures/blocks/grass.png", false);

    public static byte[] registeredBlocksArray = registeredBlocks.ToArray();
    
    public Block(string name, byte id, bool isTransparent) {
        this.name = name;
        this.isTransparent = isTransparent;
        this.id = id;
        registeredBlocks.AddLast(id);
    }

    public Block(string name, byte id, string textureName, bool isTransparent) : this(name, id, isTransparent) {
        this.textureName = textureName;
        this.uvs = new Vector2[]{};
    }

    public void Init() {

    }

    public void Tick() {

    }
}
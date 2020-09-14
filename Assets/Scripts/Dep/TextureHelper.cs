using UnityEngine;
using System.Collections.Generic;
using System.IO;

public static class TextureHelper {
    public static Texture2D ATLAS = null;

	public static void GenerateTextureAtlas() {
		var _Images = Directory.GetFiles("textures/blocks/");
		var PixelWidth = 16;
		var PixelHeight = 16;
		var atlaswidth = Mathf.CeilToInt((Mathf.Sqrt(_Images.Length)+1) * PixelWidth);
		var atlasheight = Mathf.CeilToInt((Mathf.Sqrt(_Images.Length)+1) * PixelHeight);
		var Atlas = new Texture2D(atlaswidth,atlasheight);
		Atlas.filterMode = FilterMode.Point;
		var count = 0;

		for(var x = 0; x< atlaswidth / PixelWidth; x++) {
			for (var y = 0; y < atlaswidth / PixelWidth ; y++) {
				if (count > _Images.Length-1) break;
				var temp = new Texture2D(0, 0);
				temp.LoadImage(File.ReadAllBytes(_Images[count]));
				Atlas.SetPixels(x * PixelWidth, y * PixelHeight, PixelWidth, PixelHeight, temp.GetPixels());
				float startx = x * PixelWidth;
				float starty = y * PixelHeight;
				var perpixelratiox = 1.0f / (float)Atlas.width;
				var perpixelratioy = 1.0f / (float)Atlas.height;
				startx *= perpixelratiox;
				starty *= perpixelratioy;
				var endx = startx + (perpixelratiox * PixelWidth);
				var endy = starty + (perpixelratioy * PixelHeight);

				foreach(var block in Block.registeredBlocks) {
					//if (_Images[count] == block.) {
					//	block.uvs = new Vector2[] {new Vector2(startx,starty),new Vector2(startx,endy),new Vector2(endx,starty),new Vector2(endx,endy)};
					//}
				}
				count++;
			}
		}
        Atlas.filterMode = FilterMode.Point;
		File.WriteAllBytes("atlas.png", Atlas.EncodeToPNG());
        ATLAS = Atlas;
	}
}
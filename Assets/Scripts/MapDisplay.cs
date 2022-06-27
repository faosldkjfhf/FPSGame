using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Takes a noise map, turns it into a texture, and applies that texture to a plane
public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void DrawTexture(Texture2D texture) {
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData mesh, Texture2D texture) {
        meshFilter.sharedMesh = mesh.createMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}

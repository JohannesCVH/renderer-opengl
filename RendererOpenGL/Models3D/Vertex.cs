using OpenTK.Mathematics;

namespace RendererOpenGL.Models3D;

public struct Vertex
{
    public Vector3 Position { get; set; }
    public Vector3 Normal { get; set; }
    public Vector2 TextureCoordinates { get; set; }
    
    public Vertex(Vector3 position)
    {
        Position = position;
    }
}

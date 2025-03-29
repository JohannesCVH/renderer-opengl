using OpenTK.Mathematics;

namespace RendererOpenGL;

public class Entity
{
    public Vector3 Position { get; set; }

	public Entity(Vector3 pos)
	{
		Position = pos;
	}
}

using OpenTK.Mathematics;
using static RendererOpenGL.Globals;

namespace RendererOpenGL;

internal class Program
{
	static void Main(string[] args)
	{	
		using (var re = new RenderingEngine(800, 600, "Renderer OpenGL"))
		{
			re.Run();
		}

		for (int i = 0; i < ENTITIES.Count; i++)
		{
			ENTITIES[i].Dispose();
		}
	}
}
using OpenTK.Graphics.OpenGL4;

namespace RendererOpenGL;

public class RawModel : IDisposable
{
    private int _vao;
	public int VAO { get { return _vao; } private set { _vao = value; } }
    public int VertexCount { get; set ;}
	private int _vbo;
	public int VBO { get { return _vbo; } private set { _vbo = value; } }
    private int _ebo;
	public int EBO { get { return _ebo; } private set { _ebo = value; } }

    public RawModel(float[] vertices, int[] faces)
    {
        VAO = GL.GenVertexArray();
        GL.BindVertexArray(VAO);

        VBO = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        EBO = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, faces.Length * sizeof(uint), faces, BufferUsageHint.StaticDraw);

        //Attributes in a vertex shader are disabled by default, to use one first enable it with this function -> EnableVertexAttribArray(layoutLocation)
        GL.EnableVertexAttribArray(0);
        //This sets the vertex locations attribute in the vertex shader. The index parameter (first parameter) is the location of the attribute in the shader i.e. layout (location = 0) in vec3 aPosition; would make the index 0
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

        //Unbind
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindVertexArray(0);
    }

    public void Dispose()
    {   
        Console.Write("Disposing RawModel..");
        GL.DeleteBuffer(VBO);
        GL.DeleteVertexArray(VAO);
        Console.WriteLine("DONE");
    }
}

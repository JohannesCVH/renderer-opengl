using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace RendererOpenGL.Models3D;

public class Mesh : IDisposable
{
    public Vertex[] Vertices { get; set; }
    public int VertexCount { get { return Vertices.Length; } }
    public uint[] Indices { get; set; }
    // public Texture[] Textures { get; set; }
    public Vector4[] Colors { get; set; }
    
    private int _vao;
	public int VAO { get { return _vao; } private set { _vao = value; } }
	private List<int> _vertexBufferObjects = new List<int>();
	public List<int> VertexBufferObjects { get { return _vertexBufferObjects; } }
    // private int _ebo;
	// public int EBO { get { return _ebo; } private set { _ebo = value; } }
    
    public Mesh()
    {
        
    }
    
    public void CreateMesh()
    {
        VAO = GL.GenVertexArray();
        GL.BindVertexArray(VAO);

        StoreDataInAttributeList(0, 3, Vertices.Select(x => x.Position).ToArray());
        StoreDataInAttributeList(1, 3, Vertices.Select(x => x.Normal).ToArray());

        // EBO = GL.GenBuffer();
        // GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
        // GL.BufferData(BufferTarget.ElementArrayBuffer, Indices.Length * sizeof(uint), Indices, BufferUsageHint.StaticDraw);

        //Attributes in a vertex shader are disabled by default, to use one first enable it with this function -> EnableVertexAttribArray(layoutLocation)
        GL.EnableVertexAttribArray(0);
        
        
        
        GL.BindVertexArray(0);
    }
    
    private void StoreDataInAttributeList(int attrNum, int size, Vector3[] data)
    {
        int vbo = GL.GenBuffer();
        VertexBufferObjects.Add(vbo);
        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        //size -> * 3 because x,y,z
        GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float) * 3, data, BufferUsageHint.StaticDraw);
        //This sets the vertex locations attribute in the vertex shader. 
        //The index parameter (first parameter) is the location of the attribute in the shader i.e. layout (location = 0) in vec3 aPosition; would make the index 0
        GL.VertexAttribPointer(attrNum, size, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }
    
    public void Dispose()
    {   
        Console.Write("Disposing RawModel..");
        for (int i = 0; i < VertexBufferObjects.Count; i++)
        {
            GL.DeleteBuffer(VertexBufferObjects[i]);
        }
        GL.DeleteVertexArray(VAO);
        Console.WriteLine("DONE");
    }
}

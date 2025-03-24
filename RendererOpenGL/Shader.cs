using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace RendererOpenGL;

public class Shader : IDisposable
{
    public int ShaderHandle;
    private int VertexShader;
    private int FragmentShader;

    private readonly Dictionary<string, int> UniformLocations;

    public Shader(string vertexPath, string fragmentPath)
    {
        string vertexShaderSrc = File.ReadAllText(vertexPath);
        string fragmentShaderSrc = File.ReadAllText(fragmentPath);

        VertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(VertexShader, vertexShaderSrc);

        FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(FragmentShader, fragmentShaderSrc);

        GL.CompileShader(VertexShader);

        GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out int success);
        if (success == 0)
        {
            string infoLog = GL.GetShaderInfoLog(VertexShader);
            Console.WriteLine(infoLog);
        }

        GL.CompileShader(FragmentShader);

        GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out success);
        if (success == 0)
        {
            string infoLog = GL.GetShaderInfoLog(FragmentShader);
            Console.WriteLine(infoLog);
        }

        ShaderHandle = GL.CreateProgram();

        GL.AttachShader(ShaderHandle, VertexShader);
        GL.AttachShader(ShaderHandle, FragmentShader);

        GL.LinkProgram(ShaderHandle);

        GL.GetProgram(ShaderHandle, GetProgramParameterName.LinkStatus, out success);
        if (success == 0)
        {
            string infoLog = GL.GetProgramInfoLog(ShaderHandle);
            Console.WriteLine(infoLog);
        }

        GL.DetachShader(ShaderHandle, VertexShader);
        GL.DetachShader(ShaderHandle, FragmentShader);
        GL.DeleteShader(FragmentShader);
        GL.DeleteShader(VertexShader);

        GL.GetProgram(ShaderHandle, GetProgramParameterName.ActiveUniforms, out var uniformCount);

        UniformLocations = new Dictionary<string, int>();

        for (var i = 0; i < uniformCount; i++)
        {
            //Get uniform name
            var key = GL.GetActiveUniform(ShaderHandle, i, out _, out _);

            var location = GL.GetUniformLocation(ShaderHandle, key);
            UniformLocations.Add(key, location);
        }
    }

    public void Use()
    {
        GL.UseProgram(ShaderHandle);
    }

    public void SetMatrix4(string name, Matrix4 data)
    {
        GL.UseProgram(ShaderHandle);
        GL.UniformMatrix4(UniformLocations[name], true, ref data);
    }

    private bool DisposedValue = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!DisposedValue)
        {
            GL.DeleteProgram(ShaderHandle);

            DisposedValue = true;
        }
    }

    ~Shader()
    {
        if (DisposedValue == false)
        {
            Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

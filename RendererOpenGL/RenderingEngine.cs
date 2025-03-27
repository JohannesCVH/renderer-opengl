using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using static RendererOpenGL.Globals;
using static RendererOpenGL.Camera;
using static RendererOpenGL.MathLib;

namespace RendererOpenGL;

public class RenderingEngine : GameWindow
{
    private Shader Shader;
    
    public RenderingEngine(int width, int height, string title) : 
        base(GameWindowSettings.Default, new NativeWindowSettings() { 
            Size = (width, height), Title = title 
        })
    {
        CenterWindow();
        UpdateFrequency = 30;
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        //Load Meshes
        string filePathTeapot = Path.Combine(
			Directory.GetCurrentDirectory(),
			"./Assets/teapot.obj"
		);
		
		string filePathShip = Path.Combine(
			Directory.GetCurrentDirectory(),
			"./Assets/ship.obj"
		);

		string filePathCube = Path.Combine(
			Directory.GetCurrentDirectory(),
			"./Assets/cube.obj"
		);

		ObjReader cubeReader = new ObjReader(filePathCube);
		ObjReader shipReader = new ObjReader(filePathShip);
		ObjReader teapotReader = new ObjReader(filePathTeapot);

        Entity MainEntity = new Entity(
            new Vector3(0.0f, 0.0f, 6.0f),
            1.0f,
            0.0f,
            cubeReader.Vertices.ToArray(),
            cubeReader.Faces.ToArray()
        );
        MainEntity.Rotation = 0.5f;

        ENTITIES.Add(MainEntity);

        //Shaders
        string vertexShaderPath = Path.Combine(Directory.GetCurrentDirectory(), "Shaders/shader.vert");
        string fragmentShaderPath = Path.Combine(Directory.GetCurrentDirectory(), "Shaders/shader.frag");
        Shader = new Shader(vertexShaderPath, fragmentShaderPath);

        //Clear screen
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
    {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, e.Width, e.Height);
        WINDOW_ASPECT = (float)WINDOW_WIDTH / WINDOW_HEIGHT;
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);
        HandleInput();
        ENTITIES[0].RotateYaw(1);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        GL.Clear(ClearBufferMask.ColorBufferBit);

        //Code goes here.
        for (int i = 0; i < ENTITIES.Count; i++)
        {
            GL.BindVertexArray(ENTITIES[i].RawModel.VAO);
            GL.EnableVertexAttribArray(0);

            var transform = CreateTransformationMatrix(ENTITIES[i].Position, ENTITIES[i].ScaleFactor, ENTITIES[i].RotX, ENTITIES[i].RotY, ENTITIES[i].RotZ);
            var view = GetViewMatrix();
            var projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(WINDOW_FOV), WINDOW_ASPECT, Z_NEAR, Z_FAR);

            Shader.Use();
            Shader.SetMatrix4("model", transform);
            Shader.SetMatrix4("view", view);
            Shader.SetMatrix4("projection", projection);
            
            GL.DrawElements(PrimitiveType.Triangles, ENTITIES[i].Faces.Length, DrawElementsType.UnsignedInt, 0);

            GL.DisableVertexAttribArray(0);
            GL.BindVertexArray(0);
        }

        SwapBuffers();
    }

    protected override void OnUnload()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindVertexArray(0);
        GL.UseProgram(0);

        GL.DeleteProgram(Shader.ShaderHandle);

        Shader.Dispose();
        base.OnUnload();
    }

    private void HandleInput()
    {
        if (KeyboardState.IsKeyDown(Keys.Escape)) Close();

        if (KeyboardState.IsKeyDown(Keys.W)) CAMERA_POS += CAMERA_DIR * CAMERA_SPEED;
        if (KeyboardState.IsKeyDown(Keys.S)) CAMERA_POS -= CAMERA_DIR * CAMERA_SPEED;
        if (KeyboardState.IsKeyDown(Keys.A)) CAMERA_POS += CAMERA_RIGHT * CAMERA_SPEED;
        if (KeyboardState.IsKeyDown(Keys.D)) CAMERA_POS -= CAMERA_RIGHT * CAMERA_SPEED;
    }
}

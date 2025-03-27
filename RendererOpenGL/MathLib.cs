using OpenTK.Mathematics;

namespace RendererOpenGL;

public static class MathLib
{
    public static Matrix4 CreateTransformationMatrix(Vector3 pos, float scale, float rotX, float rotY, float rotZ)
    {
        var transform = Matrix4.Identity;
        transform = transform * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotX));
        transform = transform * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotY));
        transform = transform * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotZ));
        transform = transform * Matrix4.CreateScale(scale);
        transform = transform * Matrix4.CreateTranslation(pos);
        
        return transform;
    }
}
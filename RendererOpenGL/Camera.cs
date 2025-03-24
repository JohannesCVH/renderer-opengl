using OpenTK.Mathematics;

namespace RendererOpenGL;

public static class Camera
{
    public static Vector3 CAMERA_POS = new Vector3(0.0f, 0.0f, 4.0f);
	public static Vector3 CAMERA_TAR = Vector3.Zero;
	//Since we know that the camera points towards the negative z direction we want the direction vector to point towards the camera's positive z-axis. 
	//If we switch the subtraction order around we now get a vector pointing towards the camera's positive z-axis.
	//The name direction vector is not the best chosen name, since it is actually pointing in the reverse direction of what it is targeting.
	//I.e. the camera direction is pointing TO the camera.
	public static Vector3 CAMERA_DIR = Vector3.Normalize(CAMERA_POS - CAMERA_TAR);
    public static Vector3 CAMERA_RIGHT = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, CAMERA_DIR));
    public static Vector3 CAMERA_UP = Vector3.Normalize(Vector3.Cross(CAMERA_DIR, CAMERA_RIGHT));

    public static float CAMERA_SPEED = 0.5f;

    public static Matrix4 GetViewMatrix()
    {
        return Matrix4.LookAt(CAMERA_POS, CAMERA_POS + CAMERA_DIR, CAMERA_UP);
    }
}

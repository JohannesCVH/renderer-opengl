namespace RendererOpenGL;

internal static class Globals
{
	public const int WINDOW_WIDTH = 1280;
	public const int WINDOW_HEIGHT = 768;
	public const int WINDOW_WIDTH_HALF = WINDOW_WIDTH / 2;
	public const int WINDOW_HEIGHT_HALF = WINDOW_HEIGHT / 2;
	public static int WINDOW_FOV = 80;
	public static float WINDOW_ASPECT = (float)WINDOW_WIDTH / WINDOW_HEIGHT;
	public static float Z_NEAR = 0.4f;
	public static float Z_FAR = 100.0f;
	public static bool PERSPECTIVE = true;
	public const float WORLD_SIZE = 1.0f;
	public static bool ENABLE_ROTATION = false;
	public static DateTime SETTING_CHANGE_LAST_UPDATED = DateTime.Now;

    public static List<Entity> ENTITIES = new List<Entity>();
}
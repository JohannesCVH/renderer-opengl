using OpenTK.Mathematics;

namespace RendererOpenGL;

public class Entity : IDisposable
{
    public Vector3 Position { get; set; }

	public float ScaleFactor { get; set; }
	public float Rotation { get; set; }
	private float _angle;
	public float Angle { 
		get { return _angle; } 
		set {
			if (_angle + value >= 360) 
				_angle = 0 + value;
			else if (_angle + value < 0) 
				_angle = 360 + value;
			else _angle = value;
		}
	}
	public float[] Vertices { get; set; }
	public int[] Faces { get; set; }
	public RawModel RawModel { get; set; }

	public Entity(Vector3 pos, float scaleF, float angle, float[] vertices, int[] faces)
	{
		Position = pos;
		ScaleFactor = scaleF;
		Angle = angle;
		Vertices = vertices;
		Faces = faces;
		RawModel = new RawModel(vertices, faces);
	}

	public void Rotate(float? angle = null)
	{   
		Angle += angle ?? Rotation;
	}

	public void RotateRoll(float? angle = null)
	{   
		Angle += angle ?? Rotation;
	}

	public void RotateYaw(float? angle = null)
	{   
		Angle += angle ?? Rotation;
	}

	public void RotatePitch(float? angle = null)
	{   
		Angle += angle ?? Rotation;
	}

    public void Dispose()
    {
        RawModel.Dispose();
    }
}

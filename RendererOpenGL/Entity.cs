using OpenTK.Mathematics;

namespace RendererOpenGL;

public class Entity : IDisposable
{
    public Vector3 Position { get; set; }

	public float ScaleFactor { get; set; }
	public float Rotation { get; set; }
	private float _rotX;
	public float RotX { 
		get { return _rotX; } 
		set {
			if (_rotX + value >= 360) 
				_rotX = 0 + value;
			else if (_rotX + value < 0) 
				_rotX = 360 + value;
			else _rotX = value;
		}
	}
	private float _rotY;
	public float RotY { 
		get { return _rotY; } 
		set {
			if (_rotY + value >= 360) 
				_rotY = 0 + value;
			else if (_rotY + value < 0) 
				_rotY = 360 + value;
			else _rotY = value;
		}
	}
	private float _rotZ;
	public float RotZ { 
		get { return _rotZ; } 
		set {
			if (_rotZ + value >= 360) 
				_rotZ = 0 + value;
			else if (_rotZ + value < 0) 
				_rotZ = 360 + value;
			else _rotZ = value;
		}
	}
	public float[] Vertices { get; set; }
	public int[] Faces { get; set; }
	public RawModel RawModel { get; set; }

	public Entity(Vector3 pos, float scaleF, float angle, float[] vertices, int[] faces)
	{
		Position = pos;
		ScaleFactor = scaleF;
		RotX = angle;
		Vertices = vertices;
		Faces = faces;
		RawModel = new RawModel(vertices, faces);
	}


	public void RotatePitch(float angle)
	{   
		RotX += angle;
	}

	public void RotateYaw(float angle)
	{   
		RotY += angle;
	}
	
	public void RotateRoll(float angle)
	{   
		RotZ += angle;
	}

    public void Dispose()
    {
        RawModel.Dispose();
    }
}

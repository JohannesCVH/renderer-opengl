using System.Globalization;
using OpenTK.Mathematics;
using RendererOpenGL.Models3D;

namespace RendererOpenGL;

public class ObjReader
{
    private string[] ObjLines { get; set; }
    private string[] mtlLines { get; set; }
    public List<Vector3> Vertices { get; set; } = new List<Vector3>();
    private List<Vector3> Normals { get; set; } = new List<Vector3>();
    public List<uint> Faces { get; set; } = new List<uint>();
    private List<Vertex> VerticesMain { get; set; } = new List<Vertex>();
    public Mesh Mesh { get; set; }
    
    public ObjReader(string filePathObj, string? filePathMtl)
    {
        ObjLines = File.ReadAllLines(filePathObj);
        
        if (filePathMtl != null) 
            mtlLines = File.ReadAllLines(filePathMtl);
        
        Mesh = new Mesh();
        
        Process();
    }

    private void Process()
    {
        //Read vertices
        for (int i = 0; i < ObjLines.Length; i++)
        {
            string[] tokens = ObjLines[i].Split(' ');
            if (tokens[0].Equals("v"))
            {
                Vertices.Add(new Vector3(
                    float.Parse(tokens[1], CultureInfo.InvariantCulture),
                    float.Parse(tokens[2], CultureInfo.InvariantCulture),
                    float.Parse(tokens[3], CultureInfo.InvariantCulture)
                ));
            }
        }

        //Read Normals
        for (int i = 0; i < ObjLines.Length; i++)
        {
            string[] tokens = ObjLines[i].Split(' ');
            if (tokens[0].Equals("vn"))
            {
                Normals.Add(new Vector3(
                    float.Parse(tokens[1], CultureInfo.InvariantCulture),
                    float.Parse(tokens[2], CultureInfo.InvariantCulture),
                    float.Parse(tokens[3], CultureInfo.InvariantCulture)
                ));
            }
        }
        
        //Read Faces
        for (int i = 0; i < ObjLines.Length; i++)
        {
            string[] tokens = ObjLines[i].Trim().Split(' ');
            if (tokens[0].Equals("f") && !ObjLines[i].Contains('/'))
            {
                uint verLoc1 = uint.Parse(tokens[1]) - 1;
                uint verLoc2 = uint.Parse(tokens[2]) - 1;
                uint verLoc3 = uint.Parse(tokens[3]) - 1;
                
                Faces.Add(verLoc1);
                Faces.Add(verLoc2);
                Faces.Add(verLoc3);
                
                VerticesMain.Add(new Vertex(Vertices[(int)verLoc1]));
                VerticesMain.Add(new Vertex(Vertices[(int)verLoc2]));
                VerticesMain.Add(new Vertex(Vertices[(int)verLoc3]));
            }
            else if (tokens[0].Equals("f") && ObjLines[i].Contains('/'))
            {
                for (int j = 1; j < tokens.Length; j++)
                {
                    string[] nums = tokens[j].Split('/');
                    
                    uint verLoc1 = uint.Parse(nums[0]) - 1;
                    // uint verLoc2 = uint.Parse(nums[1]) - 1;
                    uint verLoc3 = uint.Parse(nums[2]) - 1;
                    
                    Faces.Add(verLoc1);
                    
                    VerticesMain.Add(new Vertex(Vertices[(int)verLoc1]) { Normal = Normals[(int)verLoc3] });
                }
            }
        }
        
        Mesh.Vertices = VerticesMain.ToArray();
        Mesh.Indices = Faces.ToArray();
        Mesh.CreateMesh();
    }
}
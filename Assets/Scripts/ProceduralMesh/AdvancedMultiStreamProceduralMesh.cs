using Unity.Collections;
using Unity.Mathematics;
using static Unity.Mathematics.math;
using UnityEngine;
using UnityEngine.Rendering;
using float3 = Unity.Mathematics.float3;

namespace HEXWorld.ProceduralMesh
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class AdvancedMultiStreamProceduralMesh : MonoBehaviour
    {
        private void OnEnable()
        {
            int vertexAttributeCount = 4;
            int vertextCount = 4;

            Mesh.MeshDataArray meshDataArray = Mesh.AllocateWritableMeshData(1);
            Mesh.MeshData meshData = meshDataArray[0];

            var vertexAttributes = new NativeArray<VertexAttributeDescriptor>(vertexAttributeCount, Allocator.Temp,
                NativeArrayOptions.UninitializedMemory);
            vertexAttributes[0] = new VertexAttributeDescriptor(dimension: 3);
            vertexAttributes[1] = new VertexAttributeDescriptor(VertexAttribute.Normal, dimension: 3, stream: 1);
            vertexAttributes[2] = new VertexAttributeDescriptor(VertexAttribute.Tangent, dimension: 4, stream: 2);
            vertexAttributes[3] = new VertexAttributeDescriptor(VertexAttribute.TexCoord0, dimension: 2, stream: 3);
            meshData.SetVertexBufferParams(vertextCount, vertexAttributes);
            vertexAttributes.Dispose();

            NativeArray<float3> positions = meshData.GetVertexData<float3>();
            positions[0] = 0f;
            positions[1] = float3(1f, 0f, 0f);
            positions[2] = up();
            positions[3] = float3(1f, 1f, 1f);

            NativeArray<float3> normals = meshData.GetVertexData<float3>(1);
            normals[0] = normals[0] = normals[0] = normals[0] = float3(0f, 0f, -1f);
            
            NativeArray<float4> tangets = meshData.GetVertexData<float4>(2);
            tangets[0] = tangets[0] = tangets[0] = tangets[0] = float4(1f, 0f, 0f, -1f);

            NativeArray<float2> texCoords = meshData.GetVertexData<float2>(3);
            texCoords[0] = 0f;
            texCoords[1] =  float2(1f, 0f);
            texCoords[2] = float2(0f, 1f);
            texCoords[3] = 1f;

            var mesh = new Mesh
            {
                name = "Advanced procedural mesh"
            };
            Mesh.ApplyAndDisposeWritableMeshData(meshDataArray, mesh);
            GetComponent<MeshFilter>().mesh = mesh;
        }
    }
}
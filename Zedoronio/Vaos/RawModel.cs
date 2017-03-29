using System;
using OpenTK.Graphics.OpenGL;

namespace Zedoronio.Vaos
{
	public class RawModel : IDisposable
	{
		public int Vertices { get; set; }
		public Vao Vao { get; set; }
		public RawModel(float[] vertices, uint[] indexBuffers)
		{
			Vertices = indexBuffers.Length;
			Vao = new Vao();
			Vao.CreateVbos(vertices, indexBuffers);
		}

		public void Render()
		{
			GL.BindVertexArray(Vao.Id);
			GL.EnableVertexAttribArray(0);
			GL.DrawElements(PrimitiveType.Triangles, Vertices, DrawElementsType.UnsignedInt, 0);
			GL.DisableVertexAttribArray(0);
			GL.BindVertexArray(0);
		}

		public void Dispose()
		{
			Vao.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}

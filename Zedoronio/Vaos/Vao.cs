using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace Zedoronio.Vaos
{
	public class Vao : IDisposable
	{
		public int Id { get; set; }
		public List<int> Vbos { get; set; } = new List<int>();

		public Vao()
		{
			Id = GL.GenVertexArray();
		}

		public void CreateVbos(float[] vertices, uint[] indices)
		{
			Bind();
			StoreIndices(indices);
			StoreVertices(vertices);
			Unbind();
		}

		public void Bind()
		{
			GL.BindVertexArray(Id);
		}

		public void Unbind()
		{
			GL.BindVertexArray(0);
		}

		public int CreateEmptyVbo()
		{
			int id = GL.GenBuffer();
			Vbos.Add(id);
			return id;
		}

		public void StoreIndices(uint[] indices)
		{
			int id = CreateEmptyVbo();
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, id);
			GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(uint) * indices.Length, indices, BufferUsageHint.StaticDraw);
		}

		public void StoreVertices(float[] vertices)
		{
			int id = CreateEmptyVbo();
			GL.BindBuffer(BufferTarget.ArrayBuffer, id);
			GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * vertices.Length, vertices, BufferUsageHint.StaticDraw);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		}

		public void Dispose()
		{
			GL.DeleteVertexArray(Id);
			foreach (var vbo in Vbos)
				GL.DeleteBuffer(vbo);
			GC.SuppressFinalize(this);
		}
	}
}

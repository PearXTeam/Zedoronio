using System.Collections.Generic;

namespace Zedoronio.Vaos
{
	public class ModelLoader
	{
		public Dictionary<string, RawModel> Models { get; }= new Dictionary<string, RawModel>();

		public void Create(string name, float[] vertices, uint[] indexBuffers)
		{
			Models.Add(name, new RawModel(vertices, indexBuffers));
		}

		public void RemoveVao(string name)
		{
			Models[name].Dispose();
			Models.Remove(name);
		}
	}
}

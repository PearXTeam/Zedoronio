using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using OpenTK.Graphics.OpenGL;
using Zedoronio.Textures;

namespace Zedoronio.Typography
{
    public class TypographyAtlas : Texture, IDisposable
    {
        public static byte[] Signature { get; } =  {90, 70, 78, 84};

        public Dictionary<char, TypographyAtlasEntry> Entries { get; set; } = new Dictionary<char, TypographyAtlasEntry>();
        public Texture Texture { get; set; } = new Texture();
        public short Height { get; set; }

        public void LoadAtlas(Bitmap bmp)
        {
            Texture.MinFilter = TextureMinFilter.Linear;
            Texture.MagFilter = TextureMagFilter.Linear;
            Texture.LoadImage(bmp);
        }

        public void SaveEntries(Stream str)
        {
            using (BinaryWriter wr = new BinaryWriter(str))
            {
                wr.Write(Signature);
                wr.Write(Height);
                foreach (var pair in Entries)
                {
                    wr.Write(pair.Key);
                    wr.Write(pair.Value.X);
                    wr.Write(pair.Value.Width);
                }
            }
        }

        public void SaveEntries(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                SaveEntries(fs);
            }
        }

        public void LoadEntries(Stream str)
        {
            using (BinaryReader r = new BinaryReader(str))
            {
                var sig = r.ReadBytes(Signature.Length);
                if (!sig.SequenceEqual(Signature))
                {
                    throw new FileLoadException("Invalid signature!");
                }
                Height = r.ReadInt16();
                Entries = new Dictionary<char, TypographyAtlasEntry>();
                while (true)
                {
                    try
                    {
                        var ch = r.ReadChar();
                        TypographyAtlasEntry entr = new TypographyAtlasEntry();
                        entr.X = r.ReadInt16();
                        entr.Width = r.ReadInt16();
                        Entries.Add(ch, entr);
                    }
                    catch (EndOfStreamException)
                    {
                        break;
                    }
                }
            }
        }

        public void LoadEntries(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                LoadEntries(fs);
            }
        }
    }
}
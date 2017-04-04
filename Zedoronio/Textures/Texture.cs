using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using PearXLib.Maths;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace Zedoronio.Textures
{
    public class Texture : IDisposable
    {
        private TextureWrapMode wm = TextureWrapMode.Repeat;
        private TextureMinFilter minf = TextureMinFilter.Nearest;
        private TextureMagFilter magf = TextureMagFilter.Nearest;
        public int Id { get; set; }

        public TextureWrapMode WrapMode
        {
            get { return wm; }
            set
            {
                wm = value;
                GL.BindTexture(TextureTarget.Texture2D, Id);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) WrapMode);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) WrapMode);
            }
        }

        public TextureMinFilter MinFilter
        {
            get { return minf; }
            set
            {
                minf = value;
                GL.BindTexture(TextureTarget.Texture2D, Id);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) MinFilter);
            }
        }

        public TextureMagFilter MagFilter
        {
            get { return magf; }
            set
            {
                magf = value;
                GL.BindTexture(TextureTarget.Texture2D, Id);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) MagFilter);
            }
        }

        public Size Size { get; private set; }

        public Texture()
        {
            Id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, Id);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) WrapMode);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) WrapMode);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) MinFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) MagFilter);
        }

        public void LoadImage(Image img)
        {
            Size = img.Size;
            using (var bmp = new Bitmap(img))
            {
                GL.Enable(EnableCap.Texture2D);
                GL.BindTexture(TextureTarget.Texture2D, Id);
                var dat = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, dat.Width, dat.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, dat.Scan0);
                bmp.UnlockBits(dat);
                GL.Disable(EnableCap.Texture2D);
            }
        }


        public void Remove()
        {
            GL.DeleteTexture(Id);
        }


        public void Dispose()
        {
            Remove();
        }
    }
}
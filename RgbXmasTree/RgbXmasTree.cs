using System.Device.Spi;
using System.Drawing;
using Iot.Device.Spi;

// ReSharper disable UnusedMember.Global

namespace Wingandprayer.PiTree3D.RgbXmasTree
{
    public enum BrightnessMode
    {
        UseMasterBrightness = 0,
        UseColorAlpha = 1
    }

    public enum IndexMode
    {
        Normal = 0,
        Tinsel = 1
    }

    public class RgbXmasTree : IDisposable
    {
        private static readonly byte[] TinselMap = {
            15, 16, 0, 7, 19, 24, 12, 6,
            14, 17, 1, 8, 20, 23, 11, 5,
            13, 18, 2, 9, 21, 22, 10, 4,
            3
        };

        private const int NumLights = 25;
        private readonly SpiDevice _spiDevice;
        private readonly Color[] _lights = new Color[NumLights];
        private byte _brightness;

        public Color this[int i]
        {
            get => _lights[IndexMode == IndexMode.Tinsel ? TinselMap[i] : i];
            set => _lights[IndexMode == IndexMode.Tinsel ? TinselMap[i] : i] = value;
        }

        public RgbXmasTree()
        {
            _spiDevice = new SoftwareSpi(25, -1, 12);
            Off();
            Mode = BrightnessMode.UseMasterBrightness;
            Brightness = 31;
        }

        public BrightnessMode Mode { get; set; }

        public IndexMode IndexMode { get; set; }

        public byte Brightness
        {
            get => _brightness;
            set => _brightness = Math.Min((byte)31, value);
        }

        public Color Color
        {
            set
            {
                Array.Fill(_lights, value);
                Send();
            }
        }

        public void Off()
        {
            Color = Color.Black;
        }

        // ReSharper disable once UnusedMember.Global
        public void On()
        {
            Color = Color.White;
        }

        public void Send()
        {
            byte[] buffer = new byte[NumLights * 4 + 9];

            for (int i = 0; i < NumLights; i++)
            {
                buffer[(i + 1) * 4 + 0] = (byte)((Mode == BrightnessMode.UseColorAlpha ? _lights[i].A / 32 : Brightness) | 0b11100000);
                buffer[(i + 1) * 4 + 1] = _lights[i].B;
                buffer[(i + 1) * 4 + 2] = _lights[i].G;
                buffer[(i + 1) * 4 + 3] = _lights[i].R;
            }

            _spiDevice.Write(buffer);
        }

        public void Dispose()
        {
            ((IDisposable)_spiDevice).Dispose();
        }
    }
}

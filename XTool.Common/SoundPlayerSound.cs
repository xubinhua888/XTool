using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XTool.Common.Properties;

namespace XTool.Common
{
    public class SoundPlayerSound : IDisposable
    {
        /// <summary>
        /// 正常声音
        /// </summary>
        private SoundPlayer objSoundSoundPlayer = new SoundPlayer();

        private byte[] byteSuccess = null;

        private byte[] byteError = null;

        private Stream currentSound = null;

        private CancellationTokenSource cts = new CancellationTokenSource();

        private byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        private Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        private void Play()
        {
            while (true)
            {
                try
                {
                    if (cts.Token.IsCancellationRequested)
                    {
                        break;
                    }
                    lock (this)
                    {
                        if (currentSound != null)
                        {
                            objSoundSoundPlayer.Stop();
                            objSoundSoundPlayer = new SoundPlayer(currentSound);
                            objSoundSoundPlayer.Play();
                            currentSound = null;
                        }
                    }
                    Thread.Sleep(20);
                }
                catch { }
            }
        }

        #region ISound 成员

        public void Init()
        {
            byteSuccess = StreamToBytes(Resources.new_success);
            byteError = StreamToBytes(Resources.new_error);
            Thread threadPlayer = new Thread(new ThreadStart(Play));
            threadPlayer.Priority = ThreadPriority.Highest;
            threadPlayer.Start();
        }

        public void Success()
        {
            lock (this)
            {
                currentSound = BytesToStream(byteSuccess);
            }
        }

        public void Error()
        {
            lock (this)
            {
                currentSound = BytesToStream(byteError);
            }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            cts.Cancel();
        }

        #endregion
    }
}

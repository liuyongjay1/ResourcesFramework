using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace MotionFramework.Network
{
    public class DownloadHandlerFileStream : DownloadHandlerScript
    {
        protected Action<long, float> onProgress;

        private FileStream _fs;
        private long _downloadedSize;
        private float _lastRecordTime;
        private int _totalLength;

        private int _deltaLength;
        
        public DownloadHandlerFileStream(FileStream fs, int bufferSize=4096, long downloaded=0, Action<long, float> onProgressCallback = null) : base(new byte[bufferSize])
        {
            _fs = fs;
            _downloadedSize = downloaded;
            onProgress = onProgressCallback;
            _lastRecordTime = Time.realtimeSinceStartup;
            _totalLength = -1;
            _deltaLength = 0;
        }

        protected override float GetProgress()
        {
            return _totalLength <= 0 ? 0 : Mathf.Clamp01((float)_downloadedSize / (float)_totalLength);
        }

        protected override void ReceiveContentLength(int contentLength)
        {
            _totalLength = contentLength;
        }

        protected override void CompleteContent()
        {
            float deltaTime = Time.realtimeSinceStartup - _lastRecordTime;
            if (deltaTime <= 0.2f)
            {
                onProgress(_downloadedSize, _deltaLength / 1024f / deltaTime);
            }
        }

        protected override bool ReceiveData(byte[] data, int dataLength)
        {
            if (data == null || data.Length == 0) return false;
            _downloadedSize += dataLength;
            _fs.Write(data, 0, dataLength);
            float deltaTime = Time.realtimeSinceStartup - _lastRecordTime;
            _deltaLength += dataLength;
            if (deltaTime > 0.2f)
            {
                onProgress(_downloadedSize, _deltaLength / 1024f / deltaTime);
                _deltaLength = 0;
                _lastRecordTime += deltaTime;
            }
            
            return true;
        }
    }
}

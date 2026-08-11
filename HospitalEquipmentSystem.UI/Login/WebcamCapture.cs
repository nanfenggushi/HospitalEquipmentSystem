using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Drawing;

namespace HospitalEquipmentSystem.UI.Login
{
    /// <summary>
    /// 基于 AForge.Video.DirectShow 的摄像头采集封装。
    /// AForge 内部已经封装了 DirectShow 枚举、pin 连接和帧回调，
    /// 比手写 COM 更稳定，也不需要新增界面控件。
    /// </summary>
    internal sealed class WebcamCapture : IDisposable
    {
        private readonly object _sync = new object();
        private VideoCaptureDevice _device;
        private Bitmap _latestFrame;
        private bool _hasNewFrame;
        private bool _isRunning;
        private bool _disposed;

        public bool IsRunning
        {
            get
            {
                lock (_sync)
                {
                    return _isRunning;
                }
            }
        }

        /// <summary>
        /// 启动指定索引的摄像头，cameraIndex 对应 App.config 的 RGB_CAMERA_INDEX。
        /// </summary>
        public void Start(int cameraIndex)
        {
            ThrowIfDisposed();
            if (_isRunning)
                return;

            FilterInfoCollection devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (devices.Count == 0)
                throw new InvalidOperationException("系统没有可用的摄像头设备。");

            if (cameraIndex < 0 || cameraIndex >= devices.Count)
                throw new InvalidOperationException(
                    "指定摄像头不存在，当前共有 " + devices.Count + " 个摄像头，请检查 RGB_CAMERA_INDEX。");

            _device = new VideoCaptureDevice(devices[cameraIndex].MonikerString);
            _device.NewFrame += Device_NewFrame;

            try
            {
                _device.Start();
                lock (_sync)
                {
                    _isRunning = true;
                }
            }
            catch
            {
                Stop();
                throw;
            }
        }

        /// <summary>
        /// 获取最新一帧。没有新帧时返回 null；调用方负责 Dispose。
        /// </summary>
        public Bitmap GetLatestFrame()
        {
            lock (_sync)
            {
                if (!_hasNewFrame || _latestFrame == null)
                    return null;

                _hasNewFrame = false;
                return (Bitmap)_latestFrame.Clone();
            }
        }

        public void Stop()
        {
            VideoCaptureDevice device;
            lock (_sync)
            {
                _isRunning = false;
                _hasNewFrame = false;
                device = _device;
            }

            if (device != null)
            {
                device.NewFrame -= Device_NewFrame;
                try
                {
                    if (device.IsRunning)
                    {
                        device.SignalToStop();
                        device.WaitForStop();
                    }
                }
                catch
                {
                }

                lock (_sync)
                {
                    if (ReferenceEquals(_device, device))
                        _device = null;
                }
            }

            lock (_sync)
            {
                if (_latestFrame != null)
                {
                    _latestFrame.Dispose();
                    _latestFrame = null;
                }
            }
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            Stop();
            _disposed = true;
        }

        private void Device_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap copy;
            try
            {
                copy = (Bitmap)eventArgs.Frame.Clone();
            }
            catch
            {
                return;
            }

            lock (_sync)
            {
                if (_disposed || !_isRunning)
                {
                    copy.Dispose();
                    return;
                }

                Bitmap old = _latestFrame;
                _latestFrame = copy;
                _hasNewFrame = true;
                old?.Dispose();
            }
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(WebcamCapture));
        }
    }
}

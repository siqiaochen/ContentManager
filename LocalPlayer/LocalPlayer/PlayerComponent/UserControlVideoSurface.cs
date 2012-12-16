using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DirectShowLib;
using System.Runtime.InteropServices;
using System.IO;

namespace LocalPlayer.PlayerComponent
{
    public partial class UserControlVideoSurface : UserControl, IFrameControl
    {
        public UserControlVideoSurface()
        {
            InitializeComponent();
        }

        internal enum PlayState
        {
            Stopped,
            Paused,
            Running,
            Init
        };

        internal enum MediaType
        {
            Audio,
            Video
        }

        private const int WMGraphNotify = 0x0400 + 13;
        private const int VolumeFull = 0;
        private const int VolumeSilence = -10000;

        private IGraphBuilder graphBuilder = null;
        private IMediaControl mediaControl = null;
        private IMediaEventEx mediaEventEx = null;
        private IVideoWindow videoWindow = null;
        private IBasicAudio basicAudio = null;
        private IBasicVideo basicVideo = null;
        private IMediaSeeking mediaSeeking = null;
        private IMediaPosition mediaPosition = null;
        private IVideoFrameStep frameStep = null;

        private string filename = string.Empty;
        private bool isAudioOnly = false;
        private bool isFullScreen = false;
        private int currentVolume = VolumeFull;
        private PlayState currentState = PlayState.Stopped;
        private double currentPlaybackRate = 1.0;

        private IntPtr hDrain = IntPtr.Zero;

#if DEBUG
        private DsROTEntry rot = null;
#endif

        public event PlayFinishedEventHandler PlayFinishedEvent;



        /*
     * Graph creation and destruction methods
     */

        private void OpenClip()
        {
            try
            {
                // If no filename specified by command line, show file open dialog
                if (this.filename == string.Empty)
                {
                    UpdateMainTitle();

                    this.filename = GetClipFileName();
                    if (this.filename == string.Empty)
                        return;
                }

                // Reset status variables
                this.currentState = PlayState.Stopped;
                this.currentVolume = VolumeFull;

                // Start playing the media file
                PlayMovieInWindow(this.filename);
            }
            catch
            {
                CloseClip();
            }
        }

        public void PlayFile(string filepath)
        {
            try
            {
                // If no filename specified by command line, show file open dialog

                UpdateMainTitle();
                if (File.Exists(filepath))
                    this.filename = filepath;
                else
                    this.filename = string.Empty;

                if (this.filename == string.Empty) 
                    if (PlayFinishedEvent != null)
                    {
                        PlayFinishedEvent(this, EventArgs.Empty);
                    }


                // Reset status variables
                this.currentState = PlayState.Stopped;
                this.currentVolume = VolumeFull;

                // Start playing the media file
                PlayMovieInWindow(this.filename);
            }
            catch(Exception exp)
            {
                CloseClip();
                if (PlayFinishedEvent != null)
                {
                    PlayFinishedEvent(this, EventArgs.Empty);
                }
            }
        }
        public void Replay()
        {
            if (this.mediaControl != null)
            {
                try
                {
                    int hr = 0;
                    hr = this.mediaControl.Stop();
                    DsError.ThrowExceptionForHR(hr);
                    hr = this.mediaControl.Run();
                    DsError.ThrowExceptionForHR(hr);
                }
                catch (Exception exp)
                { 
                
                }
            }
        }
        private void PlayMovieInWindow(string filename)
        {
            int hr = 0;

            if (filename == string.Empty)
                return;

            this.graphBuilder = (IGraphBuilder)new FilterGraph();
            //graphBuilder.AddSourceFilter
            // Have the graph builder construct its the appropriate graph automatically
            hr = this.graphBuilder.RenderFile(filename, null);
            DsError.ThrowExceptionForHR(hr);

            // QueryInterface for DirectShow interfaces
            this.mediaControl = (IMediaControl)this.graphBuilder;
            this.mediaEventEx = (IMediaEventEx)this.graphBuilder;
            this.mediaSeeking = (IMediaSeeking)this.graphBuilder;
            this.mediaPosition = (IMediaPosition)this.graphBuilder;

            // Query for video interfaces, which may not be relevant for audio files
            this.videoWindow = this.graphBuilder as IVideoWindow;
            this.basicVideo = this.graphBuilder as IBasicVideo;

            // Query for audio interfaces, which may not be relevant for video-only files
            this.basicAudio = this.graphBuilder as IBasicAudio;

            // Is this an audio-only file (no video component)?
            CheckVisibility();

            // Have the graph signal event via window callbacks for performance
            hr = this.mediaEventEx.SetNotifyWindow(this.Handle, WMGraphNotify, IntPtr.Zero);
            DsError.ThrowExceptionForHR(hr);

            if (!this.isAudioOnly)
            {
                // Setup the video window
                hr = this.videoWindow.put_Owner(this.Handle);
                DsError.ThrowExceptionForHR(hr);

                hr = this.videoWindow.put_WindowStyle(WindowStyle.Child | WindowStyle.ClipSiblings | WindowStyle.ClipChildren);
                DsError.ThrowExceptionForHR(hr);

                //hr = InitVideoWindow(1, 1);
                hr = this.videoWindow.SetWindowPosition(0, 0, this.Width, this.Height);
                DsError.ThrowExceptionForHR(hr);

                GetFrameStepInterface();
            }
            else
            {
                // Initialize the default player size and enable playback menu items
                hr = InitPlayerWindow();
                DsError.ThrowExceptionForHR(hr);
            }

            // Complete window initialization
            this.isFullScreen = false;
            this.currentPlaybackRate = 1.0;


#if DEBUG
            rot = new DsROTEntry(this.graphBuilder);
#endif

            this.Focus();

            // Run the graph to play the media file
            hr = this.mediaControl.Run();
            DsError.ThrowExceptionForHR(hr);

            this.currentState = PlayState.Running;
        }

        public void CloseClip()
        {
            int hr = 0;

            // Stop media playback
            if (this.mediaControl != null)
                hr = this.mediaControl.Stop();

            // Clear global flags
            this.currentState = PlayState.Stopped;
            this.isAudioOnly = true;
            this.isFullScreen = false;

            // Free DirectShow interfaces
            CloseInterfaces();

            // Clear file name to allow selection of new file with open dialog
            this.filename = string.Empty;

            // No current media state
            this.currentState = PlayState.Init;

            UpdateMainTitle();
            InitPlayerWindow();
        }

        public void SetDuration(int secs)
        {
            //duration = secs;
        }

        private int InitVideoWindow(int nMultiplier, int nDivider)
        {
            int hr = 0;
            int lHeight, lWidth;

            if (this.basicVideo == null)
                return 0;

            // Read the default video size
            hr = this.basicVideo.GetVideoSize(out lWidth, out lHeight);
            if (hr == DsResults.E_NoInterface)
                return 0;

            // Account for requests of normal, half, or double size
            lWidth = lWidth * nMultiplier / nDivider;
            lHeight = lHeight * nMultiplier / nDivider;

            this.ClientSize = new Size(lWidth, lHeight);
            Application.DoEvents();

            hr = this.videoWindow.SetWindowPosition(0, 0, lWidth, lHeight);

            return hr;
        }

        private void MoveVideoWindow()
        {
            int hr = 0;

            // Track the movement of the container window and resize as needed
            if (this.videoWindow != null)
            {
                hr = this.videoWindow.SetWindowPosition(
                  this.ClientRectangle.Left,
                  this.ClientRectangle.Top,
                  this.ClientRectangle.Width,
                  this.ClientRectangle.Height
                  );
                DsError.ThrowExceptionForHR(hr);
            }
        }

        private void CheckVisibility()
        {
            int hr = 0;
            OABool lVisible;

            if ((this.videoWindow == null) || (this.basicVideo == null))
            {
                // Audio-only files have no video interfaces.  This might also
                // be a file whose video component uses an unknown video codec.
                this.isAudioOnly = true;
                return;
            }
            else
            {
                // Clear the global flag
                this.isAudioOnly = false;
            }

            hr = this.videoWindow.get_Visible(out lVisible);
            if (hr < 0)
            {
                // If this is an audio-only clip, get_Visible() won't work.
                //
                // Also, if this video is encoded with an unsupported codec,
                // we won't see any video, although the audio will work if it is
                // of a supported format.
                if (hr == unchecked((int)0x80004002)) //E_NOINTERFACE
                {
                    this.isAudioOnly = true;
                }
                else
                    DsError.ThrowExceptionForHR(hr);
            }
        }

        //
        // Some video renderers support stepping media frame by frame with the
        // IVideoFrameStep interface.  See the interface documentation for more
        // details on frame stepping.
        //
        private bool GetFrameStepInterface()
        {
            int hr = 0;

            IVideoFrameStep frameStepTest = null;

            // Get the frame step interface, if supported
            frameStepTest = (IVideoFrameStep)this.graphBuilder;

            // Check if this decoder can step
            hr = frameStepTest.CanStep(0, null);
            if (hr == 0)
            {
                this.frameStep = frameStepTest;
                return true;
            }
            else
            {
                // BUG 1560263 found by husakm (thanks)...
                // Marshal.ReleaseComObject(frameStepTest);
                this.frameStep = null;
                return false;
            }
        }

        private void CloseInterfaces()
        {
            int hr = 0;

            try
            {
                lock (this)
                {
                    // Relinquish ownership (IMPORTANT!) after hiding video window
                    if (!this.isAudioOnly)
                    {
                        hr = this.videoWindow.put_Visible(OABool.False);
                        DsError.ThrowExceptionForHR(hr);
                        hr = this.videoWindow.put_Owner(IntPtr.Zero);
                        DsError.ThrowExceptionForHR(hr);
                    }

                    if (this.mediaEventEx != null)
                    {
                        hr = this.mediaEventEx.SetNotifyWindow(IntPtr.Zero, 0, IntPtr.Zero);
                        DsError.ThrowExceptionForHR(hr);
                    }

#if DEBUG
                    if (rot != null)
                    {
                        rot.Dispose();
                        rot = null;
                    }
#endif
                    // Release and zero DirectShow interfaces
                    if (this.mediaEventEx != null)
                        this.mediaEventEx = null;
                    if (this.mediaSeeking != null)
                        this.mediaSeeking = null;
                    if (this.mediaPosition != null)
                        this.mediaPosition = null;
                    if (this.mediaControl != null)
                        this.mediaControl = null;
                    if (this.basicAudio != null)
                        this.basicAudio = null;
                    if (this.basicVideo != null)
                        this.basicVideo = null;
                    if (this.videoWindow != null)
                        this.videoWindow = null;
                    if (this.frameStep != null)
                        this.frameStep = null;
                    if (this.graphBuilder != null)
                        Marshal.ReleaseComObject(this.graphBuilder); this.graphBuilder = null;

                    GC.Collect();
                }
            }
            catch
            {
            }
        }

        /*
         * Media Related methods
         */

        public void PauseClip()
        {
            if (this.mediaControl == null)
                return;

            // Toggle play/pause behavior
            if ((this.currentState == PlayState.Paused) || (this.currentState == PlayState.Stopped))
            {
                if (this.mediaControl.Run() >= 0)
                    this.currentState = PlayState.Running;
            }
            else
            {
                if (this.mediaControl.Pause() >= 0)
                    this.currentState = PlayState.Paused;
            }

            UpdateMainTitle();
        }

        private void StopClip()
        {
            int hr = 0;
            DsLong pos = new DsLong(0);

            if ((this.mediaControl == null) || (this.mediaSeeking == null))
                return;

            // Stop and reset postion to beginning
            if ((this.currentState == PlayState.Paused) || (this.currentState == PlayState.Running))
            {
                hr = this.mediaControl.Stop();
                this.currentState = PlayState.Stopped;

                // Seek to the beginning
                hr = this.mediaSeeking.SetPositions(pos, AMSeekingSeekingFlags.AbsolutePositioning, null, AMSeekingSeekingFlags.NoPositioning);

                // Display the first frame to indicate the reset condition
                hr = this.mediaControl.Pause();
            }
            UpdateMainTitle();
        }
        public void Stop()
        {
            StopClip();
        }
        private int ToggleMute()
        {
            int hr = 0;

            if ((this.graphBuilder == null) || (this.basicAudio == null))
                return 0;

            // Read current volume
            hr = this.basicAudio.get_Volume(out this.currentVolume);
            if (hr == -1) //E_NOTIMPL
            {
                // Fail quietly if this is a video-only media file
                return 0;
            }
            else if (hr < 0)
            {
                return hr;
            }

            // Switch volume levels
            if (this.currentVolume == VolumeFull)
                this.currentVolume = VolumeSilence;
            else
                this.currentVolume = VolumeFull;

            // Set new volume
            hr = this.basicAudio.put_Volume(this.currentVolume);

            UpdateMainTitle();
            return hr;
        }
        
        private int ToggleFullScreen()
        {
            int hr = 0;
            OABool lMode;

            // Don't bother with full-screen for audio-only files
            if ((this.isAudioOnly) || (this.videoWindow == null))
                return 0;

            // Read current state
            hr = this.videoWindow.get_FullScreenMode(out lMode);
            DsError.ThrowExceptionForHR(hr);

            if (lMode == OABool.False)
            {
                // Save current message drain
                hr = this.videoWindow.get_MessageDrain(out hDrain);
                DsError.ThrowExceptionForHR(hr);

                // Set message drain to application main window
                hr = this.videoWindow.put_MessageDrain(this.Handle);
                DsError.ThrowExceptionForHR(hr);

                // Switch to full-screen mode
                lMode = OABool.True;
                hr = this.videoWindow.put_FullScreenMode(lMode);
                DsError.ThrowExceptionForHR(hr);
                this.isFullScreen = true;
            }
            else
            {
                // Switch back to windowed mode
                lMode = OABool.False;
                hr = this.videoWindow.put_FullScreenMode(lMode);
                DsError.ThrowExceptionForHR(hr);

                // Undo change of message drain
                hr = this.videoWindow.put_MessageDrain(hDrain);
                DsError.ThrowExceptionForHR(hr);

                // Reset video window
                hr = this.videoWindow.SetWindowForeground(OABool.True);
                DsError.ThrowExceptionForHR(hr);

                // Reclaim keyboard focus for player application
                //this.Focus();
                this.isFullScreen = false;
            }

            return hr;
        }

        private int StepOneFrame()
        {
            int hr = 0;

            // If the Frame Stepping interface exists, use it to step one frame
            if (this.frameStep != null)
            {
                // The graph must be paused for frame stepping to work
                if (this.currentState != PlayState.Paused)
                    PauseClip();

                // Step the requested number of frames, if supported
                hr = this.frameStep.Step(1, null);
            }

            return hr;
        }

        private int StepFrames(int nFramesToStep)
        {
            int hr = 0;

            // If the Frame Stepping interface exists, use it to step frames
            if (this.frameStep != null)
            {
                // The renderer may not support frame stepping for more than one
                // frame at a time, so check for support.  S_OK indicates that the
                // renderer can step nFramesToStep successfully.
                hr = this.frameStep.CanStep(nFramesToStep, null);
                if (hr == 0)
                {
                    // The graph must be paused for frame stepping to work
                    if (this.currentState != PlayState.Paused)
                        PauseClip();

                    // Step the requested number of frames, if supported
                    hr = this.frameStep.Step(nFramesToStep, null);
                }
            }

            return hr;
        }

        private int ModifyRate(double dRateAdjust)
        {
            int hr = 0;
            double dRate;

            // If the IMediaPosition interface exists, use it to set rate
            if ((this.mediaPosition != null) && (dRateAdjust != 0.0))
            {
                hr = this.mediaPosition.get_Rate(out dRate);
                if (hr == 0)
                {
                    // Add current rate to adjustment value
                    double dNewRate = dRate + dRateAdjust;
                    hr = this.mediaPosition.put_Rate(dNewRate);

                    // Save global rate
                    if (hr == 0)
                    {
                        this.currentPlaybackRate = dNewRate;
                        UpdateMainTitle();
                    }
                }
            }

            return hr;
        }

        private int SetRate(double rate)
        {
            int hr = 0;

            // If the IMediaPosition interface exists, use it to set rate
            if (this.mediaPosition != null)
            {
                hr = this.mediaPosition.put_Rate(rate);
                if (hr >= 0)
                {
                    this.currentPlaybackRate = rate;
                    UpdateMainTitle();
                }
            }

            return hr;
        }

        private void HandleGraphEvent()
        {
            int hr = 0;
            EventCode evCode;
            IntPtr evParam1, evParam2;

            // Make sure that we don't access the media event interface
            // after it has already been released.
            if (this.mediaEventEx == null)
                return;

            // Process all queued events
            while (this.mediaEventEx.GetEvent(out evCode, out evParam1, out evParam2, 0) == 0)
            {
                // Free memory associated with callback, since we're not using it
                hr = this.mediaEventEx.FreeEventParams(evCode, evParam1, evParam2);

                // If this is the end of the clip, just stop
                if (evCode == EventCode.Complete)
                {

                    hr = this.mediaControl.Stop();

                    CloseClip();
                    if (PlayFinishedEvent != null)
                    {
                        PlayFinishedEvent(this, EventArgs.Empty);
                    }
                    break;
                    /*
                    DsLong pos = new DsLong(0);
                    // Reset to first frame of movie
                    hr = this.mediaSeeking.SetPositions(pos, AMSeekingSeekingFlags.AbsolutePositioning,
                      null, AMSeekingSeekingFlags.NoPositioning);
                    if (hr < 0)
                    {
                        // Some custom filters (like the Windows CE MIDI filter)
                        // may not implement seeking interfaces (IMediaSeeking)
                        // to allow seeking to the start.  In that case, just stop
                        // and restart for the same effect.  This should not be
                        // necessary in most cases.
                        hr = this.mediaControl.Stop();
                        hr = this.mediaControl.Run();
                    }
                    */ 
                }
            }
        }

        /*
         * WinForm Related methods
         */

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WMGraphNotify:
                    {
                        HandleGraphEvent();
                        break;
                    }
            }

            // Pass this message to the video window for notification of system changes
            if (this.videoWindow != null)
                this.videoWindow.NotifyOwnerMessage(m.HWnd, m.Msg, m.WParam, m.LParam);

            base.WndProc(ref m);
        }

        private string GetClipFileName()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog1.FileName;
            }
            else
                return string.Empty;
        }

        private int InitPlayerWindow()
        {
            // Reset to a default size for audio and after closing a clip
            this.ClientSize = new Size(240, 120);

            // Check the 'full size' menu item

            return 0;
        }

        private void UpdateMainTitle()
        {
            // If no file is loaded, just show the application title
            if (this.filename == string.Empty)
                this.Text = "PlayWnd Media Player";
            else
            {
                string media = (isAudioOnly) ? "Audio" : "Video";
                string muted = (currentVolume == VolumeSilence) ? "Mute" : "";
                string paused = (currentState == PlayState.Paused) ? "Paused" : "";

                this.Text = String.Format("{0} [{1}] {2}{3}", System.IO.Path.GetFileName(this.filename), media, muted, paused);
            }
        }

        private void menuFileOpenClip_Click(object sender, System.EventArgs e)
        {
            // If we have ANY file open, close it and shut down DirectShow
            if (this.currentState != PlayState.Init)
                CloseClip();

            // Open the new clip
            OpenClip();
        }

        private void menuFileClose_Click(object sender, System.EventArgs e)
        {
            CloseClip();
        }

        private void menuFilePause_Click(object sender, System.EventArgs e)
        {
            PauseClip();
        }

        private void menuFileStop_Click(object sender, System.EventArgs e)
        {
            StopClip();
        }

        private void menuFileMute_Click(object sender, System.EventArgs e)
        {
            ToggleMute();
        }

        private void menuFileFullScreen_Click(object sender, System.EventArgs e)
        {
            ToggleFullScreen();
        }

        private void menuSingleStep_Click(object sender, System.EventArgs e)
        {
            StepOneFrame();
        }

        private void MainForm_Move(object sender, System.EventArgs e)
        {
            if (!this.isAudioOnly)
                MoveVideoWindow();
        }

        private void MainForm_Resize(object sender, System.EventArgs e)
        {
            if (!this.isAudioOnly)
                MoveVideoWindow();
        }

        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StopClip();
            CloseInterfaces();
        }

        private void UserControlVideoFrame_Move(object sender, EventArgs e)
        {

            MoveVideoWindow();
        }

        private void UserControlVideoFrame_Resize(object sender, EventArgs e)
        {
            MoveVideoWindow();
        }
        public void ReleaseResources()
        {
            CloseClip();
            CloseInterfaces();
        }

        private void UserControlVideoFrame_Load(object sender, EventArgs e)
        {

            //state = PlayState.Init;
        }

        private void UserControlVideoFrame_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}

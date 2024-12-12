using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace StopWatch
{
    public partial class Form1 : Form
    {
        private Stopwatch stopwatch = new Stopwatch();
        private Timer timer;
        private double ticksPerMicrosecond;

        public Form1()
        {
            InitializeComponent();

            // 1マイクロ秒当たりのクロック数を計算
            ticksPerMicrosecond = (double)Stopwatch.Frequency / 1_000_000;

            // Timerを初期化
            timer = new Timer();
            timer.Interval = 10; // 10msごとに更新
            timer.Tick += Timer_Tick;

            // イベントハンドラを設定
            btnStart.Click += BtnStart_Click;
            btnStop.Click += BtnStop_Click;
            btnReset.Click += BtnReset_Click;

            lblTimer.Text = "00:00:00.00";

            // 初期状態のボタン有効化設定
            UpdateButtonStates();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 経過時間を計算し、マイクロ秒単位で表示
            long elapsedTicks = stopwatch.ElapsedTicks;
            double elapsedMicroseconds = elapsedTicks / ticksPerMicrosecond;

            TimeSpan ts = stopwatch.Elapsed;
            lblTimer.Text = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 100, (int)(elapsedMicroseconds % 100));
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            stopwatch.Start();
            timer.Start();
            UpdateButtonStates();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            stopwatch.Stop();
            timer.Stop();
            UpdateButtonStates();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            stopwatch.Reset();
            timer.Stop();
            lblTimer.Text = "00:00:00.00";
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            if (stopwatch.IsRunning)
            {
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                btnReset.Enabled = false;
            }
            else
            {
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                btnReset.Enabled = stopwatch.ElapsedMilliseconds > 0; // リセットは何か時間が経過しているときのみ有効
            }
        }

        private void lblTimer_Click(object sender, EventArgs e)
        {

        }
    }
}

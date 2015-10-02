//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="TimerForm.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.UI
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// This is a timer form which can be used to track aggression.
    /// </summary>
    public partial class TimerForm : Form
    {
        /// <summary>
        /// This is our aggression time in seconds, 15 minutes.
        /// </summary>
        private const int AggressionTime = 15 * 60;

        /// <summary>
        /// Holds our timer.
        /// </summary>
        private Timer timer = new Timer();

        /// <summary>
        /// Holds an object lock to keep things thread safe(well, somewhat).
        /// </summary>
        private object objlock = new object();

        /// <summary>
        /// Holds the Date/Time that the timer started.
        /// </summary>
        private DateTime startDateTime = DateTime.MinValue;

        /// <summary>
        /// If (for some reason) you want to stop the timer, this holds the remaining time.
        /// </summary>
        private double pauseDuration = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerForm"/> class.
        /// </summary>
        public TimerForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Runs on form load, sets things up.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void TimerForm_Load(object sender, EventArgs e)
        {
            this.ResetButton_Click(null, EventArgs.Empty);
            this.timer.Interval = 10;
            this.timer.Tick += this.Timer_Tick;
        }

        /// <summary>
        /// This method runs when the timer ticks.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => this.Timer_Tick(sender, e)));
            }
            else
            {
                lock (this.objlock)
                {
                    TimeSpan span = DateTime.Now.Subtract(this.startDateTime);

                    double timeRemaining = AggressionTime - span.TotalSeconds;
                    if (timeRemaining <= 0)
                    {
                        this.timer.Enabled = false;
                    }

                    if (timeRemaining <= 0)
                    {
                        timeRemaining = 0;
                    }

                    int minutes = ((int)timeRemaining - ((int)timeRemaining % 60)) / 60;
                    int seconds = (int)timeRemaining % 60;
                    double frac = timeRemaining - (int)timeRemaining;
                    int fracInt = (int)(frac * 100);

                    this.minsLabel.Text = (minutes < 10 ? "0" : string.Empty) + minutes.ToString();
                    this.secsLabel.Text = (seconds < 10 ? "0" : string.Empty) + seconds.ToString();
                    this.fractionLabel.Text = (fracInt < 10 ? "0" : string.Empty) + fracInt.ToString();
                }
            }
        }

        /// <summary>
        /// This function runs when the Reset button is clicked (and when the form loads). 
        /// It sets things back up to initial values.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            this.startDateTime = DateTime.Now;

            this.minsLabel.Text = "15";
            this.secsLabel.Text = "00";
            this.fractionLabel.Text = "00";

            this.pauseDuration = 0;
        }

        /// <summary>
        /// This function runs when the start button is clicked. It starts the timer,
        /// but, if the timer was paused, sets variables up to make it look like it wasn't.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void StartButton_Click(object sender, EventArgs e)
        {
            if (!this.timer.Enabled)
            {
                if (this.pauseDuration > 0)
                {
                    this.startDateTime = DateTime.Now.Subtract(TimeSpan.FromSeconds(AggressionTime)).Add(TimeSpan.FromSeconds(this.pauseDuration));
                }
                else
                {
                    this.startDateTime = DateTime.Now;
                }

                this.timer.Start();
            }
        }

        /// <summary>
        /// This function runs when you click the stop button. It saves the time remaining
        /// so you can click start again.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void StopButton_Click(object sender, EventArgs e)
        {
            if (this.timer.Enabled)
            {
                this.timer.Stop();
                
                TimeSpan span = DateTime.Now.Subtract(this.startDateTime);

                this.pauseDuration = AggressionTime - span.TotalSeconds;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Stock_Simulation
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Rectangle[] rects;
		Label[] currPriceLabels;
		Label[] changePriceLabels;
		double[] currPrices = { 1412.7, 4897.5, 3187.1, 921.9 };

		public MainWindow()
		{			
			InitializeComponent();
			InitControls();

			nameLabel1.Content = "Facebook";
			nameLabel2.Content = "Apple";
			nameLabel3.Content = "Netflix";
			nameLabel4.Content = "Google";

			var dispatcherTimer = new DispatcherTimer();
			dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
			dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
			dispatcherTimer.Start();
			
        }

		private void dispatcherTimer_Tick(object sender, EventArgs e)
		{
			SimulateStocks();

			// Forcing the CommandManager to raise the RequerySuggested event
			CommandManager.InvalidateRequerySuggested();
		}

		private void SimulateStocks()
		{

			var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
			timer.Start();
			timer.Tick += (sender, args) =>
			{
				timer.Stop();

				var random = new Random();

				double[] randomValues = 
				{
					GetRandomNumber(-1.0, 1.0),
					GetRandomNumber(-1.0, 1.0),
					GetRandomNumber(-1.0, 1.0),
					GetRandomNumber(-1.0, 1.0),
				};

				for(int i=0; i<rects.Length; i++)
				{
					if(randomValues[i] > 0)
						rects[i].Fill = new SolidColorBrush(Colors.Green);
					else
						rects[i].Fill = new SolidColorBrush(Colors.Red);
					
					currPrices[i] = Math.Round(currPrices[i] + randomValues[i], 2);
					currPriceLabels[i].Content = currPrices[i];
					changePriceLabels[i].Content = randomValues[i];
				}

			};
		}

		private void InitControls()
		{
			Rectangle[] rectsInit =
			{
				rect1,
				rect2,
				rect3,
				rect4
			};
			rects = rectsInit;

			Label[] currPriceLabelsInit =
			{
				currPriceLabel1,
				currPriceLabel2,
				currPriceLabel3,
				currPriceLabel4
			};
			currPriceLabels = currPriceLabelsInit;

			Label[] changePriceLabelsInit =
			{
				changePriceLabel1,
				changePriceLabel2,
				changePriceLabel3,
				changePriceLabel4
			};
			changePriceLabels = changePriceLabelsInit;
		}
		private double GetRandomNumber(double minimum, double maximum)
		{
			Random random = new Random();
			return Math.Round(random.NextDouble() * (maximum - minimum) + minimum, 2);
		}
	}
}

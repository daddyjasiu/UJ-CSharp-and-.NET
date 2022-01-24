using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace ClockTimer
{
	public partial class Default : System.Web.UI.Page
	{
		private DateTime data;
		//Konstruktor
		public Default()
		{
			data = new DateTime();
		}
		//funkcja dzialajaca przy wczytaniu
		protected void Page_Load(object sender, EventArgs e)
		{
			setDateToLabel();
		}
		//funkcj atimera
		protected void Timer1_Tick(object sender, EventArgs e)
		{
			setDateToLabel();
		}
		//funkcj austawiajaca czas labelowi
		private void setDateToLabel()
		{
			data = DateTime.Now;
			Label1.Text = data.Year + "-" +
			data.Month + "-" +
			data.Day + " " +
			data.Hour + ":" +
			data.Minute + ":" +
			data.Second + ":" +
			data.Millisecond;
		}
	}
}

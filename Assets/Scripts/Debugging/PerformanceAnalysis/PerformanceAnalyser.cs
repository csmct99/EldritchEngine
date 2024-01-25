using System.Collections.Generic;
using System.Diagnostics;

namespace EldritchEngine.Debugging
{
	public class PerformanceAnalyser
	{
		private Stopwatch _stopwatch;
		private string _name;
		
		private List<long> _timeRecords = new List<long>(1200);
		
		public PerformanceAnalyser()
		{
			_stopwatch = new Stopwatch();
		}
		
		public void Stop()
		{
			_stopwatch.Stop();
		}

		public void RecordCurrentTime()
		{
			_timeRecords.Add(_stopwatch.ElapsedMilliseconds);
		}
		
		public float GetAverageTime()
		{
			long sum = 0;
			foreach (long time in _timeRecords)
			{
				sum += time;
			}
			return sum / _timeRecords.Count;
		}
		
		public float GetMaxTime()
		{
			long max = 0;
			foreach (long time in _timeRecords)
			{
				if (time > max)
				{
					max = time;
				}
			}
			return max;
		}
		
		public float GetMinTime()
		{
			long min = long.MaxValue;
			foreach (long time in _timeRecords)
			{
				if (time < min)
				{
					min = time;
				}
			}
			return min;
		}
		
		public void Reset(string name)
		{
			_name = name;
			_stopwatch.Reset();
			_stopwatch.Start();
		}
		
		public void Restart()
		{
			_stopwatch.Restart();
		}
		
		public void Start()
		{
			_stopwatch.Start();
		}

		public override string ToString()
		{
			return string.Format("PerformanceAnalyser: {0} - Average: {1}ms, Max: {2}ms, Min: {3}ms", _name, GetAverageTime(), GetMaxTime(), GetMinTime());
		}
	}
}

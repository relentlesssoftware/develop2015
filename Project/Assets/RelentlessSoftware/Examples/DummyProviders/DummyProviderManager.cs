using UnityEngine;
using System.Collections.Generic;

namespace Relentless.Examples
{
	/// <summary>
	/// This is the Main interface for your game code.
	/// 
	/// It sends the SessionStarted event when all the providers are ready
	/// </summary>
	public class DummyProviderManager : ProviderManager<DummyProviderBase>
	{
		/// <summary>
		/// This is called when all providers are ready
		/// </summary>
		protected override void OnAfterInitialise()
		{
			var eventParameters = new Dictionary<string, string>() {{"Platform", Application.platform.ToString()}};
			LogEvent("SessionStarted", eventParameters);
		}

		/// <summary>
		/// Use aggregation to call all the providers in turn
		/// </summary>
		/// <param name="eventName"></param>
		/// <param name="eventParameters"></param>
		public void LogEvent(string eventName, Dictionary<string, string> eventParameters)
		{
			foreach (var provider in AllProviders())
			{
				provider.LogEvent(eventName, eventParameters);
			}
		}
	}
}

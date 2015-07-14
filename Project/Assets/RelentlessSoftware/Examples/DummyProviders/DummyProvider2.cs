using UnityEngine;
using System.Collections;

namespace Relentless.Examples
{
	/// <summary>
	/// The second example of a Provider implementation.
	/// 
	/// This one is ready immediately
	/// </summary>
	public class DummyProvider2 : DummyProviderBase
	{
		/// <summary>
		/// This could be a key that you need to log into the service.
		/// You can add more Editor data for each provider
		/// </summary>
		[SerializeField]
		private string m_publicKey;

		protected override string ProviderName
		{
			get { return "DummyProvider2"; }
		}

		public override void Initialise()
		{
			// connect to service events

			// Log into service using m_publicKey

			// you can set IsReady now, or wait for service to respond
			IsReady = true;
		}

		public override void LogEvent(string eventName, System.Collections.Generic.Dictionary<string, string> eventParameters)
		{
			// log an event to the service or provider
			Debug.Log("DummyProvider2: LogEvent " + eventName);
		}
	}
}

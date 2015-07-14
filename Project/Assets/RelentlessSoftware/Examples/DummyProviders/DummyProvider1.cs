using UnityEngine;
using System.Collections;

namespace Relentless.Examples
{
	/// <summary>
	/// The first example of a PRovider implementation.
	/// 
	/// This one waits 2 seconds before its ready
	/// </summary>
	public class DummyProvider1 : DummyProviderBase
	{
		/// <summary>
		/// This could be a project Id that you need to log into the service.
		/// You can add more Editor data for each provider
		/// </summary>
		[SerializeField]
		private string m_projectId;

		protected override string ProviderName
		{
			get { return "DummyProvider1"; }
		}

		public override void Initialise()
		{
			// connect to service events

			// Log into service using m_projectId

			// you can set IsReady now, or wait for service to respond
			StartCoroutine(LogonToServiceCoroutine());
		}

		public override void LogEvent(string eventName, System.Collections.Generic.Dictionary<string, string> eventParameters)
		{
			// log an event to the service or provider
			Debug.Log("DummyProvider1: LogEvent " + eventName);
		}

		private IEnumerator LogonToServiceCoroutine()
		{
			yield return new WaitForSeconds(2.0f);

			IsReady = true;
		}
	}
}
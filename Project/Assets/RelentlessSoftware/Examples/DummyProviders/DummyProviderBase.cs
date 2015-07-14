using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Relentless.Examples
{
	/// <summary>
	/// Shared base class for Dummy providers. This can be a way to share common code or enforce common interfaces.
	/// </summary>
	public abstract class DummyProviderBase : ProviderBase
	{
		/// <summary>
		/// All Dummy providers have to implement this method
		/// </summary>
		/// <param name="eventName"></param>
		/// <param name="eventParameters"></param>
		public abstract void LogEvent(string eventName, Dictionary<string, string> eventParameters);
	}
}

using UnityEngine;

namespace Relentless
{
	/// <summary>
	/// Derive all your providers from this class, and add them to the same gameobject that holds the ProviderManager
	/// </summary>
	public abstract class ProviderBase : MonoBehaviour
	{
		private bool m_hasCheckedPlatform = false;
		private bool m_isValid = false;

		/// <summary>
		/// Set to true when the provider is initialised. This means you vcan wait for the provider in the Manager's coroutine
		/// </summary>
		public bool IsReady { get; protected set; }

		/// <summary>
		/// Used to filter providers at runtime
		/// </summary>
		public UnityEngine.RuntimePlatform[] Platforms;

		/// <summary>
		/// Set this if you need to distinguish between more than one provider based on priority.
		/// This is used in situations where more than one provider was found and initialised, but you need to decide at runtime which one is more importany
		/// Use the helper methods on the provider manager to get the highest priority provider that is available for this platform.
		/// </summary>
		public ProviderPriority ProviderPriority = ProviderPriority.Low;

		/// <summary>
		/// A user friendly name for this provider
		/// </summary>
		protected abstract string ProviderName { get; }

		#region Construction and Initialisation

		protected ProviderBase()
		{
			IsReady = false;
		}

		/// <summary>
		/// Implement your own initialisation code for each specific provider
		/// </summary>
		public virtual void Initialise()
		{
			IsReady = true;
		}

		#endregion

		public bool IsValid()
		{
			return this.IsValid(true, true);
		}

		protected bool IsValid(bool printReason, bool cacheCheck)
		{
			if (m_hasCheckedPlatform)
			{
				return m_isValid;
			}

			m_isValid = InternalIsValid(printReason);

			if (cacheCheck)
			{
				m_hasCheckedPlatform = true;
			}

			return m_isValid;
		}

		private bool InternalIsValid(bool printReason)
		{
			if (!enabled)
			{
				if (printReason)
				{
					Debug.Log("ProviderBase: Skipping provider " + ProviderName + " as it's not enabled");
				}
				return false;
			}

			bool providerIsValid = false;

			if (Platforms == null || Platforms.Length == 0)
			{
				providerIsValid = true;
			}
			else
			{
				foreach (var platform in Platforms)
				{
					if (Application.platform == platform)
					{
						providerIsValid = true;
						break;
					}
				}
			}

			if (!providerIsValid)
			{
				if (printReason)
				{
					Debug.Log(
						"ProviderBase: Skipping provider " + ProviderName + " as it's not enabled for platform " + Application.platform);
				}

				return false;
			}

			return true;
		}
	}
}
